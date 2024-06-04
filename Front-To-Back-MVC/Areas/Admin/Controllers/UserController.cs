using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Front_To_Back_MVC.Models;
using Front_To_Back_MVC.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Front_To_Back_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="SuperAdmin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users =  _userManager.Users.ToList();

            List<UserRoleVM> usersRoles = new();

            foreach (var item in users)
            {
                var roles = await _userManager.GetRolesAsync(item);
                string userRoles = String.Join(",",roles.ToArray());

                usersRoles.Add(new UserRoleVM
                {
                    FullName = item.FullName,
                    Email = item.FullName,
                    Roles = userRoles
                });
            }

            return View();
        }

    }
}

