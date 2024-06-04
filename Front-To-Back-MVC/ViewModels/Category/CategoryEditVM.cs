using System;
using System.ComponentModel.DataAnnotations;

namespace Front_To_Back_MVC.ViewModels.Category
{
	public class CategoryEditVM
	{
		[Required(ErrorMessage ="This input cannot be empty")]
		[StringLength(15)]
		public string Name { get; set; }
	}
}

