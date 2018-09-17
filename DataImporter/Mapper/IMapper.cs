using System.Collections.Generic;

namespace DataImporter.Mapper
{
    public interface IMapper<TTarget, TSource> where TTarget : class, new()
    {
        IEnumerable<TTarget> Map(TSource source);

        bool ValidateMappingProperties();
    }
}