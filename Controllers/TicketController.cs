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
        private readonly IConfiguration _configuration;
        public TicketController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from [ticket]";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("airlineAppCon");
            SqlDataReader myReader;
            using (SqlConnection myconn = new SqlConnection(sqlDataSource))
            {
                myconn.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myconn))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myconn.Close();
                }
            }
            return new JsonResult(table);
        }
    }
}
