using System;

namespace BusinessObjectLayer.Dtos
{
    public class ActivityDto
    {
        public string Email { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Label { get; set; }
    }
}
