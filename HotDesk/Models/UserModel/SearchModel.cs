using HotDesk.Models.DbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Models.UserModel
{
    public class SearchModel
    {
        [Required(ErrorMessage = "Enter start date!")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Start date")]
        public string StartDate { get; set; }


        [DisplayName("Devices")]
        public ILookup<String, Device> DevicesByType { get; set; }

        public string DevicesType { get; set; }
    }
}
