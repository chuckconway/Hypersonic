using System;

namespace Hypersonic.Attributes
{
    public class PrimaryKeyAttribute : Attribute
    {
        /// <summary> Constructor. </summary>
        /// <param name="generator"> (optional) the generator. </param>
        public PrimaryKeyAttribute(Generator generator = Generator.Default | Generator.Guid)
        {
            Generator = generator;
        }

        /// <summary> Gets or sets the generator. </summary>
        /// <value> The generator. </value>
        public Generator Generator { get; set; }
    }

    [Flags]
    public enum Generator
    {
        Guid,
        Default
    }
}
