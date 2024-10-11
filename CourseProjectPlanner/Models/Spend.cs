namespace CourseProjectPlanner.Models
{
    public class Spend
    {
        [Key]
        public int SpendId { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime SpendDate { get; set; }
        public string CategoryId { get; set; }
        public string UserId { get; set; }

    }
}
