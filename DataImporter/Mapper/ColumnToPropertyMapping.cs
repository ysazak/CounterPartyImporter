using System;
using System.Data;
using System.Reflection;

namespace DataImporter.Mapper
{
    public class ColumnToPropertyMapping<T> : IPropertyMapping<T>
    {
        public string PropertyName { get; set; }
        private readonly string columnName;

        public ColumnToPropertyMapping(string propertyName, string columnName)
        {
            this.PropertyName = propertyName;
            this.columnName = columnName;
        }

        public bool TrySetValue(T instance, PropertyInfo property, DataRow dr)
        {
            try
            {
                var safeValue = dr[columnName] == null ? null : Convert.ChangeType(dr[columnName], property.PropertyType);

                property.SetValue(instance, safeValue, null);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
