using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebApiExample.Models;

namespace WebApiExample.Controllers
{
    public class ProductController : ApiController
    {
        private List<Product> products = new List<Product>()
        {
             new Product { id=1,name = "cup", price=22M, qty = 3 },
             new Product { id=2,name = "paper", price=41.7M, qty = 2 },
             new Product { id=3,name = "big", price=49.8M, qty = 1}
        };

        //public IEnumerable<Product> Get()
        //{
        //    return products.ToList();
        //}

        public IHttpActionResult Get(int id)
        {
            var p = products.Where(q => q.id == id);
            if (p == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(p);
            }
        }

        public List<Student> Get()
        {
            var students = new List<Student>
            {
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };

            students[0].Enrollments.Add(new Enrollment() { Grade = Grade.A, Course = new Course { CourseID = 1, Title = "33" } });

            var j = Json(students);
            string json = new JavaScriptSerializer().Serialize(j.Content);
            var objs = new JavaScriptSerializer().Deserialize<List<Student>>(json);

            return students;
        }


    }
}
