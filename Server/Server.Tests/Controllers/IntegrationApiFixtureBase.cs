using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using NUnit.Framework;
using WebApi.App_Start;

namespace WebApi.Tests.Controllers
{
    public class IntegrationApiFixtureBase
    {
        protected HttpServer Server;
        protected HttpClient Client;

        protected Uri BaseAddress = new Uri("http://dummyname/");

        protected HttpStatusCode StatusCode;
        protected HttpResponseHeaders Headers;

        protected string Content;
        protected byte[] ContentBytes;

        [SetUp]
        public void Setup()
        {
            SetUpServer();
            SetUpClient();

            SetUpHttpContext();
        }

        void SetUpServer()
        {
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            config.Services.Replace(
                typeof(IHttpControllerActivator),
                new ApiTestControllerActivator());

            Server = new HttpServer(config);
        }

        void SetUpClient()
        {
            Client = new HttpClient(Server);
        }

        void SetUpHttpContext()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", BaseAddress.ToString(), ""),
                new HttpResponse(new StringWriter()));
        }

        protected void AssertContent(string expected)
        {
            Assert.That(Content, Is.EqualTo(expected));
        }

        protected void AssertContentBytes(byte[] expected)
        {
            Assert.That(ContentBytes, Is.EqualTo(expected));
        }

        protected void AssertETag(int version)
        {
            Assert.That(Headers.ETag.Tag, Is.EqualTo("\"" + version + "\""));
        }

        protected void AssertStatus(HttpStatusCode statusCode)
        {
            Assert.That(StatusCode, Is.EqualTo(statusCode));
        }

        protected void AssertLocation(Uri expectedUrl)
        {
            Assert.That(Headers.Location, Is.EqualTo(expectedUrl));
        }

        protected HttpRequestMessage CreateRequest(string relativeUri, HttpMethod method)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(BaseAddress, relativeUri),
                Method = method
            };

            return request;
        }

        protected void SendRequest(HttpRequestMessage request, bool contentInBytes = false)
        {
            using (var response = Client.SendAsync(request).Result)
            {
                StatusCode = response.StatusCode;
                Headers = response.Headers;

                if (contentInBytes)
                {
                    ContentBytes = response.Content == null ? null : response.Content.ReadAsByteArrayAsync().Result;
                }
                else
                {
                    Content = response.Content == null ? null : response.Content.ReadAsStringAsync().Result;
                }
            }
        }

//        protected string GetStringFromFile(string fileName)
//        {
//            return EmbeddedResourceFiles.FromCurrentAssembly().Get(fileName);
//        }
//
//        protected byte[] GetBytesFromFile(string fileName)
//        {
//            return EmbeddedResourceFiles.FromCurrentAssembly().GetBytes(fileName);
//        }
//
//        protected string SerializeToJson(object data)
//        {
//            return JsonConvert.SerializeObject(data, Server.Configuration.Formatters.JsonFormatter.SerializerSettings);
//        }
    }

    internal class ApiTestControllerActivator : IHttpControllerActivator
    {
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = (ApiController)Activator.CreateInstance(controllerType);
            return controller;
        }
    }
}