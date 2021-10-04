using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Data
{
    public class HotDeskContext : DbContext
    {
        public HotDeskContext(DbContextOptions<HotDeskContext> options) : base(options)
        {

        }


        public DbSet<HotDesk.Models.DbModels.Employee> Employee { get; set; }
        public DbSet<HotDesk.Models.DbModels.Role> Role { get; set; }

        public DbSet<HotDesk.Models.DbModels.Device> Device { get; set; }
        public DbSet<HotDesk.Models.DbModels.Workplace> Workplace { get; set; }
        public DbSet<HotDesk.Models.DbModels.Status> Status { get; set; }

        public DbSet<HotDesk.Models.DbModels.Reservation> Reservation { get; set; }
    }

    
}
