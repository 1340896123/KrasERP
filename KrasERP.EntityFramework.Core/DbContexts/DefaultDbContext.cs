using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.OpenApi.Extensions;
using System.Linq;

namespace KrasERP.EntityFramework.Core.DbContexts
{
    [AppDbContext("KrasERP", DbProvider.Npgsql)]
    public class DefaultDbContext : AppDbContext<DefaultDbContext>
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
            /// this.Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // 将表名设置为小写
                entity.SetTableName(entity.GetTableName().ToLower());

                foreach (var property in entity.GetProperties())
                {
                    // 将列名设置为小写
                    property.SetColumnName(property.GetColumnName().ToLower());
                }

                foreach (var key in entity.GetKeys())
                {
                    // 将主键名称设置为小写
                    key.SetName(key.GetName().ToLower());
                }

                foreach (var foreignKey in entity.GetForeignKeys())
                {
                    // 将外键名称设置为小写
                    foreignKey.SetConstraintName(foreignKey.GetConstraintName().ToLower());
                }
                //foreach (var index in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetIndexes()))
                //{
                //    index.set
                //}
            }
        }

    }
}