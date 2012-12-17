#region Using Directives

using System;
using System.Data;

#endregion

namespace Hypersonic
{
    /// <summary>
    /// This class is a DataReader that provides the ability to seamlessly work
    /// with Nullable types as well as non-Nullable types.
    /// 
    /// The first version of ADO.NET 2.0 does not natively support nullable types
    /// (although this is cleary supported in the run-time).  Therefore, a simple
    /// call like this:
    /// <code>
    /// Nullable<DateTime> someDate = dataReader.GetDateTime(0);
    /// </code>
    /// will throw an exception when the database column contains a null value.
    /// 
    /// This class wraps an IDataReader (which it takes in its constructor) and
    /// 1) provides alternatives to all the GetXXX() methods with GetNullableXXX() methods
    /// 2) provides the ability to use column name in the GetXXX() methods (rather than 
    ///    having to specify the ordinal)
    /// 3) provides all the traditional IDataReader methods by delegating the calls to
    ///    the IDataReader that it wraps.
    /// Author: Steve Michelotti
    /// </summary>
    public sealed class NullableDataReader : IDataReader, INullableReader
    {

        #region Private Fields

        readonly IDataReader _reader;

        /// <summary>
        /// Delegate to be used for anonymous method delegate inference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private delegate T Conversion<T>(int ordinal);

        #endregion


        #region Constructors

        public NullableDataReader(IDataReader dataReader)
        {
            _reader = dataReader;
        }

        #endregion


        #region IDataReader Members

        public void Close()
        {
            _reader.Close();
        }

        public int Depth
        {
            get { return _reader.Depth; }
        }

        public DataTable GetSchemaTable()
        {
            return _reader.GetSchemaTable();
        }

        public bool IsClosed
        {
            get { return _reader.IsClosed; }
        }

        public bool NextResult()
        {
            return _reader.NextResult();
        }

        public bool Read()
        {
            return _reader.Read();
        }

        public int RecordsAffected
        {
            get { return _reader.RecordsAffected; }
        }

        #endregion


        #region IDisposable Members

