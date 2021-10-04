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
        private readonly IAdminService serv;

        public AdminController(IAdminService _serv)
        {
            serv = _serv;
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
        public JsonResult AddEmployee([FromBody] AddEmployeeModel model)
        {

            serv.Add(model);



            return Json(true);

        }

        public JsonResult CheckLogin(string Login)
        {
            var result = serv.Check<Employee>(u => u.Login == Login);
            return Json(result);
        }

        public JsonResult CheckRoleName(string RoleName)
        {
            var result = serv.Check<Role>(u => u.RoleName == RoleName);
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
        public JsonResult Delete([FromBody] DeleteModel _model)
        {

            var Table = _model.Table;

            if (Table == "Employee")
            {
                serv.Delete<Employee>(Convert.ToInt32(_model.Id));
            }
            else if (Table == "Role")
            {
                serv.Delete<Role>(Convert.ToInt32(_model.Id));
            }
            else if (Table == "Device")
            {
                serv.Delete<Device>(Convert.ToInt32(_model.Id));
            }
            else if (Table == "Workspace")
            {
                serv.Delete<Workplace>(Convert.ToInt32(_model.Id));
            }

            return Json(true);
        }



        [HttpGet]
        public IActionResult RolesEditor()
        {
            var model = serv.GetAll<Role>(r => r.RoleName != "Admin");
            return PartialView("RolesEditor", model.ToList());
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
        public JsonResult AddRole([FromBody] Role model)
        {
            serv.Add(model);
               
            return Json(true);
                
           
        }


        [HttpGet]
        public async Task<IActionResult> DevicesEditor()
        {
            var model = await serv.GetAll<Device>();
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
        public JsonResult AddDevice([FromBody] Device model)
        {
            serv.Add(model);
            return Json(true);
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
        public JsonResult AddWorkspace([FromBody] AddWorkspaceModel model)
        {
            serv.Add(model);
            return Json(true);
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
