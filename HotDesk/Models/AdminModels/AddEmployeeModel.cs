using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Models.AdminModels
{
    public class AddEmployeeModel
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter your name!")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter your surname!")]
        [DisplayName("Surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Enter username!")]
        [DisplayName("Username")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Enter password!")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }


        [DisplayName("Role")]
        public string Role { get; set; }

        public string[] RoleName { get; set; }
    }
}
