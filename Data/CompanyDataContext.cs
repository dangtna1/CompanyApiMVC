using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace companyApi.Data
{
    public class CompanyDataContext : DbContext
    {
        public CompanyDataContext(DbContextOptions<CompanyDataContext> options) : base(options)
        {
            
        }

        public DbSet<Company> Companies => Set<Company>();
    }
}