using Furion.DatabaseAccessor;
using KrasERP.Core.Models;
using KrasERP.Core.Permissions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrasERP.Core.SeedDatas
{
    internal class SeedData : IEntitySeedData<ItemTypeInfo>, IEntitySeedData<PropertyInfo>
    {
        public static string ItemTypeID = "ea8b80f0-8191-4ba9-aed1-7fd1aa6f9f91";
        public IEnumerable<ItemTypeInfo> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new List<ItemTypeInfo>()
            {

                new ItemTypeInfo(){
                    Id=Guid.Parse(ItemTypeID),
                     Name="ItemType",
                      RecordPermissions =new List<string> {
                          PermissionDefine.Ruler,
                          PermissionDefine.System
                      },
                      System=true
                }
            };
        }

        IEnumerable<PropertyInfo> IPrivateEntitySeedData<PropertyInfo>.HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new List<PropertyInfo>()
            {

                new PropertyInfo(){
                    Id=Guid.Parse("47aa96b0-b463-44a9-96f6-073a2f0afe01"),
                    Name="name",
                      System=true,
                      Required=true,
                       Searchable=true,
                        Unique=true,
                         SourceId=ItemTypeID,
                          Label="名称",
                             PropertyDataType= PropertyDataType.Text
                },
                 new PropertyInfo(){
                    Id=Guid.Parse("47aa96b0-b463-44a9-96f6-073a2f0afe01"),
                    Name="label",
                      System=true,
                      Required=true,
                       Searchable=true,
                        Unique=true,
                         SourceId=ItemTypeID,
                          Label="名称",
                          PropertyDataType= PropertyDataType.Text
                },
                   new PropertyInfo(){
                    Id=Guid.Parse("47aa96b0-b463-44a9-96f6-073a2f0afe01"),
                    Name="id",
                      System=true,
                      Required=true,
                       Searchable=true,
                        Unique=true,
                         SourceId=ItemTypeID,
                          Label="ID",
                          PropertyDataType= PropertyDataType.GuidField
                }
            };
        }
    }
}
