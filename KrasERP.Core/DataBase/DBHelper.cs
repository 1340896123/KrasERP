using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using KrasERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace KrasERP.Core.DataBase
{
    public static class DBHelper
    {
        static ISqlRepository sqlRepository = Db.GetSqlRepository();


        public static async Task CreateItemType(string tableName, List<BasePropertyInfo> basePropertyInfos)
        {
            //var tableName = (string)sqlRepository.SqlScalar($"select name from  kras.ItemType where id=@id", new { id = basePropertyInfos.First().SourceID });
            basePropertyInfos.AddRange(GetItemTypePropertyInfos());
            var hasDuplicates = basePropertyInfos.GroupBy(c => c.Name).Where(g => g.Count() > 1);
            //重复列名
            var repeatNames = hasDuplicates.Select(g => g.Key);
            if (repeatNames.Count() > 0)
            {
                throw Oops.Bah($"有重复列名：{string.Join(", ", repeatNames)}");
            }
            await NpgsqlHelper.CreateTableAsync(tableName);
            foreach (var basePropertyInfo in basePropertyInfos)
            {
                await CreateAlterColumn(tableName, basePropertyInfo);
            }
            await CreateAlertConstraint(tableName, basePropertyInfos);
        }

        public static async Task CreateRelationshipItemType(string tableName, string sourceTableName, string relatedTableName)
        {
            //var tableName = (string)sqlRepository.SqlScalar($"select name from  kras.ItemType where id=@id", new { id = basePropertyInfos.First().SourceID });
            //这里应该获取默认关系类列
            List<BasePropertyInfo> basePropertyInfos = new List<BasePropertyInfo>(); ;

            await NpgsqlHelper.CreateTableAsync(tableName);
            foreach (var basePropertyInfo in basePropertyInfos)
            {
                await CreateAlterColumn(tableName, basePropertyInfo);
            }
            await CreateAlertConstraint(tableName, basePropertyInfos);
        }

        private static List<BasePropertyInfo> GetRelationshipItemPropertyInfos(string sourceTableName, string relatedTableName)
        {
            List<BasePropertyInfo> basePropertyInfos = new List<BasePropertyInfo>() {
            new BasePropertyInfo(){ Name="id",PropertyDataType= PropertyDataType.Guid,Id=Guid.NewGuid(), Index=true, System=true },
            new BasePropertyInfo(){ Name="sourceId",PropertyDataType= PropertyDataType.Relation, Id=Guid.NewGuid(), System=true, Unique=true,Length=36,  },
            new BasePropertyInfo(){ Name="relatedId",PropertyDataType= PropertyDataType.Relation,Id=Guid.NewGuid(), System=true, Unique=true,Length=36,  },
            new BasePropertyInfo(){ Name="sort_order",PropertyDataType= PropertyDataType.Integer,Id=Guid.NewGuid(), System=true, Unique=true,Length=36,  }
            };
            return basePropertyInfos;
        }
        private static List<BasePropertyInfo> GetItemTypePropertyInfos()
        {
            List<BasePropertyInfo> basePropertyInfos = new List<BasePropertyInfo>() {
            new BasePropertyInfo(){ Name="id",PropertyDataType= PropertyDataType.Guid,Id=Guid.NewGuid(), Index=true, System=true },
            new BasePropertyInfo(){ Name="config_id",PropertyDataType= PropertyDataType.Relation, Id=Guid.NewGuid(), System=true, Unique=true,Length=36,  },
            //new BasePropertyInfo(){ Name="relatedId",PropertyDataType= PropertyDataType.Relation,Id=Guid.NewGuid(), System=true, Unique=true,Length=36,  },
            //new BasePropertyInfo(){ Name="sort_order",PropertyDataType= PropertyDataType.Relation,Id=Guid.NewGuid(), System=true, Unique=true,Length=36,  }
            };
            return basePropertyInfos;
        }

        public static async Task CreateAlterColumn(string tableName, BasePropertyInfo propertyInfo)
        {
            //var sql = "";
            var isRequire = !propertyInfo.Required && propertyInfo.Name.ToLower() != "id" ? false : true;

            await NpgsqlHelper.AddColumnAsync(tableName, propertyInfo.Name, DbTypeConverter.ConvertToDatabaseSqlType(propertyInfo));
            //if (propertyInfo.PropertyDataType == PropertyDataType.Foreign)
            //{
            //    return;
            //}
            //else if (propertyInfo.PropertyDataType == PropertyDataType.Relation)
            //{
            //}
            //if (propertyInfo.PropertyDataType == PropertyDataType.Bool)
            //{
            //}

        }

        public static async Task CreateAlertConstraint(string tableName, List<BasePropertyInfo> basePropertyInfos)
        {
            if (basePropertyInfos.Count > 0)
            {

                //var uniqueS = basePropertyInfos.Where(l => l.Unique = true);
                // var requiredS = basePropertyInfos.Where(l => l.Required = true);
                var pkS = basePropertyInfos.Where(l => l.Name.ToLower() == "id");
                var relationS = basePropertyInfos.Where(l => l.PropertyDataType == PropertyDataType.Relation);
                await CreateIndexConstraints(tableName, basePropertyInfos);
                await CreateNotNullConstraints(tableName, basePropertyInfos);
                await CreateUniqueConstraints(tableName, basePropertyInfos);

            }

        }

        public static async Task CreateUniqueConstraints(string tableName, IEnumerable<BasePropertyInfo> basePropertyInfos)
        {
            if (basePropertyInfos.Count() > 0)
            {
                var fff = basePropertyInfos.Select(l => l.Name);
                await NpgsqlHelper.AddUniqueConstra(tableName, fff);

            }
        }
        public static async Task CreateNotNullConstraints(string tableName, IEnumerable<BasePropertyInfo> basePropertyInfos)
        {
            if (basePropertyInfos.Count() > 0)
            {
                foreach (var propertyInfo in basePropertyInfos)
                {
                    await NpgsqlHelper.AddRequireConstra(tableName, propertyInfo.Name, propertyInfo.Required);
                }
            }
        }
        public static async Task CreateIndexConstraints(string tableName, IEnumerable<BasePropertyInfo> basePropertyInfos)
        {
            if (basePropertyInfos.Count() > 0)
            {
                foreach (var propertyInfo in basePropertyInfos)
                {
                    await NpgsqlHelper.CreateIndexAsync(tableName, propertyInfo.Name, propertyInfo.Name);
                }
            }
        }
        public static async Task<string> ConvertDefaultValue(PropertyDataType type, object value)
        {
            if (value == null)
                return " NULL";

            switch (type)
            {
                case PropertyDataType.Bool: string a = value.ToString() == "true" ? "true" : "false"; return a;
                case PropertyDataType.Date:
                    return "'" + ((DateTime)value).ToString("yyyy-MM-dd") + "'";
                case PropertyDataType.DateTime:
                    return "'" + ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss") + "'";
                case PropertyDataType.Guid:
                    return "'" + ((Guid)value).ToString() + "'";
                case PropertyDataType.List:

                    string outValue = "";
                    outValue += "'{";
                    List<string> defaultValues = (List<string>)value;
                    if (defaultValues.Count > 0)
                    {
                        foreach (var val in defaultValues)
                        {
                            outValue += $"\"{val}\",";
                        }
                        outValue = outValue.Remove(outValue.Length - 1, 1);
                    }
                    outValue += "}'";
                    return outValue;

                default:
                    return value.ToString();
            }
        }

    }


}

