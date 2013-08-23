using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using PCSComUtils.Common.BO;
using System.Collections;

namespace PCSComUtils.DataAccess
{
    public class EnumerableDataReader<T> : ObjectDataReader<T>
        where T: class
    {
        private readonly IEnumerator<T> _enumerator;
        private T _current;
        private string _tableName;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerableDataReader&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public EnumerableDataReader(IEnumerable<T> collection)
        {
            _enumerator = collection.GetEnumerator();
            var entityType = collection.GetType().GetInterface("IEnumerable`1").GetGenericArguments()[0];
            TableAttribute[] tableAttrs = entityType.GetCustomAttributes(typeof(TableAttribute), false) as TableAttribute[];
            if (tableAttrs != null)
                _tableName = tableAttrs[0].Name;
        }

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        /// <value>The name of the table.</value>
        public string TableName
        {
            get { return _tableName; }
        }

        /// <summary>
        /// Return the value of the specified field.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Object"/> which will contain the field value upon return.
        /// </returns>
        /// <param name="i">The index of the field to find. 
        /// </param><exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>. 
        /// </exception><filterpriority>2</filterpriority>
        public override object GetValue(int i)
        {
            if (i < 0 || i >= _fields.Count)
            {
                throw new IndexOutOfRangeException();
            }

            return _fields[i].Getter(_current);
        }

        /// <summary>
        /// Advances the <see cref="T:System.Data.IDataReader"/> to the next record.
        /// </summary>
        /// <returns>
        /// true if there are more rows; otherwise, false.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override bool Read()
        {
            bool returnValue = _enumerator.MoveNext();
            _current = returnValue ? _enumerator.Current : default(T);
            return returnValue;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public override void Dispose()
        {
            _current = default(T);
            _enumerator.Dispose();
        }
    }
}
