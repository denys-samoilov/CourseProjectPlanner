using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProjectPlanner.Models
{
    public class Spend
    {
        [Key]
        public int SpendId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime SpendDate { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
		[ForeignKey("CategoryId")]
		public Category Categories { get; set; } = new Category();

		[ForeignKey("UserId")]
		public User Users { get; set; } = new User();

    }
}
