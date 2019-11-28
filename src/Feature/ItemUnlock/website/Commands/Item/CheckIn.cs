using Assert = Sitecore.Diagnostics.Assert;
using SC = Sitecore;

namespace Helixbase.Feature.ItemUnlock.Commands.Item
{
    /// <summary>
    ///     https://community.sitecore.net/technical_blogs/b/sitecorejohn_blog/posts/allow-users-to-unlock-items-locked-to-others-in-the-sitecore-asp-net-cms
    /// </summary>
    public class CheckIn : SC.Shell.Framework.Commands.CheckIn
    {
        public static bool HasUnlockAccess(SC.Data.Items.Item item, SC.Security.Accounts.User user = null)
        {
            Assert.ArgumentNotNull(item, "item");

            if (user == null)
            {
                user = SC.Context.User;
                Assert.IsNotNull(user, "context user");
            }

            if (user.IsAdministrator)
                return true;

            SC.Security.AccessControl.AccessRight checkIn = SC.Security.AccessControl.AccessRight.FromName("item:checkin");

            if (checkIn != null)
                return SC.Security.AccessControl.AuthorizationManager.IsAllowed(item, checkIn, user);

            SC.Security.Accounts.Role control = SC.Security.Accounts.Role.FromName("sitecore\\Sitecore Client Maintaining");

            return SC.Security.Accounts.Role.Exists(control.Name) && SC.Security.Accounts.RolesInRolesManager.IsUserInRole(SC.Context.User, control, true /*includeIndirectMembership*/);
        }

        public static bool CanCheckIn(SC.Data.Items.Item item, SC.Security.Accounts.User user = null)
        {
            Assert.ArgumentNotNull(item, "item");

            if (user == null)
            {
                user = SC.Context.User;
                Assert.IsNotNull(user, "context user");
            }

            if (item.Appearance.ReadOnly || !item.Locking.IsLocked() || !item.Access.CanWrite() || !item.Access.CanWriteLanguage())
                return false;

            return user.IsAdministrator
                   || string.Compare(item.Locking.GetOwner(), user.Name, System.StringComparison.OrdinalIgnoreCase) == 0
                   || CheckIn.HasUnlockAccess(item);
        }

        public override SC.Shell.Framework.Commands.CommandState QueryState(SC.Shell.Framework.Commands.CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");

            if (context.Items.Length != 1)
                return base.QueryState(context);

            SC.Data.Items.Item item = context.Items[0];
            Assert.ArgumentNotNull(item, "context.Items[0]");

            if (SC.Context.IsAdministrator || !CheckIn.CanCheckIn(item))
                return base.QueryState(context);

            if (!item.Locking.IsLocked())
                return SC.Shell.Framework.Commands.CommandState.Hidden;

            if (CanCheckIn(item))
                return SC.Shell.Framework.Commands.CommandState.Enabled;

            return base.QueryState(context);
        }

        protected new void Run(SC.Web.UI.Sheer.ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            if (!SC.Web.UI.Sheer.SheerResponse.CheckModified())
                return;

            SC.Data.Items.Item item = SC.Client.GetItemNotNull(args.Parameters["id"], SC.Globalization.Language.Parse(args.Parameters["language"]), SC.Data.Version.Parse(args.Parameters["version"]));

            if (!(item.Locking.HasLock() || SC.Context.IsAdministrator || CheckIn.HasUnlockAccess(item)))
                return;

            SC.Diagnostics.Log.Audit(this, "Check in: {0}", new string[] { SC.Diagnostics.AuditFormatter.FormatItem(item) });

            using (new SC.Data.Items.EditContext(item, SC.SecurityModel.SecurityCheck.Disable))
                item.Locking.Unlock();

            SC.Context.ClientPage.SendMessage(this, "item:checkedin");
        }
    }
}