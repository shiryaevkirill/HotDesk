using HotDesk.Models.DbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Models.AdminModels
{
    public class AddWorkspaceModel
    {
        [Required(ErrorMessage = "Enter start date!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Start date")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "Enter start date!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("End date")]
        public string EndDate { get; set; }

        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public string Description { get; set; }

        public string DevicesId { get; set; }

        public ILookup<String, Device> DevicesByType { get; set; }
    }
}
