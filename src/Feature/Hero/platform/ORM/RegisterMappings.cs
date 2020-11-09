using Glass.Mapper.Sc.Pipelines.AddMaps;
using Helixbase.Foundation.ORM.Platform.Extensions;

namespace Helixbase.Feature.Hero.Platform.ORM
{
    public class RegisterMappings : AddMapsPipeline  {
        public void Process(AddMapsPipelineArgs args)
        {
            args.MapsConfigFactory.AddFluentMaps("Helixbase.Feature.Hero.Platform");
        }
    }
}
