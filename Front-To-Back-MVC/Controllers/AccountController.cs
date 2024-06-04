using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Front_To_Back_MVC.Helpers.Enums;
using Front_To_Back_MVC.Models;
using Front_To_Back_MVC.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Front_To_Back_MVC.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManage;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManage,
            SignInManager<AppUser> signinManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManage = userManage;
            _signinManager = signinManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RegisterVM reguest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = new()
            {
                FullName = reguest.FullName,
                Email = reguest.Email,
                UserName = reguest.Username
            };

            IdentityResult result = await _userManage.CreateAsync(user, reguest.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                return View();
            }

            await _userManage.AddToRoleAsync(user, nameof(Roles.SuperAdmin));

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LogInVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser existUser = await _userManage.FindByEmailAsync(request.EmailOrUsename) ?? await _userManage.FindByEmailAsync(request.EmailOrUsename);

            if(existUser == null)
            {
                ModelState.AddModelError(string.Empty, "Login Failed");
                return View();
            }

            var result = await _signinManager.PasswordSignInAsync(existUser, request.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Login Failed");
                return View();
            }

            return RedirectToAction("Home", "Index");
        }

        [HttpGet]
        public async Task<IActionResult> CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if(await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }

            return Ok();
        }
    }
}
