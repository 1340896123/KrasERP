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
    public class BasePropertyInfoSeedData : IEntitySeedData<BasePropertyInfo>
    {
        public static Guid ItemTypeID = ItemTypeInfoSeedData.ItemTypeID;

        public IEnumerable<BasePropertyInfo> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new List<BasePropertyInfo>()
            {
                new BasePropertyInfo(){
                    Id=Guid.Parse("47aa96b0-b463-44a9-96f6-073a2f0afe01"),
                    Name="id",
                      System=true,
                      Required=true,
                       Searchable=true,
                        Unique=true,
                         SourceID=ItemTypeID,
                          Label="ID",
                          PropertyDataType= PropertyDataType.Guid
                },
                new BasePropertyInfo(){
                    Id=Guid.Parse("d47693ab-dacc-1b74-35ee-1cb70750e827"),
                    Name="name",
                      System=true,
                      Required=true,
                       Searchable=true,
                        Unique=true,
                       SourceID=ItemTypeID,
                          Label="名称",
                      PropertyDataType= PropertyDataType.Text
                },
                 new BasePropertyInfo(){
                    Id=Guid.Parse("71deb1ee-0eed-a0d5-d512-4e249e9a2e44"),
                    Name="label",
                      System=true,
                      Required=true,
                       Searchable=true,
                        Unique=true,
                         SourceID=ItemTypeID,
                          Label="单数标签",
                          PropertyDataType= PropertyDataType.Text
                },
                 new BasePropertyInfo(){
                    Id=Guid.Parse("71deb1ee-0eed-a0d5-d512-4e248e9a2e44"),
                    Name=nameof(ItemTypeInfo.LabelPlural).ToLower(),
                      System=true,
                      Required=true,
                       Searchable=true,
                        Unique=true,
                         SourceID=ItemTypeID,
                          Label="复数标签",
                          PropertyDataType= PropertyDataType.Text
                },
                new BasePropertyInfo(){
                    Id=Guid.Parse("71deb1ee-0eed-a0d5-d512-4e248e9b2e44"),
                    Name=nameof(ItemTypeInfo.KeyedName).ToLower(),
                      System=true,
                      Required=true,
                       Searchable=true,
                        Unique=true,
                         SourceID=ItemTypeID,
                          Label="KeyedName",
                          PropertyDataType= PropertyDataType.Text
                },


            };

        }
    }
}
