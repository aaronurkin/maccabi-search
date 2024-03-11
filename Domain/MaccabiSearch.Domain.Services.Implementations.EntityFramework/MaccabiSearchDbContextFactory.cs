using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MaccabiSearch.Domain.Services.Implementations
{
    public class MaccabiSearchDbContextFactory : IDesignTimeDbContextFactory<MaccabiSearchDbContext>
    {
        public MaccabiSearchDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MaccabiSearchDbContext>();
            optionsBuilder.UseNpgsql(args[0]);

            return new MaccabiSearchDbContext(optionsBuilder.Options);
        }
    }
}
