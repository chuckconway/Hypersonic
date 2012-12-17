#region Using Directives

using System;

#endregion

namespace Hypersonic
{
    /// <summary>
    /// This interface defines the contract that a class must implement to read
    /// both non-null and nullable data.  The greatest benefit of this interface
    /// is that, since both NullableDataReader and NullableDataRowReader 
    /// implement it, this interface will allow these classes to be used 
    /// polymorphically.  In other words, if the consumer consumer need to 
    /// populate an object from both a IDataReader and/or a DataRow (from a
    /// DataSet) then they can just write 1 polymorphic method for this by
    /// programming against the INullableReader API rather than having to create
    /// two separate methods.
    /// Author: Steve Michelotti
    /// </summary>
    public interface INullableReader
    {
        #region Interface Methods

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        bool GetBoolean(string name);

        /// <summary>
        /// Gets the nullable boolean.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        bool? GetNullableBoolean(string name);

        /// <summary>
        /// Gets the byte.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        byte GetByte(string name);

        /// <summary>
        /// Gets the nullable byte.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        byte? GetNullableByte(string name);

        /// <summary>
        /// Gets the char.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        char GetChar(string name);

        /// <summary>
        /// Gets the nullable char.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        char? GetNullableChar(string name);

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        DateTime GetDateTime(string name);

        /// <summary>
        /// Gets the nullable date time.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        DateTime? GetNullableDateTime(string name);

        /// <summary>
        /// Gets the decimal.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        decimal GetDecimal(string name);

        /// <summary>
        /// Gets the nullable decimal.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        decimal? GetNullableDecimal(string name);

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        double GetDouble(string name);

        /// <summary>
        /// Gets the nullable double.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        double? GetNullableDouble(string name);

        /// <summary>
        /// Gets the float.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        float GetFloat(string name);

        /// <summary>
        /// Gets the nullable float.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        float? GetNullableFloat(string name);

        /// <summary>
        /// Gets the GUID.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Guid GetGuid(string name);

        /// <summary>
        /// Gets the nullable GUID.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Guid? GetNullableGuid(string name);

        /// <summary>
        /// Gets the int16.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        short GetInt16(string name);

        /// <summary>
        /// Gets the nullable int16.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        short? GetNullableInt16(string name);

        /// <summary>
        /// Gets the int32.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        int GetInt32(string name);

        /// <summary>
        /// Gets the nullable int32.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        int? GetNullableInt32(string name);

        /// <summary>
        /// Gets the int64.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        long GetInt64(string name);

        /// <summary>
        /// Gets the nullable int64.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        long? GetNullableInt64(string name);

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        string GetString(string name);

        /// <summary>
        /// Gets the nullable string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        string GetNullableString(string name);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        object GetValue(string name);

        /// <summary>
        /// Determines whether [is DB null] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// 	<c>true</c> if [is DB null] [the specified name]; otherwise, <c>false</c>.
        /// </returns>
        bool IsDBNull(string name);


        /// <summary>
        /// Gets the field count.
        /// </summary>
        /// <value>The field count.</value>
        int FieldCount { get; }

        #endregion

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        string GetName(int index);
    }
}