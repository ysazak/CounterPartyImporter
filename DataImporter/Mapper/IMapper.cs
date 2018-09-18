using System.Collections.Generic;

namespace DataImporter.Mapper
{
    public interface IMapper<TTarget, TSource> where TTarget : class, new()
    {
        IEnumerable<TTarget> Map(IEnumerable<IPropertyMapping<TTarget>> mappings, TSource source);

        bool ValidateMappingProperties(IEnumerable<IPropertyMapping<TTarget>> mappings);
    }
}