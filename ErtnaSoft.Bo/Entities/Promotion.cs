
namespace ErtnaSoft.Bo.Entities
{
    public class Promotion
    {
        public int Id { get; set; }
        public int StayTypeId { get; set; }
        //Number of days promotion include in stay
        public int StayDays  { get; set; }

        //Extra Charge for children
        public decimal ChildPrice { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
