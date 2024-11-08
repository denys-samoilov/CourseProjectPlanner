namespace CourseProjectPlanner.Models
{
    public class Spend
    {
        [Key]
        public int SpendId { get; set; }
		[RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Дане поле має бути числом з двома десятковими знаками")]
		[Required(ErrorMessage = "Дане поле обов'язкове")]
		public decimal Price { get; set; }
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime SpendDate { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }

    }
}
