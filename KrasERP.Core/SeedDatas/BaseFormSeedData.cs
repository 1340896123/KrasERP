using Furion.DatabaseAccessor;
using KrasERP.Core.Models;
using KrasERP.Core.Permissions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

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
