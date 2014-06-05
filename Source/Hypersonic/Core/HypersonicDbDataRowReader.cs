#region Using Directives

using System;
using System.Data;

#endregion

namespace Hypersonic.Core
{
    /// <summary>
    /// This class allows a DataRow to be read with a datareader-like interface.
    /// Since it implements INullableRader, it provides methods to read both
    /// non-null and nullable data fields.
    /// </summary>
    public class HypersonicDbDataRowReader
    {

        #region Private Fields

        private DataRow _row;

        /// <summary>
        /// Delegate to be used for anonymous method delegate inference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        private delegate T Conversion<T>(string name);

        #endregion


        #region Public Properties

        public DataRow Row
        {
            get
            {
                return _row;
            }
            set
            {
                _row = value;
            }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HypersonicDbDataRowReader"/> class.
        /// </summary>
        /// <param name="dataRow">The data row.</param>
        public HypersonicDbDataRowReader(DataRow dataRow)
        {
            _row = dataRow;
        }

        public HypersonicDbDataRowReader()
        {
        }

        #endregion


        #region INullableReader Members

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public bool GetBoolean(string name)
        {
            return Convert.ToBoolean(_row[name]);
        }

        /// <summary>
        /// Gets the nullable boolean.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public bool? GetNullableBoolean(string name)
        {
            return GetNullable(name, GetBoolean);
        }

        /// <summary>
        /// Gets the byte.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public byte GetByte(string name)
        {
            return Convert.ToByte(_row[name]);
        }

        /// <summary>
        /// Gets the nullable byte.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public byte? GetNullableByte(string name)
        {
            return GetNullable(name, GetByte);
        }

        /// <summary>
        /// Gets the char.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public char GetChar(string name)
        {
            return Convert.ToChar(_row[name]);
        }

        /// <summary>
        /// Gets the nullable char.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public char? GetNullableChar(string name)
        {
            return GetNullable(name, GetChar);
        }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public DateTime GetDateTime(string name)
        {
            return Convert.ToDateTime(_row[name]);
        }

        /// <summary>
        /// Gets the nullable date time.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public DateTime? GetNullableDateTime(string name)
        {
            return GetNullable(name, GetDateTime);
        }

        /// <summary>
        /// Gets the decimal.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public decimal GetDecimal(string name)
        {
            return Convert.ToDecimal(_row[name]);
        }

        /// <summary>
        /// Gets the nullable decimal.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public decimal? GetNullableDecimal(string name)
        {
            return GetNullable(name, GetDecimal);
        }

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public double GetDouble(string name)
        {
            return Convert.ToDouble(_row[name]);
        }

        /// <summary>
        /// Gets the nullable double.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public double? GetNullableDouble(string name)
        {
            return GetNullable(name, GetDouble);
        }

        /// <summary>
        /// Gets the float.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public float GetFloat(string name)
        {
            return Convert.ToSingle(_row[name]);
        }

        /// <summary>
        /// Gets the nullable float.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public float? GetNullableFloat(string name)
        {
            return GetNullable(name, GetFloat);
        }

        /// <summary>
        /// Gets the GUID.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Guid GetGuid(string name)
        {
            return (Guid)_row[name];
        }

        /// <summary>
        /// Gets the nullable GUID.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Guid? GetNullableGuid(string name)
        {
            return GetNullable(name, GetGuid);
        }

        /// <summary>
        /// Gets the int16.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public short GetInt16(string name)
        {
            return Convert.ToInt16(_row[name]);
        }

        /// <summary>
        /// Gets the nullable int16.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public short? GetNullableInt16(string name)
        {
            return GetNullable(name, GetInt16);
        }

        /// <summary>
        /// Gets the int32.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public int GetInt32(string name)
        {
            return Convert.ToInt32(_row[name]);
        }

        /// <summary>
        /// Gets the nullable int32.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public int? GetNullableInt32(string name)
        {
            return GetNullable(name, GetInt32);
        }

        /// <summary>
        /// Gets the int64.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public long GetInt64(string name)
        {
            return Convert.ToInt64(_row[name]);
        }

        /// <summary>
        /// Gets the nullable int64.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public long? GetNullableInt64(string name)
        {
            return GetNullable(name, GetInt64);
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string GetString(string name)
        {
            return _row[name].ToString();
        }

        /// <summary>
        /// Gets the nullable string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string GetNullableString(string name)
        {
            if (_row[name] == DBNull.Value)
            {
                return null;
            }
            
            return GetString(name);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public object GetValue(string name)
        {
            return _row[name];
        }

        /// <summary>
        /// Determines whether [is DB null] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// 	<c>true</c> if [is DB null] [the specified name]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsDBNull(string name)
        {
            return (_row[name] == DBNull.Value);
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public string GetName(int index)
        {
            return _row.Table.Columns[index].ColumnName;
        }

        /// <summary>
        /// Gets the field count.
        /// </summary>
        /// <value>The field count.</value>
        public int FieldCount
        {
            get { return _row.Table.Columns.Count; }
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// This generic method will be call by every interface method in the class.
        /// The generic method will offer significantly less code, with type-safety.
        /// Additionally, the methods can you delegate inference to pass the 
        /// appropriate delegate to be executed in this method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">Column name</param>
        /// <param name="convert">Delegate to invoke if the value is not DBNull</param>
        /// <returns></returns>
        private T? GetNullable<T>(string name, Conversion<T> convert) where T : struct
        {
            T? nullable;
            if (_row[name] == DBNull.Value)
            {
                nullable = null;
            }
            else
            {
                nullable = convert(name);
            }
            return nullable;
        }

        #endregion

    }
}