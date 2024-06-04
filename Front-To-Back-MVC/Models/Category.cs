using System;
namespace Front_To_Back_MVC.Models
{
	public class Category : BaseEntity
	{
		public string Name { get; set; }
		public ICollection<Course> Courses { get; set; }
	}
}

