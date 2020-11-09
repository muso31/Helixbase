using Helixbase.Feature.ItemUnlock.Platform.Commands.Item;
using System;
using Assert = Sitecore.Diagnostics.Assert;
using SC = Sitecore;

namespace Helixbase.Feature.ItemUnlock.Platform.Pipelines.GetContentEditorWarnings
{
    /// <summary>
    ///     https://community.sitecore.net/technical_blogs/b/sitecorejohn_blog/posts/allow-users-to-unlock-items-locked-to-others-in-the-sitecore-asp-net-cms
    /// </summary>
    public class IsLocked
    {
        public void Process(SC.Pipelines.GetContentEditorWarnings.GetContentEditorWarningsArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            SC.Data.Items.Item item = args.Item;

            if (item == null)
                return;

            if (SC.Context.IsAdministrator || CheckIn.CanCheckIn(item))
            {
                if (item.Locking.IsLocked() && (string.Compare(item.Locking.GetOwner(), SC.Context.User.Name, StringComparison.InvariantCultureIgnoreCase) != 0))
                {
                    SC.Pipelines.GetContentEditorWarnings.GetContentEditorWarningsArgs.ContentEditorWarning warning = args.Add();
                    warning.Title = SC.Globalization.Translate.Text("'{0}' has locked this item.", new object[] { item.Locking.GetOwnerWithoutDomain() });
                    warning.AddOption(SC.Globalization.Translate.Text("Check In"), string.Format("item:checkin(id={0},language={1},version={2})", item.ID, item.Language, item.Version.Number));
                }
            }
            else if (item.Locking.IsLocked())
            {
                if (!item.Locking.HasLock())
                {
                    args.Add(SC.Globalization.Translate.Text("You cannot edit this item because '{0}' has locked it.", new object[] { item.Locking.GetOwnerWithoutDomain() }), string.Empty);
                }
            }
            else if (SC.Configuration.Settings.RequireLockBeforeEditing && SC.Data.Managers.TemplateManager.IsFieldPartOfTemplate(SC.FieldIDs.Lock, item))
            {
                SC.Pipelines.GetContentEditorWarnings.GetContentEditorWarningsArgs.ContentEditorWarning warning = args.Add();
                warning.Title = SC.Globalization.Translate.Text("You must lock this item before you can edit it.");
                warning.Text = SC.Globalization.Translate.Text("To lock this item, click Edit on the Home tab.");
                warning.AddOption(SC.Globalization.Translate.Text("Lock and Edit"), "item:checkout");
            }
        }
    }
}
