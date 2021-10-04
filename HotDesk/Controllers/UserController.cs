using HotDesk.Data;
using HotDesk.Models.AuthModels;
using HotDesk.Models.DbModels;
using HotDesk.Models.UserModel;
using HotDesk.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotDesk.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize(Roles = "User")]
    public class UserController : Controller
    {
        private readonly IUserService serv;
        public UserController(IUserService _serv)
        {
            serv = _serv;
        }

        [HttpGet]
        public async Task<IActionResult> WorkspacesView()
        {
            var model = await serv.GetWorkspaceViewModel();
            return PartialView("WorkspacesView", model);
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<JsonResult> Apply([FromBody] ApplyModel model)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst("Id");
            var userId = claim.Value;

            await serv.Apply(model, Convert.ToInt32(userId));



            Console.WriteLine(userId);

            return Json(PartialView(true));
        }


        [HttpGet]
        public async Task<IActionResult> Search()
        {
            SearchModel model = new SearchModel();

            model.DevicesByType = await serv.GetDevicesByType();

            return PartialView("Search", model);
        }

        [HttpPost]
        [Consumes("application/json")]

        public async Task<IActionResult> UseSearch([FromBody] SearchModel model)
        {
            var _model = await serv.GetWorkspaceSearch(model);

            return PartialView("WorkspacesView", _model);
        }

        [HttpGet]
        public async Task<IActionResult> MyWorkspacesView()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst("Id");
            var userId = claim.Value;

            var model = await serv.GetMyWorkspaceViewModel(Convert.ToInt32(userId));
            return PartialView("MyWorkspacesView", model);
        }
    }
}
