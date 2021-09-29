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

        public AdminService(HotDeskContext _context)
        {
            repos = new AdminRepository(_context);
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

        public async Task<bool> AddEmployee(AddEmployeeModel model)
        {
            
            Employee employee = await repos.CheckCoincidence(model);
            if (employee == null)
            {
                employee = new Employee
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
                return true;
            }
            return false;
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
            if (Table == "Employee") {
                var item = await repos.GetEmployeeById(id);
                if (item != null)
                {
                    var res = await repos.DeleteEmployee(item);
                    return res;
                }
            }else if (Table == "Role")
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

            return false;
        }
    }
}
