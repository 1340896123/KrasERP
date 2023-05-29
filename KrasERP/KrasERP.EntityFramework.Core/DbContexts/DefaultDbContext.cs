using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace KrasERP.EntityFramework.Core.DbContexts
{
    [AppDbContext("KrasERP", DbProvider.Npgsql)]
    public class DefaultDbContext : AppDbContext<DefaultDbContext>
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
            this.Database.MigrateAsync();
        }
    }
}