using Infrastructure.Identity.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Infrastructure.Identity
{
    public class IdentityContextBuilder : IDesignTimeDbContextFactory<IdentityContext>
    {
        public IdentityContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
            optionsBuilder.UseSqlite("Data Source=temp.db");

            return new IdentityContext(optionsBuilder.Options);
        }


    }
    }
