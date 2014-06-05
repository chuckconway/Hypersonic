using System;

namespace Hypersonic.Attributes
{
    public class TableAttribute : Attribute
    {
        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>
        public string Name { get; set; }

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="name"> The name. </param>
        public TableAttribute(string name)
        {
            Name = name;
        }
        
    }
}
