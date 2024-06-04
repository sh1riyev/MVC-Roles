using System;
using Front_To_Back_MVC.Models;
using Front_To_Back_MVC.ViewModels.Course;

namespace Front_To_Back_MVC.Services.Interface
{
	public interface ICourseService
	{
		Task<IEnumerable<CourseVM>> GetAll();
		Task<bool> IsExist(string name);
		Task Create(Course course);
	}
}

