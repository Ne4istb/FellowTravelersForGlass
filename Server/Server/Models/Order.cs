using System;

namespace WebApi.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public Direction Direction { get; set; }
        public decimal Cost { get; set; }
        public DateTime Created { get; set; }
    }

    public class Direction
    {
        public GpsPoint StartPoint { get; set; }
        public GpsPoint EndPoint { get; set; }
    }

    public class GpsPoint
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public enum Status
    {
        Opened = 0,
        Approved = 1,
        Completed = 2,
        Closed = 3
    }
}