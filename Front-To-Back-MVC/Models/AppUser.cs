using System;
using Microsoft.AspNetCore.Identity;

namespace Front_To_Back_MVC.Models
{
	public class AppUser :IdentityUser
	{
		public string FullName { get; set; }
	}
}

