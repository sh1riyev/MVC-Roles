using System;
using Front_To_Back_MVC.Data;
using Front_To_Back_MVC.Models;
using Front_To_Back_MVC.Services.Interface;
using Front_To_Back_MVC.ViewModels.Course;
using Microsoft.EntityFrameworkCore;

namespace Front_To_Back_MVC.Services
{
	public class CourseService :ICourseService
	{
        private readonly AppDbContext _context;
		public CourseService(AppDbContext context)
		{
            _context = context;
		}

        public async Task Create(Course course)
        {
           await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CourseVM>> GetAll()
        {
            IEnumerable<Course> courses = await _context.Courses.Include(m => m.Category).Include(m => m.Images).ToListAsync();

            return courses.Select(m => new CourseVM
            {
                Id = m.Id,
                CourseName = m.Name,
                CategoryName = m.Category.Name,
                Price = m.Price,
                MainImage = m.Images.FirstOrDefault(m => m.IsMain == true).ToString()
            }) ; 
        }

        public async Task<bool> IsExist(string name)
        {
            return await _context.Courses.AnyAsync(m => m.Name == name && m.IsDeleted == true);
        }
    }
}

