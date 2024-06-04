using System;
using Fiorello_PB101.Helpers.Extentions;
using Front_To_Back_MVC.Models;
using Front_To_Back_MVC.Services.Interface;
using Front_To_Back_MVC.ViewModels.Course;
using Microsoft.AspNetCore.Mvc;

namespace Front_To_Back_MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CourseController : Controller
	{
		private readonly ICourseService _service;
		private readonly ICategoryService _categoryService;
		private readonly IWebHostEnvironment _env;
		public CourseController(ICourseService service, ICategoryService categoryService,IWebHostEnvironment env)
		{
			_service = service;
			_categoryService = categoryService;
			_env = env;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _service.GetAll());
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			ViewBag.categories = await _categoryService.GetAllSelectList();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CourseCreateVM reguest)
		{
            ViewBag.categories = await _categoryService.GetAllSelectList();

            if (!ModelState.IsValid)
			{
				return View();
			}

			var existCourse = await _service.IsExist(reguest.CourseName);

			if (existCourse == true)
			{
				ModelState.AddModelError("CourseName", "This name is used");
				return View();
			}

			foreach (var item in reguest.NewImage)
			{
				if (!item.CheckFileSize(500))
				{
					ModelState.AddModelError("NewImage", "Image must be max 500KB");
					return View();
				}

				if (!item.CheckFileType("image/"))
				{
                    ModelState.AddModelError("NewImage", "Format must be image");
                    return View();
                }
			}

			List<CourseImage> images = new();

			foreach (var item in reguest.NewImage)
			{
				string fileName = $"{Guid.NewGuid()}-{item.FileName}";
				string path = _env.GenerateFilePath("images", fileName);
				await item.SaveFileToLocalAsync(path);
				images.Add(new CourseImage { Name=fileName});
			}

			Course course = new()
			{
				Name = reguest.CourseName,
				Description = reguest.Description,
				Price = decimal.Parse(reguest.Price),
				CategoryId = reguest.CategoryId,
				Images = images
			};

			await _service.Create(course);

			return RedirectToAction(nameof(Index));
        }
	}
}

