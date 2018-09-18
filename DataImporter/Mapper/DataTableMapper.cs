using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DataImporter.Mapper
{
    public class DataTableMapper<TTarget> : IMapper<TTarget, DataTable> where TTarget : class, new()
    {
        private IEnumerable<PropertyInfo> GetProperties(IEnumerable<IPropertyMapping<TTarget>> mappings, Func<IPropertyMapping<TTarget>, string, bool> condition)
        {
            return GetProperties().Where(property => property.CanWrite && mappings.Any(m => condition.Invoke(m, property.Name))).ToList();
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            var objType = typeof(TTarget);
            return objType.GetProperties().Where(property => property.CanWrite).ToList();
        }

        public IEnumerable<TTarget> Map(IEnumerable<IPropertyMapping<TTarget>> mappings, DataTable source)
        {
            try
            {
                Func<IPropertyMapping<TTarget>, string, bool> whereClause = (mapping, propertyName) => mapping.PropertyName == propertyName;

                IEnumerable<PropertyInfo> properties = GetProperties(mappings, whereClause);

                var list = new List<TTarget>(source.Rows.Count);

                foreach (var dr in source.AsEnumerable())
                {
                    var instance = new TTarget();

                    foreach (var property in properties)
                    {
                        var propType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                        var propMapping = mappings.FirstOrDefault(m => m.PropertyName == property.Name);
                        if (propMapping == null)
                        {
                            throw new MappingException("Property not found", property.Name);
                        }

                        if (!propMapping.TrySetValue(instance, property, dr))
                        {
                            throw new MappingException("Failure on Assigning value", property.Name);
                        }
                    }

                    list.Add(instance);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new MappingException(ex);
            }
        }

        public bool ValidateMappingProperties(IEnumerable<IPropertyMapping<TTarget>>  mappings)
        {
            var properties = GetProperties();
            foreach (var item in mappings)
            {
                PropertyInfo propInfo = properties.FirstOrDefault(p => p.Name == item.PropertyName);
                if (propInfo == null)
                {
                    return false;
                }
            }
            return true;
        }
    }

}
