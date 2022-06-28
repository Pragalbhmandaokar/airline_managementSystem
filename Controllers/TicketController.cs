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
    }
}
