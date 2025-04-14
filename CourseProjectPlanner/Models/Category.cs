using System.ComponentModel.DataAnnotations;

namespace CourseProjectPlanner.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
		public string Name { get; set; }
		public string RefersTo { get; set; }
		public string ImageName { get; set; }
    }
}
