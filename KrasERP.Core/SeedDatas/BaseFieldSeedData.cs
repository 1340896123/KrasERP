using Furion.DatabaseAccessor;
using KrasERP.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace KrasERP.Core.SeedDatas
{
    public class BaseFieldSeedData : IEntitySeedData<BaseField>
    {
        public IEnumerable<BaseField> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new List<BaseField>()
            {
                 new BaseField(){
                      Id=Guid.Parse("dec6908c-bd9b-b1e7-40bb-deb6655b70c5"),
                      Name = nameof(ItemTypeInfo.Name).ToLower(),
                        FieldType= FieldType.Text,
                        Label="名称",
                        SourceId=ItemTypeInfoSeedData.ItemTypeFormID,
                        PropertyInfoId =     Guid.Parse("d47693ab-dacc-1b74-35ee-1cb70750e827")
                 },
                   new BaseField(){
                      Id=Guid.Parse("fd3b2635-4abf-0040-fcac-c07761c61e01"),
                      Name = nameof(ItemTypeInfo.Label).ToLower(),
                        FieldType= FieldType.Text,
                           SourceId=ItemTypeInfoSeedData.ItemTypeFormID,
                        Label="单数标签",
                        PropertyInfoId =  Guid.Parse("71deb1ee-0eed-a0d5-d512-4e249e9a2e44")
                 },
                     new BaseField(){
                      Id=Guid.Parse("421ba6c3-1798-cdac-15bf-2476fba899a1"),
                      Name = nameof(ItemTypeInfo.LabelPlural).ToLower(),
                        FieldType= FieldType.Text,
                           SourceId=ItemTypeInfoSeedData.ItemTypeFormID,
                        Label="复数标签",
                        PropertyInfoId =  Guid.Parse("71deb1ee-0eed-a0d5-d512-4e248e9a2e44")
                 }
            };
        }
    }
}
