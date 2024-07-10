using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace RabbitMQProducer
{
    class Program
    {
        private const string hostname = "cow.rmq2.cloudamqp.com";
        private const string password = "LHG8QgfPwaW-id65Op2zJiuPQaNEo9D4";
        private const string queueName = "medicalplatform.irina1";
        private const string username = "ajjfhlld";
        private const string filePath = @"D:\University\Master Year II\MFPC\RabbitMQProducer\activity.txt";

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = hostname,
                UserName = username,
                Password = password,
                VirtualHost = username
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())

            {
                channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                var activities = File.ReadLines(filePath);

                foreach (var actiivity in activities)
                {
                    var activityItems = actiivity.Split("		").ToList();
                    var activityObject = new Activity
                    {
                        Email = activityItems[0],
                        StartTime = DateTime.Parse(activityItems[1]),
                        EndTime = DateTime.Parse(activityItems[2]),
                        Label = activityItems[3]
                    };

                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(activityObject));

                    channel.BasicPublish(exchange: "",
                                         routingKey: queueName,
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine($" [x] Sent {activityObject.Email}, {activityObject.StartTime}, {activityObject.EndTime}, {activityObject.Label}");

                    Thread.Sleep(1000);
                }
            }
        }
    }
}
