using System.Reflection;

namespace Hypersonic.Core
{
    public class InstanceAndProperty
    {
        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public object Instance { get; set; }

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        /// <value>The alias.</value>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the property info.
        /// </summary>
        /// <value>The property info.</value>
        public PropertyInfo PropertyInfo { get; set; }
    }
}
