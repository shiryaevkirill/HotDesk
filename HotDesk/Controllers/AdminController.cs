using HotDesk.Data;
using HotDesk.Models.AdminModels;
using HotDesk.Models.DbModels;
using HotDesk.Services;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotDesk.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private AdminService serv;

        public AdminController(HotDeskContext _context)
        {
            serv = new AdminService(_context);
        }

        [HttpGet]
        public async Task<IActionResult> EmployeesEditor()
        {
            var model = await serv.GetEmployeesEditorModel();
            return PartialView("EmployeesEditor", model);
        }

        [HttpGet]
        public async Task<IActionResult> AddEmployee()
        {
            var model = new AddEmployeeModel();
            model.RoleName = await serv.GetRoleName();
            return PartialView("AddEmployee", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<JsonResult> AddEmployee([FromBody] AddEmployeeModel model)
        {
           
            bool result = await serv.AddEmployee(model);
            if (result)
            {
                return Json(true);
            }

            return Json(PartialView("AddEmployee", model));
        }

        public async Task<JsonResult> CheckLogin(string login)
        {
            var result = await serv.CheckLogin(login);
            return Json(result);
        }

        public async Task<JsonResult> CheckRoleName(string RoleName)
        {
            var result = await serv.CheckRoleName(RoleName);
            return Json(result);
        }

        [HttpGet]
        public IActionResult ConfirmDelete()
        {
            return PartialView("ConfirmDelete");
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<JsonResult> Delete([FromBody] DeleteModel _model)
        {

            int id = Convert.ToInt32(_model.Id);
            var res = await serv.Delete(id, _model.Table);

            return Json(res);
        }



        [HttpGet]
        public async Task<IActionResult> RolesEditor()
        {
            var model = await serv.GetRoles();
            return PartialView("RolesEditor", model);
        }


        [HttpGet]
        public IActionResult AddRole()
        {
            return PartialView("AddRole");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<JsonResult> AddRole([FromBody] Role model)
        {
            bool result = await serv.AddRole(model);
               
            return Json(result);
                
           
        }


        [HttpGet]
        public async Task<IActionResult> DevicesEditor()
        {
            var model = await serv.GetDevices();
            return PartialView("DevicesEditor", model);
        }


        [HttpGet]
        public IActionResult AddDevice()
        {
            return PartialView("AddDevice");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<JsonResult> AddDevice([FromBody] Device model)
        {
            bool result = await serv.AddDevice(model);
            return Json(result);


        }



        [HttpGet]
        public async Task<IActionResult> WorkspaceEditor()
        {
            var model = await serv.GetWorkspaceEditorModel();
            return PartialView("WorkspacesEditor",model);
        }

        [HttpGet]
        public async Task<IActionResult> AddWorkspace()
        {
            AddWorkspaceModel model = new AddWorkspaceModel();
            model.DevicesByType = await serv.GetDevicesByType();
            return PartialView("AddWorkspace", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<JsonResult> AddWorkspace([FromBody] AddWorkspaceModel model)
        {
            bool result = await serv.AddWorkspace(model);
            return Json(result);
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> ConfirmApplication([FromBody] ConfirmApplicationModel model)
        {
            bool result = await serv.ConfirmApplication(model);
            var _model = await serv.GetWorkspaceEditorModel();
            return PartialView("WorkspacesEditor", _model);
        }
    }
}
