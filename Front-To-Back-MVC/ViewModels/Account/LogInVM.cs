using System;
using System.ComponentModel.DataAnnotations;

namespace Front_To_Back_MVC.ViewModels.Account
{
	public class LogInVM
	{
		[Required]
		public string EmailOrUsename { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}

