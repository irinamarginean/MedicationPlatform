using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;
using DataAccessLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace BusinessLogicLayer.Consumer
{
    public class ConsumerService
    {
        private const string hostname = "cow.rmq2.cloudamqp.com";
        private const string password = "LHG8QgfPwaW-id65Op2zJiuPQaNEo9D4";
        private const string queueName = "medicalplatform.irina1";
        private const string username = "ajjfhlld";

        private readonly ILogger<ConsumerService> logger;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private UserManager<IdentityUser> userManager;
        private readonly IHubContext<ActivityHub> hubContext;

        public ConsumerService(ILogger<ConsumerService> logger, IServiceScopeFactory serviceScopeFactory, IHubContext<ActivityHub> hubContext)
        {
            this.logger = logger;
            this.serviceScopeFactory = serviceScopeFactory;
            this.hubContext = hubContext;
        }

        public void StartListening()
        {
            Thread thread1 = new Thread(ReceiveActivities);
            thread1.Start();
        }

        public void ReceiveActivities()
        {
            var factory = new ConnectionFactory
            {
                HostName = hostname,
                UserName = username,
                Password = password,
                VirtualHost = username,
                DispatchConsumersAsync = true
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new AsyncEventingBasicConsumer(channel);
                
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = JsonConvert.DeserializeObject<ActivityDto>(Encoding.UTF8.GetString(body));
                    message.Label = message.Label.Trim();

                    logger.LogInformation($" [x] Received {message.Email}, {message.StartTime}, {message.EndTime}, {message.Label}");
                    

                    using(var scope = serviceScopeFactory.CreateScope()) 
                    {    
                        userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
                        var dataContext = scope.ServiceProvider.GetService<DataContext>();

                        var patient = userManager.FindByEmailAsync(message.Email).Result as PatientEntity;

                         if (IsRuleOneBroken(message))
                         {
                            string messageToSend = $"Anomaly detected on patient {patient.FirstName} {patient.LastName}. Rule 1 was broken!";

                            logger.LogInformation(messageToSend);
                            await hubContext.Clients.All.SendAsync($"notification", messageToSend);
                        
                        }
                        else if (IsRuleTwoBroken(message))
                        {
                            string messageToSend = $"Anomaly detected on patient {patient.FirstName} {patient.LastName}. Rule 2 was broken!";

                            logger.LogInformation(messageToSend);
                            await hubContext.Clients.All.SendAsync($"notification", messageToSend);
                        }
                        else if (IsRuleThreeBroken(message))
                        {
                            string messageToSend = $"Anomaly detected on patient {patient.FirstName} {patient.LastName}. Rule 3 was broken!";

                            logger.LogInformation(messageToSend);
                            await hubContext.Clients.All.SendAsync($"notification", messageToSend);
                        }

                        var activity = new ActivityEntity
                        {
                            Id = Guid.NewGuid().ToString(),
                            StartTime = message.StartTime,
                            EndTime = message.EndTime,
                            Label = message.Label,
                            PatientId = patient.Id,
                            Patient = patient
                        };

                        dataContext.Activities.Add(activity);
                        dataContext.SaveChanges();
                    }
                };
                channel.BasicConsume(queue: queueName,
                        autoAck: true,
                        consumer: consumer);


                var properties = channel.CreateBasicProperties();
                while (true)
                {
                    Thread.Sleep(50);
                }
            }
        }

        private bool IsRuleOneBroken(ActivityDto activity)
        {
            if (activity.Label == "Sleeping")
            {
                var activityDuration = activity.EndTime - activity.StartTime;

                if (activityDuration.Hours >= 7 && activityDuration.Minutes > 0) return true;
            }

            return false;
        }

         private bool IsRuleTwoBroken(ActivityDto activity)
         {
            if (activity.Label == "Leaving")
            {
                var activityDuration = activity.EndTime - activity.StartTime;

                if (activityDuration.Hours >= 5 && activityDuration.Minutes > 0) return true;
            }

            return false;
         }

        private bool IsRuleThreeBroken(ActivityDto activity)
         {
            if (activity.Label == "Showering" || activity.Label == "Toileting" || activity.Label == "Grooming")
            {
                var activityDuration = activity.EndTime - activity.StartTime;

                if (activityDuration.TotalMinutes > 30) return true;
            }

            return false;
         }
    }
}
