using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkstationManagment.Core.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            string connectionString = "server=localhost;port=3306;database=workstation_db;user=root;password=my-secret-pw;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}

