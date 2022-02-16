using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore;
using Sitecore.Data.Events;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Events;
using Sitecore.Globalization;
using Sitecore.Shell.Applications.Dialogs.ProgressBoxes;
using Sitecore.Web.UI.Sheer;

namespace Headlixbase.Feature.ItemUnlock.Events
{
    public class RemovedUserEventHandler
    {
        public void OnUserDeletedUnlockFiles(object sender, EventArgs args)
        {
            if (EventDisabler.IsActive)
                return;

            var objList = new List<Item>();
            var userName = Event.ExtractParameter<string>(args, 0);

            Assert.IsNotNullOrEmpty(userName, "User name was null or empty");

            var lockedItems = Client.ContentDatabase.SelectItems($"search://*[@__lock='%{userName}%']");

            if (lockedItems == null || !lockedItems.Any())
                return;

            foreach (var lockedItem in lockedItems)
                objList.AddRange(lockedItem.Versions.GetVersions(true).Where(version =>
                    string.Compare(version.Locking.GetOwner(), userName, StringComparison.OrdinalIgnoreCase) == 0));

            ProgressBox.Execute(nameof(RemovedUserEventHandler), "Unlocking items", "Network/16x16/lock.png",
                UnlockAllItems, "lockeditems:refresh", Context.User, objList);

            SheerResponse.Alert($"Successfully unlocked {objList.Count} item(s) checked out by {userName}",
                Array.Empty<string>());
        }

        private static void UnlockAllItems(params object[] parameters)
        {
            Assert.ArgumentNotNull(parameters, nameof(parameters));

            if (!(parameters[0] is List<Item> parameter))
                return;

            var job = Context.Job;

            if (job != null)
                job.Status.Total = parameter.Count;

            foreach (var lockedItem in parameter)
            {
                job?.Status.Messages.Add(Translate.Text("Unlocking {0}", (object)lockedItem.Paths.ContentPath));
                lockedItem.Locking.Unlock();
                if (job != null)
                    ++job.Status.Processed;
            }
        }
    }
}
