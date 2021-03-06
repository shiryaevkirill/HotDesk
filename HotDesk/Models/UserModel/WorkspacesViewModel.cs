using HotDesk.Models.DbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Models.UserModel
{
    public class WorkspacesViewModel
    {
        public int WorkspaceId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Start date")]
        public string StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("End date")]
        public string EndDate { get; set; }

        [DisplayName("Devices")]
        public List<Device> Devices { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
    }
}
