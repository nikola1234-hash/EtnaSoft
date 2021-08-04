namespace EtnaSoft.Bo.Entities
{
    public class User : Audit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }

    }
}
