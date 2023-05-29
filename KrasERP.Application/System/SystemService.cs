using Furion.DatabaseAccessor;
using KrasERP.Core.DataBase;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;

namespace KrasERP.Application
{
   
    public class SystemService : ISystemService, IDynamicApiController, ITransient
    {
      
      
        public string GetDescription()
        {
        
       




            return "让 .NET 开发更简单，更通用，更流行。";
        }


    }

  
}