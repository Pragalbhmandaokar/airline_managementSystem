using airline_backend.database.Entity;
using airline_backend.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace airline_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        databaseController db;
        public TicketController()
        {
            db = new databaseController();
        }

        [HttpGet]
        public IEnumerable<Ticket> Get()
        {
            return db.ticket.ToList();
        }

        [HttpPost("addTicket")]
        public async Task<ActionResult<Ticket>> addTickets(Ticket request)
        {
            try {
                db.ticket.Add(request);
                db.SaveChanges();
                return Ok("Ticket generated : " + request.TicketId);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("searchTicketByUserId")]
        public async Task<ActionResult<Ticket>> searchTicket(string userId)
        {
            try {
               var allTickets =  db.ticket.Where(p => p.userName == userId);
               return Ok(allTickets);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("deleteTicket")]
        public async Task<ActionResult<Ticket>> deleteTicket(int ticketId)
        {
            var allTickets = db.ticket.FirstOrDefault(p => p.TicketId == ticketId);
            try
            {
                if(allTickets == null)
                {
                    return Ok("No ticket found");
                }
                db.ticket.Remove(allTickets);
                db.SaveChanges();
                return Ok("Ticket removed");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
