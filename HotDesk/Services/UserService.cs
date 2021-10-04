using HotDesk.Data;
using HotDesk.Models.DbModels;
using HotDesk.Models.UserModel;
using HotDesk.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository repos;

        public UserService(IRepository _repos)
        {
            repos = _repos;
        }

        public async Task<List<WorkspacesViewModel>> GetWorkspaceViewModel()
        {
            var w = await repos.GetAll<Workplace>();
            List <Workplace> workspaces = w.ToList();

            List<WorkspacesViewModel> result = new List<WorkspacesViewModel>();

            foreach (var workspace in workspaces)
            {
                if (workspace.OrderId != 0) continue;

                WorkspacesViewModel model = new WorkspacesViewModel();
                model.StartDate = workspace.StartDate;
                model.EndDate = workspace.EndDate;
                model.Description = workspace.Description;
                model.Devices = new List<Device>();
                model.WorkspaceId = workspace.Id;

                if (workspace.DevicesId.Length > 1)
                {
                    string devicesId = workspace.DevicesId;
                    devicesId = devicesId.Remove(devicesId.Length - 1);
                    string[] ids = devicesId.Split(';');

                    foreach (var id in ids)
                    {
                        var device = await repos.GetById<Device>(Convert.ToInt32(id));
                        model.Devices.Add(device);
                    }
                }
                result.Add(model);
            }

            return result;
        }


        public async Task<bool> Apply(ApplyModel model, int userId)
        {
            Reservation reserv = new Reservation();
            reserv.IdWorker = userId;
            reserv.IdStatus = 1;

            var reservationId = await repos.AddReservation(reserv);

            Workplace workplace = await repos.GetById<Workplace>(model.WorkspaceId);
            workplace.OrderId = reservationId;

            repos.Update(workplace);
            return true;
        }

        public async Task<ILookup<String, Device>> GetDevicesByType()
        {
            var d = await repos.GetAll<Device>();
            List<Device> devices = d.ToList();

            ILookup<String, Device> devicesByType = devices.ToLookup(d => d.DeviceType);
            return devicesByType;
        }


        public async Task<List<WorkspacesViewModel>> GetWorkspaceSearch(SearchModel search)
        {
            var w = await repos.GetAll<Workplace>();
            List<Workplace> workspaces = w.ToList();

            List<WorkspacesViewModel> result = new List<WorkspacesViewModel>();

            string[] search_types = null;
            if (search.DevicesType != null && search.DevicesType != "")
            {
                string searchType = search.DevicesType;
                searchType = searchType.Remove(searchType.Length - 1);
                search_types = searchType.Split(';');
            }

            foreach (var workspace in workspaces)
            {
                WorkspacesViewModel model = new WorkspacesViewModel();
                model.StartDate = workspace.StartDate;
                model.EndDate = workspace.EndDate;
                model.Description = workspace.Description;
                model.Devices = new List<Device>();
                model.WorkspaceId = workspace.Id;

                if (workspace.OrderId != 0) continue;

                if (workspace.DevicesId.Length > 1)
                {
                    string devicesId = workspace.DevicesId;
                    devicesId = devicesId.Remove(devicesId.Length - 1);
                    string[] ids = devicesId.Split(';');

                    foreach (var id in ids)
                    {
                        var device = await repos.GetById<Device>(Convert.ToInt32(id));
                        model.Devices.Add(device);
                    }
                }

                if (search.StartDate != "" && model.StartDate != search.StartDate) continue;

                bool check_one;
                bool check_all = true;
                if (search_types != null)
                {
                    foreach (var type in search_types)
                    {
                        check_one = false;
                        foreach (var device in model.Devices)
                        {
                            if (device.DeviceType == type) check_one = true;
                        }

                        if (!check_one) check_all = false;
                    }
                }
                if (!check_all) continue;

                result.Add(model);
            }

            return result;
        }


        public async Task<List<MyWorkspacesViewModel>> GetMyWorkspaceViewModel(int userId)
        {
            var w = await repos.GetAll<Workplace>();
            List<Workplace> workspaces = w.ToList();

            List<MyWorkspacesViewModel> result = new List<MyWorkspacesViewModel>();

            foreach (var workspace in workspaces)
            {
                if (workspace.OrderId == 0) continue;
                Reservation reserv = await repos.GetById<Reservation>(workspace.OrderId);

                if (reserv.IdWorker != userId) continue;


                MyWorkspacesViewModel model = new MyWorkspacesViewModel();
                model.StartDate = workspace.StartDate;
                model.EndDate = workspace.EndDate;
                model.Description = workspace.Description;
                model.Devices = new List<Device>();
                var status = await repos.GetById<Status>(reserv.IdStatus);
                model.Status = status.StatusName;

                if (workspace.DevicesId.Length > 1)
                {
                    string devicesId = workspace.DevicesId;
                    devicesId = devicesId.Remove(devicesId.Length - 1);
                    string[] ids = devicesId.Split(';');

                    foreach (var id in ids)
                    {
                        var device = await repos.GetById<Device>(Convert.ToInt32(id));
                        model.Devices.Add(device);
                    }
                }
                result.Add(model);
            }

            return result;
        }
    }
}
