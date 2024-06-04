using System;
using Front_To_Back_MVC.Models;
using Front_To_Back_MVC.ViewModels.Category;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Front_To_Back_MVC.Services.Interface
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryCourseVM>> GetAllWithCourseCount();
		Task Create(Category category);
		Task Delete(Category category);
		Task Edit(Category category, CategoryEditVM reguest);
		Task<Category> GetById(int? id);
		Task<bool> IsExist(string name);
		Task<Category> GetByIdWithCourse(int id);
		Task<IEnumerable<Category>> GetAllCategories();
		Task<SelectList> GetAllSelectList();
    }
}

