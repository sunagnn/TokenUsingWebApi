using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using TokenUsingWebApi.Models;
using YamlDotNet.Serialization;

namespace TokenUsingWebApi.Controllers
{
    [System.Web.Http.Authorize]
    public class TokenUsingWebApiController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("~/api/token/getTest")]
        public List<Test> GetTest()
        {
            List<Test> list = new List<Test>();
            Test obj = new Test { jobOrderId = "fgxhbvn", order = 3, targetAmount = 343 };
            try
            {
                list.Add(obj);
                // stokList = GetPoriniStokList();
            }
            catch (Exception ex)
            {
            }

            return list;
        }

        //  [Authorize]
        [HttpGet]
        [Route("~/api/token/get")]
        public HttpResponseMessage Get(string format)
        {
            // Get products from database
            List<Test> products = new List<Test>();
            Test obj = new Test { jobOrderId = "fgxhbvn", order = 3, targetAmount = 343 };
            Test obj1 = new Test { jobOrderId = "11fgxhbvn", order = 13, targetAmount = 1343 };
            products.Add(obj);
            products.Add(obj1);
            // Check requested format
            switch (format.ToLower())
            {
                case "json":
                    // Return products in JSON format
                    return Request.CreateResponse(HttpStatusCode.OK, products, Configuration.Formatters.JsonFormatter);
                case "xml":
                    // Return products in XML format
                    return Request.CreateResponse(HttpStatusCode.OK, products, Configuration.Formatters.XmlFormatter);
                case "yaml":
                    // Return products in YAML format
                    var yaml = new SerializerBuilder().Build().Serialize(products);
                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(yaml, Encoding.UTF8, "application/yaml");
                    return response;
                default:
                    // Return products in JSON format as default
                    return Request.CreateResponse(HttpStatusCode.OK, products, Configuration.Formatters.JsonFormatter);
            }
        }

        // Other controller methods...


    }
}