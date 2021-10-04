using HotDesk.Controllers;
using HotDesk.Data;
using HotDesk.Models.AdminModels;
using HotDesk.Models.DbModels;
using HotDesk.Services;
using HotDesk.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotDeskTest
{
    public class AdminServiceTest
    {
        private readonly AdminService serv;
        private readonly Mock<IRepository> repos = new Mock<IRepository>();

        public AdminServiceTest(){
            serv = new AdminService(repos.Object);
        }

        [Fact]
        public void Get_By_Predicate_Tet()
        {
            // Arrange
            Func<Employee, bool> predicate = u => u.Id == 1;

            var expectedEmployee = new Employee
            {
                Id = 1,
                Name = "testName",
                Surname = "testSurname",
                Login = "testLogin",
                Password = "testPassword"
            };

            repos.Setup(x => x.Get(predicate)).Returns(expectedEmployee);
            // Act
            var actualEmployee = serv.Get(predicate);
            // Assert
            Assert.Equal(expectedEmployee, actualEmployee);

        }
        
        [Fact]
        public void Get_All_Entities_Test()
        {
            Func<Device, bool> predicate = u => u.DeviceType == "mouse";

            repos.Setup(x => x.GetAll(predicate)).Returns(new Device[]
            {
                new Device {DeviceType = "mouse"},
                new Device {DeviceType = "mouse"},
                new Device {DeviceType = "mouse"},
            }.AsQueryable());

            // Act
            var actual = serv.GetAll(predicate);

            // Assert
            Assert.Equal(3, actual.Count());
        }

        [Fact]
        public void Add_Entities_Test()
        {
            Device item = new Device { DeviceType = "mouse" };

            repos.Setup(x => x.Add(item));

            // Act
            serv.Add(item);

            // Assert
            repos.Verify(r => r.Add(item));
        }

        [Fact]
        public void Delete_Entities_Test()
        {
            Device item = new Device { Id = 1 };

            repos.Setup(x => x.GetById<Device>(1)).Returns(Task.FromResult(item));
            repos.Setup(x => x.Remove(item));
            // Act
            serv.Delete<Device>(1);

            // Assert
            repos.Verify(r => r.Remove(item));
        }
        [Fact]
        public async void Get_Employees_Editor_Model_()
        {
            var id = 1;

            Func<Role, bool> get_role_predicate = r => r.RoleName == "Admin";
            Func<Employee, bool> get_all_employee_predicate = e => e.IdRole != 1;

            repos.Setup(x => x.Get(get_role_predicate)).Returns(new Role { Id = 1, RoleName = "Admin" });
            repos.Setup(x => x.GetAll(get_all_employee_predicate)).Returns(new Employee[]
            {
                new Employee{Id=1,IdRole=2}
            }.AsQueryable());
            repos.Setup(x => x.GetAll<Role>()).Returns(Task.FromResult(new Role[]
            {
                new Role{Id=1, RoleName="Admin"},
                new Role{Id=2, RoleName="User"}
            }.AsQueryable()));

            EmployeesEditor expected = new EmployeesEditor
            {
                Employees = new Employee[0].ToList(),
                Roles = new Role[] { new Role { Id = 1, RoleName = "Admin" }, new Role { Id = 2, RoleName = "User" } }.ToList()
        };

            // Act
            var result = await serv.GetEmployeesEditorModel();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
