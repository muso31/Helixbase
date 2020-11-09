using Glass.Mapper.Sc.Pipelines.AddMaps;
using Helixbase.Foundation.ORM.Platform.Extensions;

namespace Helixbase.Foundation.ORM.Platform.Mappings
{
    public class RegisterMappings : AddMapsPipeline  {
        public void Process(AddMapsPipelineArgs args)
        {
            args.MapsConfigFactory.AddFluentMaps("Helixbase.Foundation.ORM.Platform");
        }
    }
}
