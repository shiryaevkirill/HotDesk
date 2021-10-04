using HotDesk.Models.DbModels;
using HotDesk.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Services
{
    public interface IUserService
    {

        Task<List<WorkspacesViewModel>> GetWorkspaceViewModel();
        Task<bool> Apply(ApplyModel model, int userId);
        Task<ILookup<String, Device>> GetDevicesByType();
        Task<List<WorkspacesViewModel>> GetWorkspaceSearch(SearchModel search);
        Task<List<MyWorkspacesViewModel>> GetMyWorkspaceViewModel(int userId);
    }
}
