using MaccabiSearch.Domain.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace MaccabiSearch.Api.IoC
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDevelopment(this IApplicationBuilder app)
        {
            app.RunDbMibrations();
            return app;
        }

        private static IApplicationBuilder RunDbMibrations(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<MaccabiSearchDbContext>())
                {
                    db.Database.Migrate();
                }
            }
            return app;
        }
    }
}
