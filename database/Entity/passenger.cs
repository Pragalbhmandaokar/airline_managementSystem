namespace airline_backend.database.Entity
{
    public class passenger
    {
        public int passengerId { get; set; }
        public int userId { get; set; }
        public string passengerName { get; set; }
        public string ticketNumberForFlight { get; set; }
        public string panNumber { get; set; }

        public string userName { get; set; } 
    }
}
