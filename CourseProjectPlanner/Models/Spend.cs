namespace CourseProjectPlanner.Models
{
    public class Spend
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime SpendDate { get; set; }
        public string CategoryId { get; set; }

        
    }
}
