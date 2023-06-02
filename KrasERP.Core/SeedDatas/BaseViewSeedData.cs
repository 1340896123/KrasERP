using Furion.DatabaseAccessor;
using KrasERP.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace KrasERP.Core.SeedDatas
{
    public class BaeViewSeedData : IEntitySeedData<BaeView>
    {
        public IEnumerable<BaeView> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new List<BaeView>()
            {
                new BaeView(){
                    Id=ItemTypeInfoSeedData.ItemTypeViewID,
                    SourceId=ItemTypeInfoSeedData.ItemTypeID,
                    RelatedId=ItemTypeInfoSeedData.ItemTypeFormID
                }
            };
        }
    }
}
