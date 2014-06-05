using System;
using System.Collections.Generic;
using System.Linq;
using Hypersonic.Attributes;
using Hypersonic.Core;
using Hypersonic.Core.Extensions;
using Hypersonic.Session.Persistance;
using Hypersonic.Session.Query.Expressions;

namespace Hypersonic.Session.Persistence
{
    public class HasPrimaryKeys : IKeysDefined
    {
        /// <summary> Gets a value indicating whether the primary keys exist. </summary>
        /// <value> true if primary keys exist, false if not. </value>
        public bool PrimaryKeysExist
        {
            get { return true; }
        }

        /// <summary> Generates the SQL. </summary>
        /// <param name="name">        The name. </param>
        /// <param name="primaryKeys"> The item. </param>
        /// <param name="properties">  The properties. </param>
        /// <returns> The sql. </returns>
        public string GenerateSql(string name, IList<Property> primaryKeys, IList<Property> properties)
        {
            var generator = new SqlGenerator();
            var toSql = new PrimaryKeysToSql();

            bool valuesAreDefault = primaryKeys.Any(p => Convert.ToString(p.Value) == Convert.ToString(GetRuntimeDefaultValue(p.PropertyDescriptor.PropertyType)));
            primaryKeys = CreateNewGuidForGuidPrimaryKeyMarkedWithGuidGeneratorFlag(primaryKeys, properties);
            var withoutPrimaryKeys = properties.Except(primaryKeys, new CompareProperty());

            string sql = valuesAreDefault 
                        ? generator.Insert(name, withoutPrimaryKeys) 
                        : generator.CombineToWhere(generator.Update(name, withoutPrimaryKeys), toSql.ConvertToSql(primaryKeys));

            return sql;
        }

        /// <summary>
        /// Creates a new unique identifier for unique identifier primary key marked with generator flag.
        /// </summary>
        /// <param name="primaryKeys"> The item. </param>
        /// <param name="properties">  The properties. </param>
        /// <returns>
        /// The new new unique identifier for unique identifier primary key marked with unique identifier
        /// generator flag.
        /// </returns>
        private IList<Property> CreateNewGuidForGuidPrimaryKeyMarkedWithGuidGeneratorFlag(IList<Property> primaryKeys, IList<Property> properties )
        {
            var guids = primaryKeys.Where(p => p.PropertyDescriptor.PropertyType == typeof (Guid)).ToList();

            foreach (var property in guids)
            {
                var attribute = property.PropertyDescriptor.GetAttribute<PrimaryKeyAttribute>();

                if( attribute != null && attribute.Generator == Generator.Guid)
                {
                    var prop = properties.Single(p => p.Name == property.Name);
                    var newId = Guid.NewGuid();
                    prop.Value = newId;

                    prop.PropertyDescriptor.SetValue(prop.Instance, newId);

                    primaryKeys.Remove(property);
                }
            }

            return primaryKeys;
        }

        /// <summary> Gets a runtime default value. </summary>
        /// <param name="type"> The type. </param>
        /// <returns> The runtime default value. </returns>
        private static object GetRuntimeDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        private class CompareProperty : IEqualityComparer<Property>
        {
            /// <summary> Tests if two Property objects are considered equal. </summary>
            /// <param name="x"> Property to be compared. </param>
            /// <param name="y"> Property to be compared. </param>
            /// <returns> true if the objects are considered equal, false if they are not. </returns>
            public bool Equals(Property x, Property y)
            {
                return x.Name == y.Name;
            }

            /// <summary> Calculates the hash code for this object. </summary>
            /// <param name="obj"> The object. </param>
            /// <returns> The hash code for this object. </returns>
            public int GetHashCode(Property obj)
            {
                return 0;
            }
        }
    }
}
