using System.Collections.Generic;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class OrdersController : ApiController
    {
        public IEnumerable<Order> Get(string location)
        {
            return new[]
                {
                    new Order
                        {
                            Direction = new Direction
                                {
                                    StartPoint = new GpsPoint
                                        {
                                            Longitude = location,
                                            Latitude = location
                                        }
                                }
                        } 

                };
        }
    }
}
