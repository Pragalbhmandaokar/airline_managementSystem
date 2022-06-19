namespace airline_backend.models
{
    public class Login
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set;  }

    }
}
