using airline_backend.database.Entity;
using Microsoft.EntityFrameworkCore;

namespace airline_backend.models
{
    public class databaseController:DbContext
    {
        public DbSet<UserModel> users { get; set; }

        public DbSet<Ticket> ticket { get; set; }
        public DbSet<Flights> flights { get; set; }
        public DbSet<passenger> passengers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB; initial catalog=airlineDB;persist security info=True;");
        }
    }
}
