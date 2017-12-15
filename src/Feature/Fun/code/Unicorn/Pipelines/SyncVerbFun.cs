using Unicorn.ControlPanel.Pipelines.UnicornControlPanelRequest;
using Unicorn.ControlPanel.Responses;
using Unicorn.Logging;

namespace Helixbase.Feature.Fun.Unicorn.Pipelines
{
    public class SyncVerbFun : SyncVerb
    {
        protected override IResponse CreateResponse(UnicornControlPanelRequestPipelineArgs args)
        {
            return new WebConsoleResponseFun("Sync Unicorn", args.SecurityState.IsAutomatedTool,
                new HeadingServiceFun(),
                progress => Process(progress, new WebConsoleLogger(progress, args.Context.Request.QueryString["log"])));
        }
    }
}