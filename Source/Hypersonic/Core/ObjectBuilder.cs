using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Hypersonic.Core.Extensions;

namespace Hypersonic.Core
{
    public class ObjectBuilder
    {
        private readonly HypersonicSettings _settings;

        public ObjectBuilder(HypersonicSettings settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// Hydrates the type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public T HydrateType<T>(IHypersonicDbReader reader) where T : class, new()
        {
            var instance = new T();
            return HydrateType(reader, instance) as T;
        }

        /// <summary> Hydrate type. </summary>
        /// <param name="reader">   The reader. </param>
        /// <param name="instance"> The instance. </param>
        /// <returns> . </returns>
        private object HydrateType(IHypersonicDbReader reader, object instance)
        {
            var flattener = new Flattener(_settings);
            var namesAndValues = flattener.GetPropertiesWithDefaultValues(instance);

            Populate(reader, namesAndValues);
            return instance;
        }

        /// <summary> Hydrate type. </summary>
        /// <param name="type">   The type. </param>
        /// <param name="reader"> The reader. </param>
        /// <returns> . </returns>
        public object HydrateType(Type type, IHypersonicDbReader reader)
        {
            var instance = Activator.CreateInstance(type);
            var materialized = HydrateType(reader, instance);

            //TODO:Class Materialized interception

            return materialized;
        }

        /// <summary> Populates. </summary>
        /// <param name="reader">     The reader. </param>
        /// <param name="properties"> The properties. </param>
        private static void Populate(IHypersonicDbReader reader, IEnumerable<Property> properties)
        {
            foreach (var property in properties.Where(p => !p.Instance.IsCollection()))
            {
                var propertyName = property.Name;
                try
                {
                    var value = reader.GetValue(property.Name);
                    value = (value != DBNull.Value ? value : null);

                    value = ConvertToProperType(value, property);
                    property.PropertyDescriptor.SetValue(property.Instance, value);

                }
                catch (IndexOutOfRangeException)
                {
                    Debug.WriteLine(string.Format("Property Name {0} was not found in the results", propertyName));
                }
            }
        }

        /// <summary> Converts this object to a proper type. </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the requested operation is invalid. </exception>
        /// <param name="value">    The value. </param>
        /// <param name="property"> The property. </param>
        /// <returns> The given data converted to a proper type. </returns>
        private static object ConvertToProperType(object value, Property property)
        {
            //TODO:Intercept Data Property Materialize here.
            
            if (property.PropertyDescriptor.PropertyType.IsEnum)
            {
                value = Enum.Parse(property.PropertyDescriptor.PropertyType, Convert.ToString(value), true);
            }

            if (property.PropertyDescriptor.PropertyType.IsValueType && value == null && Nullable.GetUnderlyingType(property.PropertyDescriptor.PropertyType) == null)
            {
                throw new InvalidOperationException(string.Format("Can't cast the property {0}, which is of type '{1}', to a 'null' value, consider using a Nullable type", property.Name, property.PropertyDescriptor.PropertyType.Name));
            }

            if (value != null && value.GetType() != property.PropertyDescriptor.PropertyType)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(property.PropertyDescriptor.PropertyType);

                try
                {
                    Type propType = property.PropertyDescriptor.PropertyType;

                    value = propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>)
                         ? Convert.ChangeType(value, Nullable.GetUnderlyingType(propType))
                         : converter.ConvertTo(value, propType);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(string.Format("Can't cast the property {0}, which is of type '{1}' to type of {2}.", property.Name, property.PropertyDescriptor.PropertyType.Name, value.GetType().Name), ex);
                }

            }

            return value;
        }
    }
}
