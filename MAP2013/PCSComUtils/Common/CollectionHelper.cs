using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using PCSComUtils.DataAccess;
using System.Data.Linq.Mapping;
using System.Diagnostics;

namespace PCSComUtils.Common
{
    public static class CollectionHelper
    {
        /// <summary>
        /// Convert IList to DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ConvertTo<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            
            var fields = DynamicProperties.CreatePropertyMethods<T>();

            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (DataColumn column in table.Columns)
                {
                    var field = fields.SingleOrDefault(f => f.Info.Name.Equals(column.ColumnName));
                    row[column.ColumnName] = field.Getter(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }

            return table;
        }

        /// <summary>
        /// Converts IList of DataRow to IList of Entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rows">The rows.</param>
        /// <returns></returns>
        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    var item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }

        /// <summary>
        /// Converts DataTable to IList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public static IList<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            var rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);
        }

        /// <summary>
        /// Create item of <see cref="T"/> from DataRow
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    object value = row[column.ColumnName];
                    prop.SetValue(obj, value, null);
                }
            }

            return obj;
        }

        /// <summary>
        ///     Create data table from object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            var table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                var propertyType = prop.PropertyType;
                if (propertyType.IsGenericType &&
                    propertyType.GetGenericTypeDefinition() != typeof(Nullable<>))
                    continue;
                var memberInfo = entityType.GetMember(prop.Name);
                var columnAttr = Attribute.GetCustomAttribute(memberInfo[0], typeof(ColumnAttribute)) as ColumnAttribute;
                if (columnAttr == null)
                    continue;
                DataColumn column;
                if (propertyType.IsGenericType &&
                    propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var converter = new NullableConverter(propertyType);
                    column = new DataColumn(prop.Name, converter.UnderlyingType) {AllowDBNull = true};
                }
                else
                {
                    column = new DataColumn(prop.Name, propertyType) {AllowDBNull = columnAttr.CanBeNull};
                }
                var allowIdentity = columnAttr.DbType.Contains("IDENTITY");
                column.AutoIncrement = allowIdentity;
                if (allowIdentity)
                {
                    column.AutoIncrementSeed = 1;
                    column.AutoIncrementStep = 1;
                }
                table.Columns.Add(column);
            }

            return table;
        }

        /// <summary>
        /// Bulks copy entities
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="table">The table.</param>
        /// <param name="entities">The entities.</param>
        public static void BulkCopy<TEntity>(this Table<TEntity> table, IEnumerable<TEntity> entities) where TEntity : class
        {
            using (var bulk = new SqlBulkCopy(Utils.Instance.ConnectionString))
            {
                var reader = new EnumerableDataReader<TEntity>(entities);

                foreach (var column in reader.ColumnMappingList)
                    bulk.ColumnMappings.Add(column.Key, column.Value);

                bulk.DestinationTableName = reader.TableName;
                bulk.WriteToServer(reader);
            }
        }

        /// <summary>
        /// Bulks copy entities
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="table">The table.</param>
        /// <param name="entities">The entities.</param>
        public static void BulkCopy<TEntity>(this Table<TEntity> table, DataTable entities) where TEntity : class
        {
            using (var bulk = new SqlBulkCopy(Utils.Instance.ConnectionString, SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.KeepNulls))
            {
                bulk.ColumnMappings.Clear();
                // map all column except the identity column
                foreach (DataColumn column in entities.Columns.Cast<DataColumn>().Where(column => !column.AutoIncrement))
                {
                    bulk.ColumnMappings.Add(column.ColumnName, column.Ordinal);
                }
                bulk.DestinationTableName = entities.TableName;
                bulk.WriteToServer(entities);
            }
        }
    }
}