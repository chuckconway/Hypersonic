using System;

namespace Hypersonic.Attributes
{
    public class ColumnAttribute : Attribute, IName
    {
        /// <summary>   Gets or sets the name of the field. </summary>
        ///
        /// <value> The name of the field. </value>
        public string Name { get; set; }

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="name"> Name of the field. </param>
        public ColumnAttribute(string name)
        {
            Name = name;
        }
    }

    public interface IName
    {
        /// <summary> Gets the name. </summary>
        /// <value> The name. </value>
        string Name { get; }
    }
}
