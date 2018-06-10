using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiExample.Models;

namespace WebApiExample.Controllers
{
    public class ProductController : ApiController
    {
        private List<Product> products = new List<Product>()
        {
             new Product { id=1,name = "cup", price=22M, qty = 3 },
             new Product { id=2,name = "paper", price=41.7M, qty = 3 }
        };

        public IEnumerable<Product> Get()
        {
            return products.ToList();
        }

        public IHttpActionResult Get(int id)
        {
            var p =  products.Where(q => q.id == id);
            if (p == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(p);
            }
        }


    }
}
