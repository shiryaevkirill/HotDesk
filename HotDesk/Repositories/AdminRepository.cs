using HotDesk.Data;
using HotDesk.Models.AdminModels;
using HotDesk.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Repositories
{

    public class AdminRepository
    {
        private readonly HotDeskContext context;

        public AdminRepository(HotDeskContext _context)
        {
            context = _context;
        }

        public async Task<List<Employee>> GetUsers()
        {
            var adminId = await context.Role.Where(r => r.RoleName == "Admin").FirstOrDefaultAsync();
            var users = await context.Employee.Where(e => e.IdRole != adminId.Id).ToListAsync();
            return users;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var user = await context.Employee.FirstOrDefaultAsync(p => p.Id == id);
            return user;
        }

        public async Task<Role> GetRoleById(int id)
        {
            var role = await context.Role.FirstOrDefaultAsync(p => p.Id == id);
            return role;
        }

        public async Task<Device> GetDeviceById(int id)
        {
            var role = await context.Device.FirstOrDefaultAsync(p => p.Id == id);
            return role;
        }

        public async Task<List<Role>> GetRoles(){
            var roles = await context.Role.Where(r => r.RoleName != "Admin").ToListAsync();
            return roles;
        }

        public async Task<List<Device>> GetDevices()
        {
            var devices = await context.Device.ToListAsync();
            return devices;
        }

        public async Task<Employee> CheckCoincidence(AddEmployeeModel model)
        {
            Employee employee = await context.Employee.FirstOrDefaultAsync(u => u.Login == model.Login);
            return employee;
        }

        public async Task<Role> FindRole(AddEmployeeModel model)
        {
            Role userRole = await context.Role.FirstOrDefaultAsync(r => r.RoleName == model.Role);
            return userRole;
        }

        public async Task<bool> AddEmployee(Employee employee)
        {
            context.Employee.Add(employee);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEmployee(Employee employee)
        {
            context.Employee.Remove(employee);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRole(Role role)
        {
            context.Role.Remove(role);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDevice(Device device)
        {
            context.Device.Remove(device);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddRole(Role role)
        {
            context.Role.Add(role);
            await context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> AddDevice(Device device)
        {
            context.Device.Add(device);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
