using Helixbase.Feature.ItemUnlock.Platform.Commands.Item;
using Assert = Sitecore.Diagnostics.Assert;
using SC = Sitecore;

namespace Helixbase.Feature.ItemUnlock.Platform.Commands.ContentEditor
{
    /// <summary>
    ///     https://community.sitecore.net/technical_blogs/b/sitecorejohn_blog/posts/allow-users-to-unlock-items-locked-to-others-in-the-sitecore-asp-net-cms
    /// </summary>
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
