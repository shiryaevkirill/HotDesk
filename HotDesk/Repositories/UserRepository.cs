using HotDesk.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Repositories
{
    public class UserRepository
    {
        private readonly HotDeskContext context;

        public UserRepository(HotDeskContext _context)
        {
            context = _context;
        }




    }
}
