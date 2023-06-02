using Furion.DatabaseAccessor;
using KrasERP.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace KrasERP.Core.SeedDatas
{
    public class RelationshipTypeSeedData : IEntitySeedData<RelationshipType>
    {
        public IEnumerable<RelationshipType> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new List<RelationshipType>()
            {
                new RelationshipType(){
                    Id= ItemTypeInfoSeedData.ItemTypeRelationshipTypeID,
                     Name="RelationshipType",
                     Label="关系类",
                     LabelPlural = "关系类",
                    System =true,
                    SourceID= ItemTypeInfoSeedData.ItemTypeID,  
                    RelatedId= ItemTypeInfoSeedData.ItemTypeID,
                }
            };
        }
    }
}
