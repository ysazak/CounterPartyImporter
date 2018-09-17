using System;
using System.Data;
using System.Reflection;

namespace DataImporter.Mapper
{
    public class ComputedPropertyMapping<T> : IPropertyMapping<T>
    {
        private readonly Func<DataRow, object> converter;

        public string PropertyName { get; set; }

        public ComputedPropertyMapping(string propertyName, Func<DataRow, object> converter)
        {
            this.PropertyName = propertyName;
            this.converter = converter;
        }

        public bool TrySetValue(T instance, PropertyInfo property, DataRow dr)
        {
            try
            {
                object value = converter.Invoke(dr);
                property.SetValue(instance, value, null);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
