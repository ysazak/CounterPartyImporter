using System.Data;
using System.Reflection;

namespace DataImporter.Mapper
{
    public interface IPropertyMapping<T>
    {
        string PropertyName { get; set; }

        bool TrySetValue(T instance, PropertyInfo property, DataRow dr);
    }
}