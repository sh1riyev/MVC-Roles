using System;
namespace Front_To_Back_MVC.Models
{
	public class CourseImage
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}

