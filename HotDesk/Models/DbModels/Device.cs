using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Models.DbModels
{
    public class Device
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter device name!")]
        [DisplayName("Device name")]
        public string DeviceName { get; set; }


        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter device type!")]
        [DisplayName("Device type")]
        public string DeviceType { get; set; }

        //public int UsedInId { get; set; }
    }
}
