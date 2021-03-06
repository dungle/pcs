﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace PCSComUtils.DataAccess
{
    public abstract class ObjectDataReader<T> : IDataReader
        where T : class
    {
        protected bool _closed;
        protected IList<DynamicProperties.Property> _fields;
        private readonly IList<ColumnMapping> _columnMappingList;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectDataReader&lt;T&gt;"/> class.
        /// </summary>
        protected ObjectDataReader()
        {
            _fields = DynamicProperties.CreatePropertyMethods<T>();
            _columnMappingList = new List<ColumnMapping>();
            for (int i = 0; i < _fields.Count; i++)
            {
                var field = _fields[i];
                var property = field.Info;
                var columns = property.GetCustomAttributes(typeof(ColumnAttribute), false) as ColumnAttribute[];
                foreach (var column in columns)
                {
                    if (!column.DbType.Contains("IDENTITY")
                        && !column.IsDbGenerated
                        && !column.IsVersion)
                    {
                        _columnMappingList.Add(new ColumnMapping
                        {
                            ColumnIndex = i,
                            ColumnName = column.Name ?? property.Name,
                            ColumnGetter = row => field.Getter(row)
                        });
                    }
                }
            }
            _closed = false;
        }
        
        #region IDataReader Members

        /// <summary>
        /// Return the value of the specified field.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Object"/> which will contain the field value upon return.
        /// </returns>
        /// <param name="i">The index of the field to find. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public abstract object GetValue(int i);

        /// <summary>
        /// Advances the <see cref="T:System.Data.IDataReader"/> to the next record.
        /// </summary>
        /// <returns>
        /// true if there are more rows; otherwise, false.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public abstract bool Read();

        #endregion

        #region Implementation of IDataRecord

        /// <summary>
        /// Gets the number of columns in the current row.
        /// </summary>
        /// <returns>
        /// When not positioned in a valid recordset, 0; otherwise, the number of columns in the current record. The default is -1.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public int FieldCount
        {
            get { return _fields.Count; }
        }

        /// <summary>
        /// Return the index of the named field.
        /// </summary>
        /// <returns>
        /// The index of the named field.
        /// </returns>
        /// <param name="name">The name of the field to find. 
        /// </param><filterpriority>2</filterpriority>
        public virtual int GetOrdinal(string name)
        {
            for (int i = 0; i < _fields.Count; i++)
            {
                if (_fields[i].Info.Name == name)
                {
                    return i;
                }
            }

            throw new IndexOutOfRangeException("name");
        }

        /// <summary>
        /// Gets the column located at the specified index.
        /// </summary>
        /// <returns>
        /// The column located at the specified index as an <see cref="T:System.Object"/>.
        /// </returns>
        /// <param name="i">The zero-based index of the column to get. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        object IDataRecord.this[int i]
        {
            get { return GetValue(i); }
        }

        /// <summary>
        /// Gets the value of the specified column as a Boolean.
        /// </summary>
        /// <returns>
        /// The value of the column.
        /// </returns>
        /// <param name="i">The zero-based column ordinal. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual bool GetBoolean(int i)
        {
            return (Boolean)GetValue(i);
        }

        /// <summary>
        /// Gets the 8-bit unsigned integer value of the specified column.
        /// </summary>
        /// <returns>
        /// The 8-bit unsigned integer value of the specified column.
        /// </returns>
        /// <param name="i">The zero-based column ordinal. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual byte GetByte(int i)
        {
            return (Byte)GetValue(i);
        }

        /// <summary>
        /// Gets the character value of the specified column.
        /// </summary>
        /// <returns>
        /// The character value of the specified column.
        /// </returns>
        /// <param name="i">The zero-based column ordinal. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual char GetChar(int i)
        {
            return (Char)GetValue(i);
        }

        /// <summary>
        /// Gets the date and time data value of the specified field.
        /// </summary>
        /// <returns>
        /// The date and time data value of the specified field.
        /// </returns>
        /// <param name="i">The index of the field to find. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual DateTime GetDateTime(int i)
        {
            return (DateTime)GetValue(i);
        }

        /// <summary>
        /// Gets the fixed-position numeric value of the specified field.
        /// </summary>
        /// <returns>
        /// The fixed-position numeric value of the specified field.
        /// </returns>
        /// <param name="i">The index of the field to find. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual decimal GetDecimal(int i)
        {
            return (Decimal)GetValue(i);
        }

        /// <summary>
        /// Gets the double-precision floating point number of the specified field.
        /// </summary>
        /// <returns>
        /// The double-precision floating point number of the specified field.
        /// </returns>
        /// <param name="i">The index of the field to find. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual double GetDouble(int i)
        {
            return (Double)GetValue(i);
        }

        /// <summary>
        /// Gets the <see cref="T:System.Type"/> information corresponding to the type of <see cref="T:System.Object"/> that would be returned from <see cref="M:System.Data.IDataRecord.GetValue(System.Int32)"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Type"/> information corresponding to the type of <see cref="T:System.Object"/> that would be returned from <see cref="M:System.Data.IDataRecord.GetValue(System.Int32)"/>.
        /// </returns>
        /// <param name="i">The index of the field to find. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual Type GetFieldType(int i)
        {
            return _fields[i].Info.PropertyType;
        }

        /// <summary>
        /// Gets the single-precision floating point number of the specified field.
        /// </summary>
        /// <returns>
        /// The single-precision floating point number of the specified field.
        /// </returns>
        /// <param name="i">The index of the field to find. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual float GetFloat(int i)
        {
            return (float)GetValue(i);
        }

        /// <summary>
        /// Returns the GUID value of the specified field.
        /// </summary>
        /// <returns>
        /// The GUID value of the specified field.
        /// </returns>
        /// <param name="i">The index of the field to find. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual Guid GetGuid(int i)
        {
            return (Guid)GetValue(i);
        }

        /// <summary>
        /// Gets the 16-bit signed integer value of the specified field.
        /// </summary>
        /// <returns>
        /// The 16-bit signed integer value of the specified field.
        /// </returns>
        /// <param name="i">The index of the field to find. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual short GetInt16(int i)
        {
            return (Int16)GetValue(i);
        }

        /// <summary>
        /// Gets the 32-bit signed integer value of the specified field.
        /// </summary>
        /// <returns>
        /// The 32-bit signed integer value of the specified field.
        /// </returns>
        /// <param name="i">The index of the field to find. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual int GetInt32(int i)
        {
            return (Int32)GetValue(i);
        }

        /// <summary>
        /// Gets the 64-bit signed integer value of the specified field.
        /// </summary>
        /// <returns>
        /// The 64-bit signed integer value of the specified field.
        /// </returns>
        /// <param name="i">The index of the field to find. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual long GetInt64(int i)
        {
            return (Int64)GetValue(i);
        }

        /// <summary>
        /// Gets the string value of the specified field.
        /// </summary>
        /// <returns>
        /// The string value of the specified field.
        /// </returns>
        /// <param name="i">The index of the field to find. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual string GetString(int i)
        {
            return (string)GetValue(i);
        }

        /// <summary>
        /// Return whether the specified field is set to null.
        /// </summary>
        /// <returns>
        /// true if the specified field is set to null; otherwise, false.
        /// </returns>
        /// <param name="i">The index of the field to find. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual bool IsDBNull(int i)
        {
            return GetValue(i) == null;
        }

        /// <summary>
        /// Gets the column with the specified name.
        /// </summary>
        /// <returns>
        /// The column with the specified name as an <see cref="T:System.Object"/>.
        /// </returns>
        /// <param name="name">The name of the column to find. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">No column with the specified name was found. 
        /// </exception><filterpriority>2</filterpriority>
        object IDataRecord.this[string name]
        {
            get { return GetValue(GetOrdinal(name)); }
        }

        /// <summary>
        /// Gets the data type information for the specified field.
        /// </summary>
        /// <returns>
        /// The data type information for the specified field.
        /// </returns>
        /// <param name="i">The index of the field to find. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual string GetDataTypeName(int i)
        {
            return GetFieldType(i).Name;
        }

        /// <summary>
        /// Gets the name for the field to find.
        /// </summary>
        /// <returns>
        /// The name of the field or the empty string (""), if there is no value to return.
        /// </returns>
        /// <param name="i">The index of the field to find.</param>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception>
        /// <filterpriority>2</filterpriority>
        public virtual string GetName(int i)
        {
            if (i < 0 || i >= _fields.Count)
            {
                throw new IndexOutOfRangeException("name");
            }
            return _fields[i].Info.Name;
        }

        /// <summary>
        /// Gets all the attribute fields in the collection for the current record.
        /// </summary>
        /// <returns>
        /// The number of instances of <see cref="T:System.Object"/> in the array.
        /// </returns>
        /// <param name="values">An array of <see cref="T:System.Object"/> to copy the attribute fields into. 
        /// </param><filterpriority>2</filterpriority>
        public virtual int GetValues(object[] values)
        {
            int i = 0;
            for (; i < _fields.Count; i++)
            {
                if (values.Length <= i)
                {
                    return i;
                }
                values[i] = GetValue(i);
            }
            return i;
        }

        /// <summary>
        /// Returns an <see cref="T:System.Data.IDataReader"/> for the specified column ordinal.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Data.IDataReader"/>.
        /// </returns>
        /// <param name="i">The index of the field to find. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual IDataReader GetData(int i)
        {
            // need to think about this one
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads a stream of bytes from the specified column offset into the buffer as an array, starting at the given buffer offset.
        /// </summary>
        /// <returns>
        /// The actual number of bytes read.
        /// </returns>
        /// <param name="i">The zero-based column ordinal. 
        /// </param><param name="fieldOffset">The index within the field from which to start the read operation. 
        /// </param><param name="buffer">The buffer into which to read the stream of bytes. 
        /// </param><param name="bufferoffset">The index for <paramref name="buffer"/> to start the read operation. 
        /// </param><param name="length">The number of bytes to read. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            // need to keep track of the bytes got for each record - more work than i want to do right now
            // http://msdn.microsoft.com/en-us/library/system.data.idatarecord.getbytes.aspx
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads a stream of characters from the specified column offset into the buffer as an array, starting at the given buffer offset.
        /// </summary>
        /// <returns>
        /// The actual number of characters read.
        /// </returns>
        /// <param name="i">The zero-based column ordinal. 
        /// </param><param name="fieldoffset">The index within the row from which to start the read operation. 
        /// </param><param name="buffer">The buffer into which to read the stream of bytes. 
        /// </param><param name="bufferoffset">The index for <paramref name="buffer"/> to start the read operation. 
        /// </param><param name="length">The number of bytes to read. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            // need to keep track of the bytes got for each record - more work than i want to do right now
            // http://msdn.microsoft.com/en-us/library/system.data.idatarecord.getchars.aspx
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IDataReader

        /// <summary>
        /// Closes the <see cref="T:System.Data.IDataReader"/> Object.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public virtual void Close()
        {
            _closed = true;
        }

        /// <summary>
        /// Returns a <see cref="T:System.Data.DataTable"/> that describes the column metadata of the <see cref="T:System.Data.IDataReader"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Data.DataTable"/> that describes the column metadata.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.IDataReader"/> is closed. 
        /// </exception><filterpriority>2</filterpriority>
        public virtual DataTable GetSchemaTable()
        {
            var table = new DataTable();
            foreach (DynamicProperties.Property field in _fields)
            {
                var prop = field.Info;
                if (prop.PropertyType.IsGenericType &&
                    prop.PropertyType.GetGenericTypeDefinition() != typeof(Nullable<>))
                    continue;
                if (prop.PropertyType.IsGenericType &&
                    prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var converter = new NullableConverter(prop.PropertyType);
                    table.Columns.Add(prop.Name, converter.UnderlyingType);
                }
                else
                    table.Columns.Add(prop.Name, prop.PropertyType);
            }
            return table;
        }

        /// <summary>
        /// Advances the data reader to the next result, when reading the results of batch SQL statements.
        /// </summary>
        /// <returns>
        /// true if there are more rows; otherwise, false.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public virtual bool NextResult()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a value indicating the depth of nesting for the current row.
        /// </summary>
        /// <returns>
        /// The level of nesting.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public virtual int Depth
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets a value indicating whether the data reader is closed.
        /// </summary>
        /// <returns>
        /// true if the data reader is closed; otherwise, false.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public virtual bool IsClosed
        {
            get { return _closed; }
        }

        /// <summary>
        /// Gets the number of rows changed, inserted, or deleted by execution of the SQL statement.
        /// </summary>
        /// <returns>
        /// The number of rows changed, inserted, or deleted; 0 if no rows were affected or the statement failed; and -1 for SELECT statements.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public virtual int RecordsAffected
        {
            get
            {
                // assuming select only?
                return -1;
            }
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public virtual void Dispose()
        {
            _fields = null;
        }

        #endregion

        /// <summary>
        /// Gets the columns.
        /// </summary>
        /// <value>The columns.</value>
        public IEnumerable<string> Columns
        {
            get
            {
                return _columnMappingList.Select(column => column.ColumnName);
            }
        }

        /// <summary>
        /// Gets the column mapping list.
        /// </summary>
        /// <value>The column mapping list.</value>
        public IDictionary<string, int> ColumnMappingList
        {
            get
            {
                return _columnMappingList.Select(column => new { column.ColumnName, column.ColumnIndex }).ToDictionary(x => x.ColumnName, y => y.ColumnIndex);
            }
        }

        /// <summary>
        /// Represents Column mapping for Linq
        /// </summary>
        protected class ColumnMapping
        {
            /// <summary>
            /// Gets or sets the index of the column.
            /// </summary>
            /// <value>The index of the column.</value>
            public int ColumnIndex { get; set; }
            /// <summary>
            /// Gets or sets the name of the column.
            /// </summary>
            /// <value>The name of the column.</value>
            public string ColumnName { get; set; }
            /// <summary>
            /// Gets or sets the column getter.
            /// </summary>
            /// <value>The column getter.</value>
            public Func<object, object> ColumnGetter { get; set; }
        }
    }
}
