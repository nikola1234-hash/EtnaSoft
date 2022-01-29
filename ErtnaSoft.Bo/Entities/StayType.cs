namespace ErtnaSoft.Bo.Entities
{
    public class StayType : Entity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public bool IsSpecialType { get; set; }
    }
}
