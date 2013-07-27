using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace WebApi.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("Order", "order/{id}/{action}", 
                new { controller ="Order", action = "Index"});
                        
            config.Routes.MapHttpRoute("Orders", "orders/", 
                new { controller ="Orders"});

            ConfigJsonSerializer(config);
        }

        static void ConfigJsonSerializer(HttpConfiguration config)
        {
            var jsonSerializerSettings = config.Formatters.JsonFormatter.SerializerSettings;

            jsonSerializerSettings.Converters.Add(new StringEnumConverter());

            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

#if DEBUG
            jsonSerializerSettings.Formatting = Formatting.Indented;
#endif
        }
    }
}
