namespace Tests.User.Domain.Entities
{
    public sealed class User : Entity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public byte Age { get; set; }
    }
}
