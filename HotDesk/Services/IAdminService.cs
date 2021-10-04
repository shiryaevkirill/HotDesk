using HotDesk.Models.AdminModels;
using HotDesk.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Services
{
    public interface IAdminService
    {
        T Get<T>(Func<T, bool> predicate) where T : class;

        Task<List<T>> GetAll<T>() where T : class;
        List<T> GetAll<T>(Func<T, bool> predicate) where T : class;
        void Add<T>(T item) where T : class;
        void Delete<T>(int id) where T : class;
        Task<EmployeesEditor> GetEmployeesEditorModel();
        Task<string[]> GetRoleName();
        Task<List<WorkspaceEditorModel>> GetWorkspaceEditorModel();
        void AddEmployee(AddEmployeeModel model);

        bool Check<T>(Func<T, bool> predicate) where T : class;
        void AddWorkspace(AddWorkspaceModel model);
        Task<ILookup<String, Device>> GetDevicesByType();
        Task<bool> ConfirmApplication(ConfirmApplicationModel model);
    }
}
