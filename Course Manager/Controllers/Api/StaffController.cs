using Course_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Course_Manager.Controllers.Api
{
    public class StaffController : ApiController
    {
        ApplicationDbContext _context;

        [HttpDelete]
        public IHttpActionResult DeleteStaffMember(int id)
        {
            StaffMember staffMember = _context.Staff.SingleOrDefault(s => s.Id == id);

            if (staffMember == null)
                return BadRequest();
            
            _context.Staff.Remove(staffMember);

            _context.SaveChanges();

            return Ok();
        }

        public StaffController()
        {
            _context = new ApplicationDbContext();
        }
    }
}
