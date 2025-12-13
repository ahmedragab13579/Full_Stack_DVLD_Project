using System.Collections.Generic;

namespace DVLD_Application.Services.Interfaces.Mapping
{
    public interface IMappingServices
    {
        TDestination Map<TSource, TDestination>(TSource source);

        IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source);

        void Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
