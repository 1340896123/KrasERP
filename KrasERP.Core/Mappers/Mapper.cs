using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrasERP.Core.Mappers
{
    public class MapperUtils
    {
        public static  JArray DataTable2JArray(DataTable table)
        {
            // 将 DataTable 转换为 List<Dictionary<string, object>>
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            foreach (DataRow dataRow in table.Rows)
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                foreach (DataColumn column in table.Columns)
                {
                    item[column.ColumnName] = dataRow[column];
                }
                list.Add(item);
            }

            // 将 List<Dictionary<string, object>> 转换为 JArray
            JArray jArray = JArray.FromObject(list);
            return jArray;
        }
    }
}
