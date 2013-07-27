using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class OrderController : ApiController
    {
        [HttpGet]
        public Order Get()
        {
            return example;
        }

        [HttpGet]
        [ActionName("Index")]
        public Order Get(Guid id)
        {
            //TODO: read details
            return new Order();
        }

        [HttpPost]
        [ActionName("Index")]
        public HttpResponseMessage Post(Order order)
        {
            //TODO: save details
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [HttpPut]
        [ActionName("Index")]
        public HttpResponseMessage Put(Order order)
        {
            //TODO: save details
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        [ActionName("Index")]
        public HttpResponseMessage Delete(Guid id)
        {
            //TODO: save details
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        [HttpPost]
        public HttpResponseMessage Complete(Guid id)
        {
            //TODO: save details
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        [HttpPost]
        public HttpResponseMessage Confirm(Guid id, int timeToWait)
        {
            //TODO: save details
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        readonly Order example = new Order
        {
            Id = Guid.NewGuid(),
            Status = Status.Opened,
            Cost = 175.50m,
            Created = new DateTime(2013, 7, 27),
            Direction = new Direction
            {
                StartPoint = new GpsPoint
                {
                    Latitude = "N 33.7282",
                    Longitude = "W 85.8628"
                },
                EndPoint = new GpsPoint
                {
                    Latitude = "N 34.7552",
                    Longitude = "W 87.2428"
                }
            }
        };
    }
}
