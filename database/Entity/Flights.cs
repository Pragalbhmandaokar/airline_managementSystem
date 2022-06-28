namespace airline_backend.database.Entity
{
    public class Flights
    {
        public int FlightsId { get; set; }
        public string FlightName { get; set; }
        public string ticketNumber { get; set; }
        public int avaliableNumber { get; set; }
        public string sourcePlace { get; set; }
        public string destinationPlace { get; set; }
        public DateTime dataOfJourney { get; set; }

        public int price { get; set; }
    }
}
