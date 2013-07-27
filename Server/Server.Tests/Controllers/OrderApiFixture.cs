using System;
using System.Net;
using System.Net.Http;
using NUnit.Framework;

namespace WebApi.Tests.Controllers
{
	[TestFixture]
	public class OrderApiFixture : IntegrationApiFixtureBase
	{
	    private readonly Guid id = Guid.NewGuid();

        [Test]
        public void Should_return_orders()
        {
            var request = CreateRequest("orders?location=test", HttpMethod.Get);

            SendRequest(request);
            Console.Write(Content);

            AssertStatus(HttpStatusCode.OK);
        }

        [Test]
        public void Should_return_order()
        {
            var request = CreateRequest(String.Format("order/{0}", id), HttpMethod.Get);

            SendRequest(request);

            Console.Write(Content);

            AssertStatus(HttpStatusCode.OK);
        }

        [Test]
        public void Should_send_new_order()
        {
            var request = CreateRequest(String.Format("order/{0}", id), HttpMethod.Post);

            SendRequest(request);

            Console.Write(Content);

            AssertStatus(HttpStatusCode.Created);
        }

        [Test]
        public void Should_delete_order()
        {
            var request = CreateRequest(String.Format("order/{0}", id), HttpMethod.Delete);

            SendRequest(request);

            Console.Write(Content);

            AssertStatus(HttpStatusCode.NoContent);
        }

        [Test]
        public void Should_update_order()
        {
            var request = CreateRequest(String.Format("order/{0}", id), HttpMethod.Put);

            SendRequest(request);

            Console.Write(Content);

            AssertStatus(HttpStatusCode.OK);
        }

        [Test]
        public void Should_complete_order()
        {
            var request = CreateRequest(String.Format("order/{0}/complete", id), HttpMethod.Post);

            SendRequest(request);

            Console.Write(Content);

            AssertStatus(HttpStatusCode.NoContent);
        }

        [Test]
        public void Should_confirm_order()
        {
            var request = CreateRequest(String.Format("order/{0}/confirm?timetowait=30", id), HttpMethod.Post);

            SendRequest(request);

            Console.Write(Content);

            AssertStatus(HttpStatusCode.NoContent);
        }
	}
}
