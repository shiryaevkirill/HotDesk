using HotDesk.Data;
using HotDesk.Models;
using HotDesk.Models.AuthModels;
using HotDesk.Models.DbModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotDesk.Controllers
{
   
    public class AuthorizationController : Controller
    {
        private readonly HotDeskContext context;

        public AuthorizationController(HotDeskContext _context)
        {
            context = _context;
        }

        [Route("[controller]/[action]")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult Index()
        {
            ViewData["role"] = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            ViewData["name"] = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MainPanel","Panel");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = await context.Employee
                    .FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (employee != null)
                {
                    await Authenticate(employee,context.Role.Find(employee.IdRole).RoleName); 

                    return RedirectToAction("MainPanel","Panel");
                }
                ModelState.AddModelError("Login", "Incorrect username or password");
            }
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {

            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login");
            }
            else return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            return View();
        }

        private async Task Authenticate(Employee employee, string NameRole)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, employee.Name+" "+employee.Surname),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, NameRole),
                new Claim("Id", value: Convert.ToString(employee.Id))
            };
            
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
