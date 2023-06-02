using Furion.DatabaseAccessor;
using KrasERP.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace KrasERP.Core.SeedDatas
{
    public class ItemTypeInfoSeedData : IEntitySeedData<ItemTypeInfo>
    {
        public static Guid ItemTypeID = Guid.Parse("ea8b80f0-8191-4ba9-aed1-7fd1aa6f9f91");
        public static Guid ItemTypeViewID = Guid.Parse("7f7d7ab8-f1db-f8d7-3fe6-e05f946fd904");
        public static Guid ItemTypeFormID = Guid.Parse("693527c1-3f8b-cb70-73b6-8289642cc4ea");
        public static Guid ItemTypeRelationshipTypeID = new Guid("91F31CA4-9505-493C-A981-2FE22A936CA6"); 
        public IEnumerable<ItemTypeInfo> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new List<ItemTypeInfo>()
            {
                new ItemTypeInfo(){
                    Id=ItemTypeID,
                     Name="ItemType",
                     Label="对象类",
                        LabelPlural = "对象类",
            //RecordPermissions =new List<string> {
            //    PermissionDefine.Ruler,
            //    PermissionDefine.System
            //},
                    System =true
                }
            };
        }
    }
}
