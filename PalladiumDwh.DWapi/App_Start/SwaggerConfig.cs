using System.Web.Http;
using WebActivatorEx;
using PalladiumDwh.DWapi;
using Swashbuckle.Application;
using System;
using System.Xml.XPath;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace PalladiumDwh.DWapi
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "DWapi");
                    c.PrettyPrint();
                    c.IncludeXmlComments(string.Format(@"{0}\bin\PalladiumDwh.DWapi.xml",
                        System.AppDomain.CurrentDomain.BaseDirectory));
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle("NDWH API");
                    c.InjectJavaScript(thisAssembly, "PalladiumDwh.DWapi.Swagger.js");
                    //c.InjectStylesheet(thisAssembly, "PalladiumDwh.DWapi.Swagger.css");
                });
        }
    }
}