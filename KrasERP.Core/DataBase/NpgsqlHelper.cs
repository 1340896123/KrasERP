using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


namespace KrasERP.Core.DataBase
{
    public static class NpgsqlHelper
    {
        static ISqlRepository sqlRepository = Db.GetSqlRepository();

        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static bool TableExists(string tableName)
        {
            IsValidIdentifier(tableName);
            var sql = $"SELECT EXISTS (  SELECT 1 FROM   information_schema.tables  WHERE  table_schema = 'public static ' AND table_name = @tableName ) ";
            bool tableExists = (bool)sqlRepository.SqlScalar(sql, new { tableName });
            return tableExists;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static async Task CreateTableAsync(string tableName)
        {
            IsValidIdentifier(tableName);
            var createTableCommand = $"CREATE TABLE {tableName}()";
            await sqlRepository.SqlNonQueryAsync(createTableCommand);
        }

        public static async Task AddColumnAsync(string tableName, string columnName, string columnType)
        {
            IsValidIdentifier(tableName, columnName, columnType);
            var addColumnCommand = $"ALTER TABLE {tableName} ADD COLUMN {columnName} {columnType}";
            await sqlRepository.SqlNonQueryAsync(addColumnCommand);
        }


        public static async Task AlterColumnTypeAsync(string tableName, string columnName, string columnType)
        {
            IsValidIdentifier(tableName, columnName, columnType);
            var alterColumnCommand = $"ALTER TABLE {tableName} ALTER COLUMN {columnName} TYPE {columnType}";
            await sqlRepository.SqlNonQueryAsync(alterColumnCommand);
        }

        public static async Task AlterColumnNameAsync(string tableName, string columnName, string newColumnName)
        {
            IsValidIdentifier(tableName, columnName, newColumnName);
            var alterColumnCommand = $"ALTER TABLE {tableName} RENAME COLUMN {columnName} TO  {newColumnName}";
            await sqlRepository.SqlNonQueryAsync(alterColumnCommand);
        }

        public static async Task DropColumnAsync(string tableName, string columnName)
        {
            IsValidIdentifier(tableName, columnName);
            var dropColumnCommand = $"ALTER TABLE {tableName} DROP COLUMN {columnName}";
            await sqlRepository.SqlNonQueryAsync(dropColumnCommand);
        }

        public static async Task DropTableSchemeAsync(string tableName)
        {
            IsValidIdentifier(tableName);
            var dropTableCommand = $"DROP TABLE {tableName}";
            await sqlRepository.SqlNonQueryAsync(dropTableCommand);
        }
        public static async Task DropTableDataAsync(string tableName)
        {
            IsValidIdentifier(tableName);
            var dropTableCommand = $"DELETE FROM {tableName};";
            await sqlRepository.SqlNonQueryAsync(dropTableCommand);
        }
        public static async Task AddForeignKeyConstraintAsync(string tableName, string constraintName, string columnName, string referencedTableName, string referencedColumnName)
        {
            IsValidIdentifier(tableName, constraintName, columnName, referencedTableName, referencedColumnName);
            var addForeignKeyConstraintCmdText = $@"
          ALTER TABLE {tableName}
          ADD CONSTRAINT {constraintName} FOREIGN KEY ({columnName})
          REFERENCES {referencedTableName} ({referencedColumnName})";
            await sqlRepository.SqlNonQueryAsync(addForeignKeyConstraintCmdText);
        }
        public static async Task AddPrimaryKeyConstra(string tableName, List<string> columnNames)
        {
            IsValidIdentifier(tableName);
            IsValidIdentifier(columnNames.ToArray());
            var sql = $@"SELECT CONSTRAINT_NAME
FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
WHERE TABLE_NAME = @tableName AND CONSTRAINT_TYPE = 'PRIMARY KEY'";

            var dd = await sqlRepository.SqlScalarAsync(sql, new { tableName });
            if (dd != null)
            {
                string constraintName = dd as string;
                sql = $"ALTER TABLE {tableName} DROP CONSTRAINT  {constraintName};" +
                    $"ADD PRIMARY KEY ({string.Join(",", columnNames)});";
                await sqlRepository.SqlNonQueryAsync(sql);

            }
        }

        //public static async Task AddUniqueConstra(string tableName, string constraintName, string columnName)
        //{
        //    IsValidIdentifier(tableName, constraintName, columnName);
        //    var sql = $"ALTER TABLE {tableName} ADD CONSTRAINT {constraintName} UNIQUE ({columnName})";
        //    await sqlRepository.SqlNonQueryAsync(sql);
        //}
        public static async Task AddUniqueConstra(string tableName, IEnumerable<string> columnNames)
        {
            IsValidIdentifier(tableName);
            IsValidIdentifier(columnNames.ToArray());
            var sql = $"ALTER TABLE {tableName} ADD  UNIQUE ({string.Join(",", columnNames)})";
            await sqlRepository.SqlNonQueryAsync(sql);
        }

        /// <summary>
        /// 获取是关于某列是否有约束
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="constraintType"></param>
        /// <returns></returns>
        public static async Task<bool> QueryHasConstra(string tableName, string columnName, ConstraintType constraintType)
        {
            IsValidIdentifier(tableName, columnName);
            var sql = "";
            if (constraintType == ConstraintType.NotNull)
            {
                sql = $@"SELECT attnotnull
                FROM pg_attribute
                WHERE attrelid = '{tableName}'::regclass
                  AND attname = '{columnName}';
                ";
                var f = await sqlRepository.SqlScalarAsync(sql);
                var s = f.ToString();
                return s == "t";

            }
            else
            {
                sql = $@"SELECT
	                        count(*) num
                        FROM
	                        pg_constraint
                        WHERE
	                        contype = 'u'
	                        AND conrelid = 'kkkk' :: REGCLASS
	                        AND conkey = ARRAY (
		                        SELECT
			                        attnum
		                        FROM
			                        pg_attribute
		                        WHERE
			                        attrelid = 'kkkk' :: REGCLASS
			                        AND attname = 'id'
	                        );";
                var f = await sqlRepository.SqlScalarAsync(sql);
                var s = f.ToString();
                return s != "0";
            }

        }

        public static async Task AddRequireConstra(string tableName, string columnName, bool isRequire)
        {
            IsValidIdentifier(tableName, columnName);
            var tem = isRequire == true ? "NOT NULL" : "NULL";
            var sql = $"ALTER TABLE {tableName} ALTER COLUMN {columnName} SET {tem}";
            await sqlRepository.SqlNonQueryAsync(sql);
        }

        public static async Task DropConstraintAsync(string tableName, string constraintName)
        {
            IsValidIdentifier(tableName, constraintName);
            var dropConstraintCmdText = $"ALTER TABLE {tableName} DROP CONSTRAINT {constraintName}";
            await sqlRepository.SqlNonQueryAsync(dropConstraintCmdText);
        }
        public static async Task DroppAllConstraintByTableNameAsync(string tableName)
        {
            IsValidIdentifier(tableName);
            var sql = $@"SELECT concat('ALTER TABLE ',table_name,' DROP CONSTRAINT IF EXISTS ',constraint_name,';') AS query
FROM information_schema.table_constraints
WHERE constraint_type = 'CHECK' AND table_name = '{tableName}';";
            var conSql = await sqlRepository.SqlScalarAsync(sql);
            if (conSql != null)
            {
                await sqlRepository.SqlScalarAsync(conSql.ToString());
            }
        }

        //public static async Task DropConstraintByColumnNameAsync(string tableName, string columnName)
        //{
        //    IsValidIdentifier(tableName, columnName);
        //    var sql = $@"SELECT constraint_name
        //FROM information_schema.constraint_column_usage
        //WHERE table_name = @tableName AND column_name = @columnName;
        //";
        //    var fffs = await sqlRepository.SqlQueryAsync(sql, new { tableName, columnName });

        //    if (fffs.Rows.Count > 0)
        //    {
        //        var listSql = new List<string>();
        //        for (int i = 0; i < fffs.Rows.Count; i++)
        //        {
        //            var constraint_name = fffs.Rows[i][0].ToString();
        //            var temSql = $@"ALTER TABLE {tableName} DROP CONSTRAINT IF EXISTS {constraint_name};";
        //            listSql.Add(temSql);
        //        }
        //        await sqlRepository.SqlNonQueryAsync(string.Join(";", listSql));
        //    }
        //}


        public static async Task AddDefaultValueAsync(string tableName, string columnName, string defaultValue)
        {
            IsValidIdentifier(tableName, columnName);
            IsValidDefaultValueAsync(tableName, columnName, defaultValue);
            var addDefaultValueCmdText = $"ALTER TABLE {tableName} ALTER COLUMN {columnName} SET DEFAULT @defaultValue ";
            await sqlRepository.SqlNonQueryAsync(addDefaultValueCmdText, new { tableName, columnName, defaultValue });
        }

        public static async Task CreateIndexAsync(string tableName, string columnName, string indexName)
        {
            IsValidIdentifier(tableName, columnName, indexName);
            var sql = $"CREATE INDEX {indexName} ON {tableName}({columnName})";
            await sqlRepository.SqlNonQueryAsync(sql);
        }
        public static async Task DeleteRowByID(string tableName, Guid id)
        {
            IsValidIdentifier(tableName);
            var sql = $"DELETE FROM \"{tableName}\"  where id=@id";
            await sqlRepository.SqlNonQueryAsync(sql, new { id });
        }
        public static async Task<bool> InsertRowAsync(string tableName, List<DbParameter> parameters)
        {
            var columnsBuilder = new StringBuilder();
            var valuesBuilder = new StringBuilder();
            foreach (var param in parameters)
            {
                columnsBuilder.AppendFormat("[{0}], ", param.ParameterName);
                if (param.Value == null || string.IsNullOrWhiteSpace(param.Value.ToString()))
                {
                    valuesBuilder.AppendFormat("@{0}, ", param.ParameterName);
                    param.Value = DBNull.Value;
                }
                else
                {
                    valuesBuilder.AppendFormat("@{0}, ", param.ParameterName);
                }
            }
            var columns = columnsBuilder.ToString().TrimEnd(',', ' ');
            var values = valuesBuilder.ToString().TrimEnd(',', ' ');

            var sql = $"INSERT INTO [{tableName}] ({columns}) VALUES ({values})";
            return await sqlRepository.SqlNonQueryAsync(sql, parameters) > 0;
        }
        public static async Task<int> UpdateRowAsync(string tableName, List<DbParameter> parameters)
        {
            IsValidIdentifier(tableName);
            if (parameters == null || parameters.Count == 0)
            {
                throw new ArgumentException("Parameters cannot be null or empty.", nameof(parameters));
            }

            var sql = BuildUpdateSql(tableName, parameters);

            return await sqlRepository.SqlNonQueryAsync(sql, parameters);
        }

        private static string BuildUpdateSql(string tableName, List<DbParameter> parameters)
        {
            var sb = new StringBuilder($"UPDATE \"{tableName}\" SET ");

            for (int i = 0; i < parameters.Count; i++)
            {
                var param = parameters[i];

                if (string.IsNullOrWhiteSpace(param.Value?.ToString()))
                {
                    sb.Append($"\"{param.ParameterName}\"=@{param.ParameterName}");
                }
                else
                {
                    sb.Append($"\"{param.ParameterName}\"={param.Value}");
                }

                if (i < parameters.Count - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append(" WHERE id=@id");

            return sb.ToString();
        }


        public static async Task DropIndexByIndexNameAsync(string indexName)
        {
            IsValidIdentifier(indexName);
            var dropIndexCmdText = $"DROP INDEX IF EXISTS {indexName}";
            await sqlRepository.SqlNonQueryAsync(dropIndexCmdText);
        }
        public static async Task DropIndexByTableNameAsync(string tableName)
        {
            IsValidIdentifier(tableName);
            var dropIndexCmdText = $"SELECT indexname FROM pg_indexes WHERE tablename = @tableName ";
            var fffs = await sqlRepository.SqlQueryAsync(dropIndexCmdText, new { tableName });
            if (fffs.Rows.Count > 0)
            {
                var listSql = new List<string>();
                for (int i = 0; i < fffs.Rows.Count; i++)
                {
                    var indexName = fffs.Rows[i][0].ToString();
                    var temSql = $@"ALTER TABLE {tableName} DROP INDEX IF EXISTS {indexName};";
                    listSql.Add(temSql);
                }
                await sqlRepository.SqlNonQueryAsync(string.Join(";", listSql));
            }
        }

        public static async Task CreateSequenceAsync(string sequenceName)
        {
            IsValidIdentifier(sequenceName);
            var sql = $"CREATE SEQUENCE {sequenceName}";
            await sqlRepository.SqlNonQueryAsync(sql);
        }

        public static async Task DropSequenceAsync(string sequenceName)
        {
            IsValidIdentifier(sequenceName);
            var sql = $"DROP SEQUENCE IF EXISTS {sequenceName}";
            await sqlRepository.SqlNonQueryAsync(sql);
        }

        public static async Task CreateSchemaAsync(string schemaName)
        {
            IsValidIdentifier(schemaName);
            var sql = $"CREATE SCHEMA IF NOT EXISTS {schemaName}";
            await sqlRepository.SqlNonQueryAsync(sql);
        }

        public static async Task DropSchemaAsync(string schemaName)
        {
            IsValidIdentifier(schemaName);
            var sql = $"DROP SCHEMA IF EXISTS {schemaName} CASCADE";
            await sqlRepository.SqlNonQueryAsync(sql);
        }

        public static async Task CreateTriggerFunctionAsync(string functionName, string functionBody)
        {

            //functionBody should be like this:
            //$$
            //BEGIN
            // -- trigger function body
            //END;
            //$$ LANGUAGE plpgsql;

            //functionBody should be like this:
            //$$
            //BEGIN
            // -- trigger function body
            //END;
            //$$ LANGUAGE plpgsql;
            IsValidIdentifier(functionName);
            ValidateFunctionParameters(functionBody);
            var sql = $"CREATE OR REPLACE FUNCTION {functionName}() RETURNS TRIGGER AS{functionBody}";
            await sqlRepository.SqlNonQueryAsync(sql);

        }


        public static async Task DropTriggerFunctionAsync(string functionName)
        {
            IsValidIdentifier(functionName);
            var sql = $"DROP FUNCTION IF EXISTS {functionName}";

            await sqlRepository.SqlNonQueryAsync(sql);
        }




        public static async Task CreateTriggerAsync(string triggerName, string tableName, string functionName, string eventManipulation, string timing, string condition = "")
        {

            //eventManipulation should be like this: INSERT OR DELETE OR UPDATE

            //timing should be: BEFORE or AFTER

            //condition should be like this: WHEN (NEW.column_name operator value)
            IsValidIdentifier(triggerName, tableName, functionName);
            ValidateTriggerParameters(eventManipulation, timing);
            if (condition != "")
                condition = " " + condition;

            var sql = $"CREATE TRIGGER {triggerName}{timing} {eventManipulation} ON {tableName}{condition} FOR EACH ROW EXECUTE FUNCTION{functionName}";

            await sqlRepository.SqlNonQueryAsync(sql);

        }


        public static async Task DropTriggerAsync(string triggerName, string tableName)
        {
            IsValidIdentifier(triggerName, tableName);
            var sql = $"DROP TRIGGER IF EXISTS{triggerName} ON {tableName}";

            await sqlRepository.SqlNonQueryAsync(sql);

        }

        public static async Task CreateViewAsync(string viewName, string selectStatement)
        {
            IsValidIdentifier(viewName);
            //if (!selectStatement.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
            //{
            //    throw new ArgumentException("Select statement must start with SELECT.");
            //}
            var sql = $"CREATE VIEW {viewName} ON {selectStatement}";
            await sqlRepository.SqlNonQueryAsync(sql);
        }

        public static async Task DropViewAsync(string viewName)
        {
            IsValidIdentifier(viewName);
            var sql = $"DROP VIEW IF EXISTS {viewName}";
            await sqlRepository.SqlNonQueryAsync(sql);
        }

        public static async Task CreateStoredProcedureAsync(string procedureName, string procedureArguments, string procedureBody)
        {
            IsValidIdentifier(procedureName);
            var sql = $@"CREATE OR REPLACE PROCEDURE {procedureName} {procedureArguments}
                    LANGUAGE PLPGSQL
                    AS $$
                    BEGIN
                    {procedureBody}
                    END; $$";
            await sqlRepository.SqlNonQueryAsync(sql);
        }



        public static async Task DropStoredProcedureAsync(string procedureName)
        {
            IsValidIdentifier(procedureName);
            var sql = $"DROP PROCEDURE IF EXISTS {procedureName}";
            await sqlRepository.SqlNonQueryAsync(sql);
        }

        public static async Task RenameTableAsync(string oldTableName, string newTableName)
        {
            IsValidIdentifier(oldTableName, newTableName);
            var sql = $"ALTER TABLE {oldTableName} RENAME TO {newTableName}";
            await sqlRepository.SqlNonQueryAsync(sql);
        }

        public static async Task RenameColumnAsync(string tableName, string oldColumnName, string newColumnName)
        {
            IsValidIdentifier(tableName, oldColumnName, newColumnName);
            string sql = $"ALTER TABLE {tableName} RENAME COLUMN {oldColumnName} TO {newColumnName}";
            await sqlRepository.SqlNonQueryAsync(sql);
        }

        public static async Task RenameConstraintAsync(string tableName, string oldConstraintName, string newConstraintName)
        {
            IsValidIdentifier(tableName, oldConstraintName, newConstraintName);
            var sql = $"ALTER TABLE {tableName} RENAME CONSTRAINT {oldConstraintName} TO {newConstraintName}";
            await sqlRepository.SqlNonQueryAsync(sql);
        }
        /// <summary>
        /// 验证表名和列名是否包含特殊字符
        /// </summary>
        /// <param name="identifiers"></param>
        /// <returns></returns>
        public static async void IsValidIdentifier(params string[] identifiers)
        {
            await Task.Run(() =>
               {
                   var flag = true;
                   for (int i = 0; i < identifiers.Length; i++)
                   {
                       var identifier = identifiers[i];

                       // 检查标识符是否为空或空白
                       if (string.IsNullOrWhiteSpace(identifier))
                           flag = false;

                       // 检查标识符的长度是否超过PgSQL允许的最大长度
                       if (identifier.Length > 63)
                           flag = false;

                       // 检查标识符中是否包含非法字符
                       foreach (char c in identifier)
                       {
                           if (!char.IsLetterOrDigit(c) && c != '_')
                               flag = false;
                       }

                       // 检查标识符是否以数字开头
                       if (char.IsDigit(identifier[0]))
                           flag = false;

                       //如果未通过验证
                       if (!flag)
                       {
                           throw Oops.Bah($"{identifier}不能作为SQL参数!");
                       }
                   }

               });


        }


        /// <summary>
        /// 验证默认值是否为列类型相同的类型
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static async void IsValidDefaultValueAsync(string tableName, string columnName, string defaultValue)
        {
            var flag = true;
            // 查询列的数据类型
            var sql = $@"SELECT data_type
        FROM information_schema.columns
        WHERE table_name = @tableName AND column_name = @columnName;
        ";
            var result = await sqlRepository.SqlQueryAsync(sql, new { tableName, columnName });

            if (result.Rows.Count == 0)
                flag = false;

            var dataType = result.Rows[0][0].ToString();

            // 根据数据类型验证默认值
            switch (dataType)
            {
                case "integer":
                    flag = int.TryParse(defaultValue, out _);
                    break;
                case "real":
                case "double precision":
                    flag = double.TryParse(defaultValue, out _);
                    break;
                case "numeric":
                    flag = decimal.TryParse(defaultValue, out _);
                    break;
                case "text":
                    flag = true;
                    break;
                case "boolean":
                    flag = bool.TryParse(defaultValue, out _);
                    break;
                case "date":
                    flag = DateTime.TryParse(defaultValue, out _);
                    break;
                default:
                    flag = false;
                    break;
            }

            if (!flag)
            {
                throw Oops.Bah($"{tableName}表{columnName}列的{dataType}数据类型的默认值不能为{defaultValue}");
            }
        }

        private static async void ValidateFunctionParameters(string functionBody)
        {
            await Task.Run(() =>
              {
                  // Check if functionBody is null or empty
                  if (string.IsNullOrEmpty(functionBody))
                  {
                      throw new ArgumentException("Function body cannot be null or empty.");
                  }

                  // Check if functionBody contains any potentially dangerous commands
                  var blacklist = new[] { "DROP", "DELETE", "TRUNCATE", "ALTER" };
                  if (blacklist.Any(x => functionBody.IndexOf(x, StringComparison.OrdinalIgnoreCase) >= 0))
                  {
                      throw new ArgumentException("Function body contains potentially dangerous commands.");
                  }
              });
        }
        private static async void ValidateTriggerParameters(string eventManipulation, string timing)
        {
            await Task.Run(() =>
             {
                 // Check if eventManipulation is valid
                 var validEventManipulations = new[] { "INSERT", "DELETE", "UPDATE" };
                 if (!validEventManipulations.Any(x => eventManipulation.IndexOf(x, StringComparison.OrdinalIgnoreCase) >= 0))
                 {
                     throw new ArgumentException("Invalid event manipulation.");
                 }

                 // Check if timing is valid
                 var validTimings = new[] { "BEFORE", "AFTER" };
                 if (!validTimings.Contains(timing.ToUpper()))
                 {
                     throw new ArgumentException("Invalid timing.");
                 }
             });
        }


    }
}