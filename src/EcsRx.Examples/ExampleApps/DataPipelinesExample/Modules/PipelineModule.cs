using System.Net.Http;
using EcsRx.Examples.ExampleApps.DataPipelinesExample.Pipelines;
using EcsRx.Infrastructure.Dependencies;
using EcsRx.Infrastructure.Extensions;
using EcsRx.Plugins.Persistence.Builders;
using EcsRx.Plugins.Persistence.Extensions;
using LazyData.Json;
using LazyData.Json.Handlers;
using Persistity.Endpoints.Http;
using Persistity.Pipelines;
using Persistity.Pipelines.Builders;

namespace EcsRx.Examples.ExampleApps.DataPipelinesExample.Modules
{
    public class PipelineModule : IDependencyModule
    {
        public void Setup(IDependencyContainer container)
        {
            // By default only the binary stuff is loaded, but you can load json, yaml, bson etc
            container.Bind<IJsonPrimitiveHandler, BasicJsonPrimitiveHandler>();
            container.Bind<IJsonSerializer, JsonSerializer>();
            container.Bind<IJsonDeserializer, JsonDeserializer>();
            
            // Register our custom pipeline using the json stuff above
            container.Bind<PostJsonHttpPipeline>();
        }
    }
}