using GKMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKMS
{
    public class DatabaseContext : DbContext
    {
        public DbSet<KeyAllocation> KeyAllocations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=GKMS.db");
        }
    }
}
