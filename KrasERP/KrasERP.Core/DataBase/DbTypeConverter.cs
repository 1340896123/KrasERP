
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
        public static string ConvertToDatabaseSqlType(PropertyDataType type,int length,int precision, int scale)
        {
            string pgType = "";
            var dict = new Dictionary<PropertyDataType, string>() {
                    { PropertyDataType.String ,$"varchar({length})"},
                    { PropertyDataType.Text ,"text"},
                    { PropertyDataType.Integer ,"integer"},
                    { PropertyDataType.Float , $"numeric({precision},{scale})"},
                    { PropertyDataType.Decimal , $"numeric({precision},{scale})"},
                    { PropertyDataType.Bool , "boolean"},
                    { PropertyDataType.Date , "date"},
                    { PropertyDataType.Image , "byte[]"},
                    { PropertyDataType.MD5 ,$"varchar({length})"},
                    { PropertyDataType.List , $"varchar({length})"},
                    { PropertyDataType.FilterList , $"varchar({length})"},
                    { PropertyDataType.Foreign , $"varchar({length})"},
                    { PropertyDataType.MultiValueList , $"varchar({length})"},
                    { PropertyDataType.GuidField , "uuid"},
                    { PropertyDataType.RelationField , $"varchar({length})"},
                    { PropertyDataType.GeographyField , "geography"},
                    { PropertyDataType.DateTime , "timestamp"},
            };
            if (dict.ContainsKey(type))
            {
                pgType = dict[type].ToString();
            }
            else {
                throw Oops.Oh("类型转化失败!");
            }
            return pgType;
        }

    }
}
