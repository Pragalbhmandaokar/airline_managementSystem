namespace airline_backend.database.Entity
{
    public class Tickets
    {
        public int TicketsId { get; set; }
        public string passengerId { get; set; }
        public string flightId { get; set; }
        public string seatNumber { get; set; }
    }
}
