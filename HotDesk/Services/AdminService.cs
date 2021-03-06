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
    public class AdminService : IAdminService
    {
        private readonly IRepository repos;

        public AdminService(IRepository _repos)
        {
            repos = _repos;
        }


        public T Get<T>(Func<T, bool> predicate) where T : class
        {
            return repos.Get(predicate);
        }

        public async Task<List<T>> GetAll<T>() where T : class
        {
            var result = await repos.GetAll<T>();
            return result.ToList();
        }

        public List<T> GetAll<T>(Func<T, bool> predicate) where T : class
        {
            return repos.GetAll<T>(predicate).ToList();
        }

        public void Add<T>(T item) where T : class
        {
            repos.Add(item);
            repos.SaveChanges();
        }

        public void Delete<T>(int id) where T : class
        {
            T item = repos.GetById<T>(id);

            repos.Remove(item);
        }


        public async Task<EmployeesEditor> GetEmployeesEditorModel()
        {
            var model = new EmployeesEditor();
            var adminId = repos.Get<Role>(r => r.RoleName == "Admin");
            var users = repos.GetAll<Employee>(e => e.IdRole != adminId.Id);
            model.Employees = users.ToList();
            var roles = await repos.GetAll<Role>();
            model.Roles = roles.ToList();
            return model;
        }

        public async Task<string[]> GetRoleName()
        {
            var roles = await repos.GetAll<Role>();
            List<Role> Roles = roles.ToList();
            List<string> RoleName = new List<string>();
            foreach (Role role in Roles)
            {
                RoleName.Add(role.RoleName);
            }
            return RoleName.ToArray();
        }


        public async Task<List<WorkspaceEditorModel>> GetWorkspaceEditorModel()
        {
            var wp = await repos.GetAll<Workplace>();
            List<Workplace> workspaces = wp.ToList();
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
                    Reservation reserv = repos.GetById<Reservation>(workspace.OrderId);

                    var employee = repos.GetById<Employee>(reserv.IdWorker);
                    model.Employee = employee.Name + " " + employee.Surname;

                    var status = repos.GetById<Status>(reserv.IdStatus);
                    model.Status = status.StatusName;
                }
                if (workspace.DevicesId.Length > 1)
                {
                    string devicesId = workspace.DevicesId;
                    devicesId = devicesId.Remove(devicesId.Length - 1);
                    string[] ids = devicesId.Split(';');

                    foreach (var id in ids)
                    {
                        var device = repos.GetById<Device>(Convert.ToInt32(id));
                        model.Devices.Add(device);
                    }
                }
                result.Add(model);
            }

            return result;
        }


        public void AddEmployee(AddEmployeeModel model)
        {
            Employee employee = new Employee
            {
                Name = model.Name,
                Surname = model.Surname,
                Login = model.Login,
                Password = model.Password
            };

            Role userRole = repos.Get<Role>(r => r.RoleName == model.Role);
            if (userRole != null)
                employee.IdRole = userRole.Id;
            repos.Add(employee);
            repos.SaveChanges();
        }


        

        public bool Check<T>(Func<T, bool> predicate) where T:class
        {
            var result = repos.Check<T>(predicate);

            if (result == null) return true;
            else return false;
        }

        public void AddWorkspace(AddWorkspaceModel model)
        {

            Workplace workspace = new Workplace();

            workspace.StartDate = model.StartDate;
            workspace.EndDate = model.EndDate;
            workspace.Description = model.Description;
            workspace.DevicesId = model.DevicesId;

            repos.Add(workspace);
            repos.SaveChanges();
        }


   

        public async Task<ILookup<String, Device>> GetDevicesByType()
        {
            var d = await repos.GetAll<Device>();
            List<Device> devices = d.ToList();

            ILookup<String, Device> devicesByType = devices.ToLookup(d => d.DeviceType);
            return devicesByType;
        }

        public async Task<bool> ConfirmApplication(ConfirmApplicationModel model)
        {
            Reservation reserv = repos.GetById<Reservation>(Convert.ToInt32(model.OrderId));

            reserv.IdStatus = 2;
            await repos.Update(reserv);
            await repos.SaveChanges();
            return true;
        }
    }
}
