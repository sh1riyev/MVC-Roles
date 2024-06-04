using System;
using Front_To_Back_MVC.Models;
using Front_To_Back_MVC.Services.Interface;
using Front_To_Back_MVC.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Front_To_Back_MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoryController :Controller
	{
		private readonly ICategoryService _service;
		public CategoryController(ICategoryService service)
		{
			_service = service;
		}


		public async Task<IActionResult> Index()
		{
			return View(await _service.GetAllWithCourseCount());
		}

		[HttpGet]
		[Authorize(Roles ="SuperAdmin")]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CategoryCreateVM reguest)
		{
			if (!ModelState.IsValid) return View();

			bool existCategory = await _service.IsExist(reguest.Name);

			if (existCategory)
			{
				ModelState.AddModelError("Name", "This name is already used");
				return View();
			}

			await _service.Create(new Models.Category { Name = reguest.Name });
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id is null) return BadRequest();

			var category = await _service.GetById(id);

			if (category is null) return NotFound();

			return View(new CategoryEditVM { Name = category.Name });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int? id, CategoryEditVM reguest)
		{
            if (id is null) return BadRequest();

            var category = await _service.GetById(id);

            if (category is null) return NotFound();

            if (!ModelState.IsValid) return View();

            var dbCategories = await _service.GetAllCategories();

			if (dbCategories.Any(m => m.Name == reguest.Name && m.Id != category.Id && m.IsDeleted == false))
			{
				ModelState.AddModelError("Name", "Try another,this name is used");
				return View();
			}

			await _service.Edit(category, reguest);

            return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int ? id)
		{
            if (id is null) return BadRequest();

            var category = await _service.GetById(id);

            if (category is null) return NotFound();

			await _service.Delete(category);

			return RedirectToAction(nameof(Index));
        }

		[HttpGet]
		public async Task<IActionResult> Detail(int? id)
		{
            if (id is null) return BadRequest();

            var category = await _service.GetByIdWithCourse((int)id);

            if (category is null) return NotFound();

			List<CategoryCourseImageVM> imageVMs = new();

			foreach (var item in category.Courses)
			{
				foreach (var img in item.Images)
				{
					imageVMs.Add(new CategoryCourseImageVM
					{
						Image = img.Name,
						IsMain = img.IsMain
					});
				}
			}

			return View(new CategoryDetailVM
			{
                Id = category.Id,
                CategoryName = category.Name,
                CreateDate = category.CreateDate.ToString("dd-mm-yyyy"),
                CourseNames = category.Courses.Where(m => m.CategoryId == id).Select(m => m.Name).ToList(),
                Images = imageVMs

            });
        }
	}
}

