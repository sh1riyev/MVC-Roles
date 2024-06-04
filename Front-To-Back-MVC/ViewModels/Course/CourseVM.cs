using System;
namespace Front_To_Back_MVC.ViewModels.Course
{
	public class CourseVM
	{
		public int Id { get; set; }
		public string CourseName { get; set; }
		public string CategoryName { get; set; }
		public string MainImage { get; set; }
        public decimal Price { get; set; }
    }
}

