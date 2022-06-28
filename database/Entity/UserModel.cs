namespace airline_backend.database.Entity
{
    public class UserModel
    {
        public int UserModelId { get; set; }
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
