using HotDesk.Data;
using HotDesk.Models.AdminModels;
using HotDesk.Models.DbModels;
using HotDesk.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Services
{
    public class AdminService
    {
        private AdminRepository repos;
        HotDeskContext context;

        public AdminService(HotDeskContext _context)
        {
            repos = new AdminRepository(_context);
            context = _context;
        }

        public async Task<EmployeesEditor> GetEmployeesEditorModel()
        {
            var model = new EmployeesEditor();
            model.Employees = await repos.GetUsers();
            model.Roles = await repos.GetRoles();
            return model;
        }

        public async Task<string[]> GetRoleName()
        {
            List<Role> Roles = await repos.GetRoles();
            List<string> RoleName = new List<string>();
            foreach (Role role in Roles)
            {
                RoleName.Add(role.RoleName);
            }
            return RoleName.ToArray();
        }


        public async Task<List<Role>> GetRoles()
        {
            List<Role> Roles = await repos.GetRoles();
 
            return Roles;
        }

        public async Task<List<Device>> GetDevices()
        {
            List<Device> Device = await repos.GetDevices();

            return Device;
        }

        public async Task<List<WorkspaceEditorModel>> GetWorkspaceEditorModel()
        {
            List<Workplace> workspaces = await repos.GetWorkspaces();

            List<WorkspaceEditorModel> result = new List<WorkspaceEditorModel>();

            foreach (var workspace in workspaces)
            {
                WorkspaceEditorModel model = new WorkspaceEditorModel();
                model.StartDate = workspace.StartDate;
                model.EndDate = workspace.EndDate;
                model.Description = workspace.Description;
                model.Devices = new List<Device>();
                model.WorkspaceId = workspace.Id;
                model.OrderId = workspace.OrderId;

                if (workspace.OrderId != 0)
                {
                    Reservation reserv = await repos.GetReservationById(workspace.OrderId);

                    var employee = await repos.GetEmployeeById(reserv.IdWorker);
                    model.Employee = employee.Name + " " + employee.Surname;

                    var status = await repos.GetStatusById(reserv.IdStatus);
                    model.Status = status.StatusName;
                }
                if (workspace.DevicesId.Length > 1)
                {
                    string devicesId = workspace.DevicesId;
                    devicesId = devicesId.Remove(devicesId.Length - 1);
                    string[] ids = devicesId.Split(';');

                    foreach (var id in ids)
                    {
                        var device = await repos.GetDeviceById(Convert.ToInt32(id));
                        model.Devices.Add(device);
                    }
                }
                result.Add(model);
            }

            return result;
        }


        public async Task<bool> AddEmployee(AddEmployeeModel model)
        {


            Employee employee = new Employee
            {
                Name = model.Name,
                Surname = model.Surname,
                Login = model.Login,
                Password = model.Password
            };

            Role userRole = await repos.FindRole(model);
            if (userRole != null)
                employee.IdRole = userRole.Id;
            bool res = await repos.AddEmployee(employee);
            return res;
        }

        public async Task<bool> CheckLogin(string login)
        {
            var employee = await repos.CheckLogin(login);

            if (employee == null) return true;
            else return false;
        }

        public async Task<bool> CheckRoleName(string name)
        {
            var role = await repos.CheckRoleName(name);

            if (role == null) return true;
            else return false;
        }

        public async Task<bool> AddWorkspace(AddWorkspaceModel model)
        {


            Workplace workspace = new Workplace();

            workspace.StartDate = model.StartDate;
            workspace.EndDate = model.EndDate;
            workspace.Description = model.Description;
            workspace.DevicesId = model.DevicesId;

            bool res = await repos.AddWorkspace(workspace);

            return true;
        }


        public async Task<bool> AddRole(Role model)
        {

            Role role = new Role {RoleName = model.RoleName };
            bool res = await repos.AddRole(role);
            return res;
        }

        public async Task<bool> AddDevice(Device model)
        {

            Device device = new Device { DeviceName = model.DeviceName, DeviceType = model.DeviceType};
            bool res = await repos.AddDevice(device);
            return res;
        }

        public async Task<bool> Delete(int id, string Table)
        {

            if (Table == "Employee")
            {
                var item = await repos.GetEmployeeById(id);
                if (item != null)
                {
                    var res = await repos.DeleteEmployee(item);
                    return res;
                }
            }
            else if (Table == "Role")
            {
                var item = await repos.GetRoleById(id);
                if (item != null)
                {
                    var res = await repos.DeleteRole(item);
                    return res;
                }
            }else if (Table == "Device")
            {
                var item = await repos.GetDeviceById(id);
                if (item != null)
                {
                    var res = await repos.DeleteDevice(item);
                    return res;
                }
            }
            else if (Table == "Workspace")
            {
                var item = await repos.GetWorkspaceById(id);
                if (item != null)
                {
                    if (item.OrderId != 0)
                    {
                        var reservation = await repos.GetReservationById(item.OrderId);
                        await repos.DeleteReservation(reservation);
                    }

                        var res = await repos.DeleteWorkspace(item);

                    

                    return res;
                }
            }

            return false;
        }

        public async Task<ILookup<String, Device>> GetDevicesByType()
        {
            List<Device> devices = await repos.GetDevices();

            ILookup<String, Device> devicesByType = devices.ToLookup(d => d.DeviceType);
            return devicesByType;
        }

        public async Task<bool> ConfirmApplication(ConfirmApplicationModel model)
        {
            Reservation reserv = await repos.GetReservationById(Convert.ToInt32(model.OrderId));

            reserv.IdStatus = 2;

            await repos.UpdateReservation(reserv);

            return true;
        }
    }
}
