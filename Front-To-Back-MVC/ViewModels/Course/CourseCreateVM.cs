using System;
namespace Front_To_Back_MVC.ViewModels.Course
{
	public class CourseCreateVM
	{
		public string CourseName { get; set; }
		public int CategoryId { get; set; }
		public string Description { get; set; }
		public string Price { get; set; }
		public List<IFormFile> NewImage { get; set; }
	}
}

