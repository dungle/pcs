using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCSComUtils.DataAccess
{
    public class ListDataReader<T> : ObjectDataReader<T>
        where T: class
    {
        private int _index;
        private IList<T> _list;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListDataReader&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="list">The list.</param>
        public ListDataReader(IList<T> list)
        {
            _index = -1;
            _list = list;
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
            return ++_index < _list.Count;
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

            return _fields[i].Getter(_list[_index]);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public override void Dispose()
        {
            _fields = null;
            _list = null;
        }
    }
}
