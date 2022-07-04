namespace airline_backend.database.Entity
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string ticketNumber { get; set; }
        public string flyingFrom { get; set; }
        public string flyingTo { get; set; }
        public int price { get; set; }
        public string userName { get; set; }
    }
}
