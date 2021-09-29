//using HotDesk.Models.AuthModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using HotDesk.Models.DbModels;
//using HotDesk.Data;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Http;

//namespace HotDesk.Services
//{
//    public class AuthorizationService
//    {
//        private readonly HotDeskContext context;
//        public AuthorizationService(HotDeskContext _context)
//        {
//            context = _context;
//        }

//        public async Task<bool> Login(LoginModel model, ref ModelStateDictionary ModelState)
//        {
//            if (ModelState.IsValid)
//            {
//                Employee employee = await context.Employee
//                    .FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);

//                if (employee != null)
//                {
//                    await Authenticate(employee, context.Role.Find(employee.IdRole).RoleName); // аутентификация

//                    return true;
//                }
//                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
//            }
//            //return View(model);
//            return false;
//        }

//        private async Task Authenticate(Employee employee, string NameRole)
//        {
//            // создаем один claim
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimsIdentity.DefaultNameClaimType, employee.Login),
//                new Claim(ClaimsIdentity.DefaultRoleClaimType, NameRole)
//            };
//            // создаем объект ClaimsIdentity
//            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
//                ClaimsIdentity.DefaultRoleClaimType);
//            // установка аутентификационных куки
//            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
//        }
//    }
//}
