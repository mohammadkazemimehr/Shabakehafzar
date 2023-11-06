using Microsoft.EntityFrameworkCore;
using Shabakehafzar.Data;
using Shabakehafzar.Data.Context;

namespace Shabakehafzar.API.Helper.Extentions
{
    public static class WebHostExtention
    {
        public static WebApplication Seed(this WebApplication host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var databaseContext = serviceProvider.GetRequiredService<AppDataContext>();
                databaseContext.Database.Migrate();
            }

            return host;
        }
    }
}
