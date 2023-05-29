using Furion.ClayObject.Extensions;
using Furion.DatabaseAccessor;
using KrasERP.Core.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace KrasERP.Core.DataBase
{
    public static class DBHelper
    {
        static ISqlRepository sqlRepository = Db.GetSqlRepository();

        public static void CreateTable(string name)
        {


            {
                string sql = $"CREATE TABLE \"{name}\" ();";


                sqlRepository.SqlNonQueryAsync(sql);

            }
        }

        public static void RenameTable(string name, string newName)
        {

            {
                string sql = $"ALTER TABLE \"{name}\" RENAME TO \"{newName}\";";



                sqlRepository.SqlNonQueryAsync(sql);
            }
        }

        public static void DeleteTable(string name, bool cascade = false)
        {

            {
                string cascadeCommand = cascade ? " CASCADE" : "";
                string sql = $"DROP TABLE IF EXISTS \"{name}\"{cascadeCommand};";



                sqlRepository.SqlNonQueryAsync(sql);
            }
        }
        public static void CreateColumn(string tableName, string name, PropertyDataType type, bool isPrimaryKey, object defaultValue, bool isNullable, bool isUnique, int length=0, int precision=0, int scale=0,
            bool useCurrentTimeAsDefaultValue = false, bool generateNewId = false)
        {
            string pgType = DbTypeConverter.ConvertToDatabaseSqlType(type, length, precision, scale);
            if (type == PropertyDataType.Foreign)
            {
                // CreateAutoNumberColumn(tableName, name);
                return;
            }
            {


                string canBeNull = isNullable && !isPrimaryKey ? "NULL" : "NOT NULL";
                string sql = $"ALTER TABLE \"{tableName}\" ADD COLUMN \"{name}\" {pgType} {canBeNull}";

                if (useCurrentTimeAsDefaultValue)
                {
                    sql += @" DEFAULT now() ";
                }
                else if (generateNewId)
                {
                    sql += @" DEFAULT  uuid_generate_v1() ";
                }
                else
                {
                    var defVal = ConvertDefaultValue(type, defaultValue);
                    sql += $" DEFAULT {defVal}";
                }

                if (isPrimaryKey)
                    sql += $" PRIMARY KEY";

                sql += ";";



                sqlRepository.SqlNonQueryAsync(sql);
            }
        }




        public static void RenameColumn(string tableName, string name, string newName)
        {

            {
                string sql = $"ALTER TABLE \"{tableName}\" RENAME COLUMN \"{name}\" TO \"{newName}\";";



                sqlRepository.SqlNonQueryAsync(sql);
            }
        }

        public static void DeleteColumn(string tableName, string name)
        {

            {
                string sql = $"ALTER TABLE \"{tableName}\" DROP COLUMN IF EXISTS \"{name}\";";
                sqlRepository.SqlNonQueryAsync(sql);
            }
        }

        public static void SetPrimaryKey(string tableName, List<string> columns)
        {
            if (columns.Count == 0)
                return;

            string keyNames = "";
            foreach (var col in columns)
            {
                keyNames += $"\"{col}\", ";
            }
            keyNames = keyNames.Remove(keyNames.Length - 2, 2);


            {
                string sql = $"ALTER TABLE \"{tableName}\" ADD PRIMARY KEY ({keyNames});";



                sqlRepository.SqlNonQueryAsync(sql);
            }
        }

        public static void CreateUniqueConstraint(string constraintName, string tableName, List<string> columns)
        {
            if (columns.Count == 0)
                return;

            string colNames = "";
            foreach (var col in columns)
            {
                colNames += $"\"{col}\", ";
            }
            colNames = colNames.Remove(colNames.Length - 2, 2);


            {
                string sql = $"ALTER TABLE \"{tableName}\" DROP CONSTRAINT IF EXISTS \"{constraintName}\";";

                sqlRepository.SqlNonQueryAsync(sql);

                sql = $"ALTER TABLE \"{tableName}\" ADD CONSTRAINT \"{constraintName}\" UNIQUE ({colNames});";

                sqlRepository.SqlNonQueryAsync(sql);
            }
        }

        public static void DropUniqueConstraint(string constraintName, string tableName)
        {

            {
                string sql = $"ALTER TABLE \"{tableName}\" DROP CONSTRAINT IF EXISTS \"{constraintName}\"";

                sqlRepository.SqlNonQueryAsync(sql);
            }
        }

        public static void SetColumnNullable(string tableName, string columnName, bool nullable)
        {

            {
                string operation = "SET";
                if (nullable)
                    operation = "DROP";
                string sql = $"ALTER TABLE \"{tableName}\" ALTER COLUMN \"{columnName}\" {operation} NOT NULL";

                sqlRepository.SqlNonQueryAsync(sql);
            }
        }
        //public static void SetColumnDefaultValue(string tableName, PropertyInfo field, bool overrideNulls)
        //{

        //    string columnName = field.Name;
        //    object value = field.GetFieldDefaultValue();
        //    var type = field.GetPropertyDataType();
        //    {
        //        if (type == PropertyDataType.DateField && ((DateField)field).UseCurrentTimeAsDefaultValue == true)
        //        {
        //            string sql = $"ALTER TABLE ONLY \"{tableName}\" ALTER COLUMN \"{columnName}\" SET DEFAULT now()";

        //            sqlRepository.SqlNonQueryAsync(sql);
        //        }
        //        else if (type == PropertyDataType.DateTimeField && ((DateTimeField)field).UseCurrentTimeAsDefaultValue == true)
        //        {
        //            string sql = $"ALTER TABLE ONLY \"{tableName}\" ALTER COLUMN \"{columnName}\" SET DEFAULT now()";

        //            sqlRepository.SqlNonQueryAsync(sql);
        //        }
        //        else
        //        {
        //            var defVal = ConvertDefaultValue(type, value);
        //            if (value != null && overrideNulls)
        //            {
        //                string updateNullRecordsSql = $"UPDATE \"{tableName}\" SET \"{columnName}\" = {defVal} WHERE \"{columnName}\" IS NULL";
        //                sqlRepository.SqlNonQuery(updateNullRecordsSql);

        //            }

        //            string sql = $"ALTER TABLE ONLY \"{tableName}\" ALTER COLUMN \"{columnName}\" SET DEFAULT {defVal}";

        //            sqlRepository.SqlNonQueryAsync(sql);
        //        }
        //    }
        //}

        public static void CreateRelation(string relName, string originTableName, string originFieldName, string targetTableName, string targetFieldName)
        {
            if (!TableExists(originTableName))
                return;

            if (!TableExists(targetTableName))
                return;
            {
                string sql = $"ALTER TABLE \"{targetTableName}\" ADD CONSTRAINT \"{relName}\" FOREIGN KEY (\"{targetFieldName}\") REFERENCES \"{originTableName}\" (\"{originFieldName}\");";

                sqlRepository.SqlNonQueryAsync(sql);
            }
        }

        public static void CreateNtoNRelation(string relName, string originTableName, string originFieldName, string targetTableName, string targetFieldName)
        {
            string relTableName = $"rel_{relName}";
            CreateTable(relTableName);
            CreateColumn(relTableName, "origin_id", PropertyDataType.GuidField, false, null, false, false);
            CreateColumn(relTableName, "target_id", PropertyDataType.GuidField, false, null, false, false);

            SetPrimaryKey(relTableName, new List<string> { "origin_id", "target_id" });

            CreateRelation($"{relName}_origin", originTableName, originFieldName, targetTableName, "origin_id");
            CreateRelation($"{relName}_target", targetTableName, targetFieldName, relTableName, "target_id");

            CreateIndex("idx_" + relName + "_origin_id", relTableName, "origin_id", null);
            CreateIndex("idx_" + relName + "_target_id", relTableName, "target_id", null);

            if (originFieldName != "id")
            {
                DropIndex($"idx_r_{relName}_{originFieldName}");
                CreateIndex($"idx_r_{relName}_{originFieldName}", originTableName, originFieldName, null);
            }
            if (targetFieldName != "id")
            {
                DropIndex($"idx_r_{relName}_{targetFieldName}");
                CreateIndex($"idx_r_{relName}_{targetFieldName}", targetTableName, targetFieldName, null);
            }
        }

        public static void DeleteRelation(string relName, string tableName)
        {
            if (!TableExists(tableName))
                return;


            {
                string sql = $"ALTER TABLE \"{tableName}\" DROP CONSTRAINT IF EXISTS \"{relName}\";";

                sqlRepository.SqlNonQueryAsync(sql);
            }

            DropIndex(relName);
        }

        public static void DeleteNtoNRelation(string relName, string originTableName, string targetTableName)
        {
            string relTableName = $"rel_{relName}";

            DeleteRelation($"{relName}_origin", originTableName);
            DeleteRelation($"{relName}_target", targetTableName);
            DeleteTable(relTableName, false);
        }

        public static void CreateIndex(string indexName, string tableName, string columnName, PropertyInfo field, bool unique = false, bool ascending = true, bool nullable = false)
        {
            if (!TableExists(tableName))
                return;


            {
                string sql = $"CREATE INDEX IF NOT EXISTS \"{indexName}\" ON \"{tableName}\" (\"{columnName}\"";
                if (unique)
                    sql = $"CREATE UNIQUE INDEX IF NOT EXISTS \"{indexName}\" ON \"{tableName}\" (\"{columnName}\"";
                if (field != null && field is GeographyField)
                    sql = $"CREATE INDEX IF NOT EXISTS \"{indexName}\" ON \"{tableName}\" USING GIST(\"{columnName}\"";
                if (!ascending)
                    sql = sql + " DESC";

                sql = sql + ")";

                if (nullable)
                {
                    sql = sql + $" WHERE \"{columnName}\" IS NOT NULL;";
                }
                else
                    sql = sql + ";";





                sqlRepository.SqlNonQueryAsync(sql);
            }
        }

        public static void CreateFtsIndexIfNotExists(string indexName, string tableName, string columnName)
        {
            if (!TableExists(tableName))
                return;


            {
                string sql = $@"CREATE INDEX IF NOT EXISTS {indexName} ON {tableName} USING gin(to_tsvector('simple', coalesce({columnName}, ' ')));";

                sqlRepository.SqlNonQueryAsync(sql);
            }
        }


        public static void DropIndex(string indexName)
        {

            {
                string sql = $"DROP INDEX IF EXISTS \"{indexName}\"";

                sqlRepository.SqlNonQueryAsync(sql);
            }
        }

        public static bool InsertRecord(string tableName, List<DbParameter> parameters)
        {





            {
                // NpgsqlCommand command = connection.CreateCommand("");

                string columns = "";
                string values = "";
                foreach (var param in parameters)
                {

                    //var parameter = command.CreateParameter() as NpgsqlParameter;
                    //parameter.ParameterName = param.Name;
                    //parameter.Value = param.Value ?? DBNull.Value;
                    //parameter.NpgsqlDbType = param.Type;
                    //command.Parameters.Add(parameter);

                    columns += $"\"{param.ParameterName}\", ";
                    if (!string.IsNullOrWhiteSpace(param.Value.ToString()))
                    {
                        values += param.Value + ", ";
                    }
                    else
                    {

                        values += $"@{param.ParameterName}, ";
                    }
                }

                columns = columns.Remove(columns.Length - 2, 2);
                values = values.Remove(values.Length - 2, 2);

                var sql = $"INSERT INTO \"{tableName}\" ({columns}) VALUES ({values})";
                return sqlRepository.SqlNonQuery(sql, parameters) > 0;


            }


        }

        public static bool UpdateRecord(string tableName, List<DbParameter> parameters)
        {

            {
                //NpgsqlCommand command = connection.CreateCommand("");

                string values = "";
                foreach (var param in parameters)
                {



                    if (!string.IsNullOrWhiteSpace(param.Value.ToString()))
                    {
                        values += $"\"{param.ParameterName}\"={param.Value}, ";
                    }
                    else
                    {
                        values += $"\"{param.ParameterName}\"=@{param.ParameterName}, ";
                    }
                }

                values = values.Remove(values.Length - 2, 2);

                var sql = $"UPDATE \"{tableName}\" SET {values} WHERE id=@id";

                return sqlRepository.SqlNonQuery(sql, parameters) > 0;
            }
        }

        public static bool DeleteRecord(string tableName, Guid id)
        {



            var sql = $"DELETE FROM \"{tableName}\" WHERE id=@id";


            return sqlRepository.SqlNonQuery(sql, new { id = id }) > 0;

        }

        public static bool TableExists(string tableName)
        {


            var sql = $"SELECT EXISTS (  SELECT 1 FROM   information_schema.tables  WHERE  table_schema = 'public' AND table_name = '{tableName}' ) ";
            bool tableExists = (bool)sqlRepository.SqlScalar(sql);


            return tableExists;
        }
        public static string ConvertDefaultValue(PropertyDataType type, object value)
        {
            if (value == null)
                return " NULL";

            switch (type)
            {
                case PropertyDataType.Bool:  string a = value.ToString() == "true" ? "true" : "false"; return a;
                case PropertyDataType.Date:
                    return "'" + ((DateTime)value).ToString("yyyy-MM-dd") + "'";
                case PropertyDataType.DateTime:
                    return "'" + ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss") + "'";
                case PropertyDataType.GuidField:
                    return "'" + ((Guid)value).ToString() + "'";
                case PropertyDataType.List:
                case PropertyDataType.MultiValueList:
                    {
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
                    }
                default:
                    return value.ToString();
            }
        }

    }


}

