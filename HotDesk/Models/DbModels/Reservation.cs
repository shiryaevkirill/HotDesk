using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Models.DbModels
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public int IdWorker { get; set; }

        public int IdStatus { get; set; }


    }
}
