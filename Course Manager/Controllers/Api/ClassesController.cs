using Course_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Course_Manager.Controllers.Api
{
    public class ClassesController : ApiController
    {
        ApplicationDbContext _context;
        // DELETE /api/courses/1
        [HttpDelete]
        public IHttpActionResult DeleteClass(int id)
        {
            Class Class = _context.Classes.SingleOrDefault(c => c.Id == id);

            if (Class == null)
                return BadRequest();

            _context.Classes.Remove(Class);

            _context.SaveChanges();

            return Ok();
        }

        public ClassesController()
        {
            _context = new ApplicationDbContext();
        }
    }
}
