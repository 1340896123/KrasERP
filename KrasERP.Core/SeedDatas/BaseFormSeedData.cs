using Furion.DatabaseAccessor;
using KrasERP.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace KrasERP.Core.SeedDatas
{
    public class BaseFormSeedData : IEntitySeedData<BaseForm>
    {
        public IEnumerable<BaseForm> HasData(DbContext dbContext, Type dbContextLocator)
        {

            return new List<BaseForm>()
            {
                new BaseForm()
                {
                    Id = ItemTypeInfoSeedData.ItemTypeFormID,
                    Name = "ItemType",
                    Description = ""
                }
            };
        }
    }
}
