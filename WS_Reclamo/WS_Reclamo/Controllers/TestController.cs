using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WS_Reclamo.Models;

namespace WS_Reclamo.Controllers
{
    public class TestController : ApiController
    {
        Test[] products = new Test[]
        {
            new Test{Id=1, Name="Manzanas", Category="Frutas", Price=20},
            new Test{Id=1, Name="Peras", Category="Frutas", Price=80},
            new Test{Id=1, Name="Uvas", Category="Frutas", Price=00},
            new Test{Id=1, Name="Chayote", Category="Verduras", Price=10},
 
        };

        public IEnumerable<Test> GetAllProducts()
        {

            return products;
        }
        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

    }
}
