using System;
using System.ComponentModel.DataAnnotations;

namespace Front_To_Back_MVC.ViewModels.Account
{
	public class RegisterVM
	{
		[Required]
		public string FullName { get; set; }
		[Required]
		public string Username { get; set; }
		[Required]
		[EmailAddress(ErrorMessage ="Email is invaild")]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
		[Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
	}
}

