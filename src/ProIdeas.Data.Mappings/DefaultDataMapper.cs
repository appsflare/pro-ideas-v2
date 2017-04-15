using AutoMapper;
using ProIdeas.DataMappings.Data.Mappings.Contracts;

namespace ProIdeas.Data.Mappings
{
    internal class DefaultDataMapper : IDataMapper
    {
        private readonly IMapper _mapper;
        public DefaultDataMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }
    }
}
