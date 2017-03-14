using Helixbase.Foundation.Extensions.Commands.Item;
using Assert = Sitecore.Diagnostics.Assert;
using SC = Sitecore;

namespace Helixbase.Foundation.Extensions.Commands.ContentEditor
{
    public class Edit : SC.Shell.Framework.Commands.ContentEditor.Edit
    {
        public override SC.Shell.Framework.Commands.CommandState QueryState(SC.Shell.Framework.Commands.CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            SC.Shell.Framework.Commands.CommandState state = base.QueryState(context);

            if (context.Items.Length == 1 && context.Items[0] != null && state == SC.Shell.Framework.Commands.CommandState.Disabled && CheckIn.CanCheckIn(context.Items[0]))
                return SC.Shell.Framework.Commands.CommandState.Enabled;

            return state;
        }

        protected static new bool CanCheckIn(SC.Data.Items.Item item)
        {
            Assert.ArgumentNotNull(item, "item");
            return CheckIn.CanCheckIn(item);
        }
    }
}
