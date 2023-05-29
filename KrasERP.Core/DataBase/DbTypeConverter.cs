
using Furion.FriendlyException;
using Furion.JsonSerialization;
using KrasERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KrasERP.Core.DataBase
{
    public static class DbTypeConverter
    {
        public static string ConvertToDatabaseSqlType(BasePropertyInfo baseProperty)
        {
            string pgType = "";
            PropertyDataType type = baseProperty.PropertyDataType;
            int length = baseProperty.Length;
            int precision = baseProperty.Precision;
            int scale = baseProperty.Scale;
            var dict = new Dictionary<PropertyDataType, string>() {
                    { PropertyDataType.String ,$"varchar({length})"},
                    { PropertyDataType.Text ,"text"},
                    { PropertyDataType.Integer ,"integer"},
                    { PropertyDataType.Float , $"numeric({precision},{scale})"},
                    { PropertyDataType.Decimal , $"numeric({precision},{scale})"},
                    { PropertyDataType.Bool , "boolean"},
                    { PropertyDataType.Date , "date"},
                    { PropertyDataType.Image , "varchar(36)"},
                    { PropertyDataType.MD5 ,$"varchar({length})"},
                    { PropertyDataType.List , $"varchar({length})"},
                    { PropertyDataType.FilterList , $"varchar({length})"},
                   // { PropertyDataType.Foreign , $"varchar({length})"},

                    { PropertyDataType.Guid , "uuid"},
                    { PropertyDataType.Relation , $"varchar({length})"},
                    { PropertyDataType.Geography , "geography"},
                    { PropertyDataType.DateTime , "timestamp"},
            };
            if (dict.ContainsKey(type))
            {
                pgType = dict[type].ToString();
            }
            else
            {
                throw Oops.Oh("类型转化失败!");
            }
            return pgType;
        }

    }
}
