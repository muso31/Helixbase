using Glass.Mapper.Sc.Pipelines.AddMaps;
using Helixbase.Foundation.ORM.Extensions;

namespace Helixbase.Feature.Redirects.ORM
{
    public class RegisterMappings : AddMapsPipeline  {
        public void Process(AddMapsPipelineArgs args)
        {
            args.MapsConfigFactory.AddFluentMaps("Helixbase.Feature.Redirects");
        }
    }
}