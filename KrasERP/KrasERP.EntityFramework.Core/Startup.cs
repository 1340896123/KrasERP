using Furion;
using KrasERP.EntityFramework.Core.DbContexts;
using Microsoft.Extensions.DependencyInjection;

namespace KrasERP.EntityFramework.Core
{
    public class Startup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
                options.AddDbPool<DefaultDbContext>();
            }, "KrasERP.Database.Migrations");
        }
    }
}