namespace CourseProjectPlanner.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
		public string Name { get; set; }
		public string Login { get; set; }
        public string Password { get; set; }
    }
}
