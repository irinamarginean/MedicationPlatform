using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjectLayer.Entities
{
    public class ActivityEntity
    {
        [Key]
        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Label { get; set; }
        public string PatientId { get; set; }
        public PatientEntity Patient { get; set; }
    }
}