        public void Dispose()
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }
        }

        #endregion


        #region IDataRecord Members

        /// <summary>
        /// Gets the number of columns in the current row.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// When not positioned in a valid recordset, 0; otherwise, the number of columns in the current record. The default is -1.
        /// </returns>
        public int FieldCount
        {
            get { return _reader.FieldCount; }
        }

        /// <summary>
        /// Gets the value of the specified column as a Boolean.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public bool GetBoolean(int i)
        {
            return _reader.GetBoolean(i);
        }

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public bool GetBoolean(string name)
        {
            return GetBoolean(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the nullable boolean.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public bool? GetNullableBoolean(int index)
        {
            return GetNullable<bool>(index, GetBoolean);
        }

        /// <summary>
        /// Gets the nullable boolean.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public bool? GetNullableBoolean(string name)
        {
            return GetNullableBoolean(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the 8-bit unsigned integer value of the specified column.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>
        /// The 8-bit unsigned integer value of the specified column.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public byte GetByte(int i)
        {
            return _reader.GetByte(i);
        }

        /// <summary>
        /// Gets the byte.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public byte GetByte(string name)
        {
            return GetByte(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the nullable byte.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public byte? GetNullableByte(int index)
        {
            return GetNullable<byte>(index, GetByte);
        }

        /// <summary>
        /// Gets the nullable byte.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public byte? GetNullableByte(string name)
        {
            return GetNullableByte(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Reads a stream of bytes from the specified column offset into the buffer as an array, starting at the given buffer offset.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <param name="fieldOffset">The index within the field from which to start the read operation.</param>
        /// <param name="buffer">The buffer into which to read the stream of bytes.</param>
        /// <param name="bufferoffset">The index for <paramref name="buffer"/> to start the read operation.</param>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>The actual number of bytes read.</returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return _reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        }

        /// <summary>
        /// Gets the character value of the specified column.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>
        /// The character value of the specified column.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public char GetChar(int i)
        {
            return _reader.GetChar(i);
        }

        /// <summary>
        /// Gets the char.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public char GetChar(string name)
        {
            return GetChar(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the nullable char.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public char? GetNullableChar(int index)
        {
            return GetNullable<char>(index, GetChar);
        }

        /// <summary>
        /// Gets the nullable char.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public char? GetNullableChar(string name)
        {
            return GetNullableChar(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Reads a stream of characters from the specified column offset into the buffer as an array, starting at the given buffer offset.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <param name="fieldoffset">The index within the row from which to start the read operation.</param>
        /// <param name="buffer">The buffer into which to read the stream of bytes.</param>
        /// <param name="bufferoffset">The index for <paramref name="buffer"/> to start the read operation.</param>
        /// <param name="length">The number of bytes to read.</param>
        /// <returns>The actual number of characters read.</returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            return _reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
        }

        /**
         * According to MS documentation, this is an infrastructural method that
         * is not intended to be used by public code.
         **/
        public IDataReader GetData(int i)
        {
            return _reader.GetData(i);
        }

        /// <summary>
        /// Gets the data type information for the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The data type information for the specified field.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public string GetDataTypeName(int i)
        {
            return _reader.GetDataTypeName(i);
        }

        /// <summary>
        /// Gets the name of the data type.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string GetDataTypeName(string name)
        {
            return _reader.GetDataTypeName(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the date and time data value of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The date and time data value of the specified field.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public DateTime GetDateTime(int i)
        {
            return _reader.GetDateTime(i);
        }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public DateTime GetDateTime(string name)
        {
            return _reader.GetDateTime(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the nullable date time.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public DateTime? GetNullableDateTime(int index)
        {
            return GetNullable<DateTime>(index, GetDateTime);
        }

        /// <summary>
        /// Gets the nullable date time.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public DateTime? GetNullableDateTime(string name)
        {
            return GetNullableDateTime(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the fixed-position numeric value of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The fixed-position numeric value of the specified field.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public decimal GetDecimal(int i)
        {
            return _reader.GetDecimal(i);
        }

        /// <summary>
        /// Gets the decimal.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public decimal GetDecimal(string name)
        {
            return _reader.GetDecimal(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the nullable decimal.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public decimal? GetNullableDecimal(int index)
        {
            return GetNullable<decimal>(index, GetDecimal);
        }

        /// <summary>
        /// Gets the nullable decimal.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public decimal? GetNullableDecimal(string name)
        {
            return GetNullableDecimal(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the double-precision floating point number of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The double-precision floating point number of the specified field.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public double GetDouble(int i)
        {
            return _reader.GetDouble(i);
        }

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public double GetDouble(string name)
        {
            return _reader.GetDouble(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the nullable double.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public double? GetNullableDouble(int index)
        {
            return GetNullable<double>(index, GetDouble);
        }

        /// <summary>
        /// Gets the nullable double.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public double? GetNullableDouble(string name)
        {
            return GetNullableDouble(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the <see cref="T:System.Type"/> information corresponding to the type of <see cref="T:System.Object"/> that would be returned from <see cref="M:System.Data.IDataRecord.GetValue(System.Int32)"/>.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The <see cref="T:System.Type"/> information corresponding to the type of <see cref="T:System.Object"/> that would be returned from <see cref="M:System.Data.IDataRecord.GetValue(System.Int32)"/>.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public Type GetFieldType(int i)
        {
            return _reader.GetFieldType(i);
        }

        /// <summary>
        /// Gets the type of the field.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Type GetFieldType(string name)
        {
            return _reader.GetFieldType(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the single-precision floating point number of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The single-precision floating point number of the specified field.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public float GetFloat(int i)
        {
            return _reader.GetFloat(i);
        }

        /// <summary>
        /// Gets the float.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public float GetFloat(string name)
        {
            return _reader.GetFloat(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the nullable float.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public float? GetNullableFloat(int index)
        {
            return GetNullable<float>(index, GetFloat);
        }

        /// <summary>
        /// Gets the nullable float.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public float? GetNullableFloat(string name)
        {
            return GetNullableFloat(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Returns the GUID value of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>The GUID value of the specified field.</returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public Guid GetGuid(int i)
        {
            return _reader.GetGuid(i);
        }

        /// <summary>
        /// Gets the GUID.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Guid GetGuid(string name)
        {
            return _reader.GetGuid(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the nullable GUID.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public Guid? GetNullableGuid(int index)
        {
            return GetNullable<Guid>(index, GetGuid);
        }

        /// <summary>
        /// Gets the nullable GUID.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Guid? GetNullableGuid(string name)
        {
            return GetNullableGuid(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the 16-bit signed integer value of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The 16-bit signed integer value of the specified field.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public short GetInt16(int i)
        {
            return _reader.GetInt16(i);
        }

        /// <summary>
        /// Gets the int16.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public short GetInt16(string name)
        {
            return _reader.GetInt16(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the nullable int16.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public short? GetNullableInt16(int index)
        {
            return GetNullable<short>(index, GetInt16);
        }

        /// <summary>
        /// Gets the nullable int16.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public short? GetNullableInt16(string name)
        {
            return GetNullableInt16(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the 32-bit signed integer value of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The 32-bit signed integer value of the specified field.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public int GetInt32(int i)
        {
            return _reader.GetInt32(i);
        }

        /// <summary>
        /// Gets the int32.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public int GetInt32(string name)
        {
            return _reader.GetInt32(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the nullable int32.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public int? GetNullableInt32(int index)
        {
            return GetNullable<int>(index, GetInt32);
        }

        /// <summary>
        /// Gets the nullable int32.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public int? GetNullableInt32(string name)
        {
            return GetNullableInt32(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the 64-bit signed integer value of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The 64-bit signed integer value of the specified field.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public long GetInt64(int i)
        {
            return _reader.GetInt64(i);
        }

        /// <summary>
        /// Gets the int64.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public long GetInt64(string name)
        {
            return _reader.GetInt64(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the nullable int64.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public long? GetNullableInt64(int index)
        {
            return GetNullable<long>(index, GetInt64);
        }

        /// <summary>
        /// Gets the nullable int64.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public long? GetNullableInt64(string name)
        {
            return GetNullableInt64(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the name for the field to find.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The name of the field or the empty string (""), if there is no value to return.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public string GetName(int i)
        {
            return _reader.GetName(i);
        }

        /// <summary>
        /// Return the index of the named field.
        /// </summary>
        /// <param name="name">The name of the field to find.</param>
        /// <returns>The index of the named field.</returns>
        public int GetOrdinal(string name)
        {
            return _reader.GetOrdinal(name);
        }


        /// <summary>
        /// Gets the string value of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>The string value of the specified field.</returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public string GetString(int i)
        {
            return _reader.GetString(i);
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string GetString(string name)
        {
            return _reader.GetString(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the nullable string.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public string GetNullableString(int index)
        {
            string nullable = _reader.IsDBNull(index) ? null : _reader.GetString(index);
            return nullable;
        }

        /// <summary>
        /// Gets the nullable string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string GetNullableString(string name)
        {
            return GetNullableString(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Return the value of the specified field.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// The <see cref="T:System.Object"/> which will contain the field value upon return.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public object GetValue(int i)
        {
            return _reader.GetValue(i);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public object GetValue(string name)
        {
            return _reader.GetValue(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets all the attribute fields in the collection for the current record.
        /// </summary>
        /// <param name="values">An array of <see cref="T:System.Object"/> to copy the attribute fields into.</param>
        /// <returns>
        /// The number of instances of <see cref="T:System.Object"/> in the array.
        /// </returns>
        public int GetValues(object[] values)
        {
            return _reader.GetValues(values);
        }

        /// <summary>
        /// Return whether the specified field is set to null.
        /// </summary>
        /// <param name="i">The index of the field to find.</param>
        /// <returns>
        /// true if the specified field is set to null; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.IndexOutOfRangeException">
        /// The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount"/>.
        /// </exception>
        public bool IsDBNull(int i)
        {
            return _reader.IsDBNull(i);
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
            return _reader.IsDBNull(_reader.GetOrdinal(name));
        }

        /// <summary>
        /// Gets the <see cref="System.Object"/> with the specified name.
        /// </summary>
        /// <value></value>
        public object this[string name]
        {
            get { return _reader[name]; }
        }

        /// <summary>
        /// Gets the <see cref="System.Object"/> with the specified i.
        /// </summary>
        /// <value></value>
        public object this[int i]
        {
            get { return _reader[i]; }
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
        /// <param name="ordinal">Column index.</param>
        /// <param name="convert">Delegate to invoke if the value is not DBNull</param>
        /// <returns></returns>
        private T? GetNullable<T>(int ordinal, Conversion<T> convert) where T : struct
        {
            T? nullable;

            if (_reader.IsDBNull(ordinal))
            {
                nullable = null;
            }
            else
            {
                nullable = convert(ordinal);
            }
            return nullable;
        }

        #endregion

    }
}