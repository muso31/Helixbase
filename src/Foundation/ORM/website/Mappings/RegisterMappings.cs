using Glass.Mapper.Sc.Pipelines.AddMaps;
using Helixbase.Foundation.ORM.Extensions;

namespace Helixbase.Foundation.ORM.Mappings
{
    public class RegisterMappings : AddMapsPipeline  {
        public void Process(AddMapsPipelineArgs args)
        {
            args.MapsConfigFactory.AddFluentMaps("Helixbase.Foundation.ORM");
        }
    }
}