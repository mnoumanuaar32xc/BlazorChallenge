using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCRUD.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public AppDBContext()
        {

        }
        public DbSet<Students> students { get; set; }
        public DbSet<Countries> countries { get; set; }
        public DbSet<Classes> classes { get; set; }

       
    }
}
