using API.Setup;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace TestesUnitarios
{
    public abstract class BaseMapperTest
    {
        public readonly IMapper Mapper;
        protected BaseMapperTest()
        {
            Mapper = new AutoMapperFixture().GetMapper();
        }

        public class AutoMapperFixture : IDisposable
        {
            public IMapper GetMapper()
            {
                var serviceCollection = new ServiceCollection();
                AutoMapperConfig.RegisterAutoMapper(serviceCollection);
                return AutoMapperConfig.autoMapperConfig.CreateMapper();
            }
            public void Dispose()
            {
                //throw new NotImplementedException();
            }
        }
    }
}