using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotDesk.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize(Roles = "Admin, User")]
    public class PanelController : Controller
    {
        public IActionResult MainPanel()
        {
            ViewData["name"] = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;
            return View();
        }
    }
}
