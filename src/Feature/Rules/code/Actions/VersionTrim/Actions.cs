namespace Helixbase.Feature.Rules.Actions.VersionTrim
{
    using System;
    using Assert = Sitecore.Diagnostics.Assert;
    using SC = Sitecore;

    public abstract class MinVersionsAction<T> :
    SC.Rules.Actions.RuleAction<T> where T : SC.Rules.RuleContext
    {
        /// <summary>
        /// Backs the MinVersions property.
        /// </summary>
        private int _minVersions;

        /// <summary>
        /// Gets or sets a value that indicates 
        /// the minimum number of versions to retain. 
        /// Set by rule parameters, or defaults to 30. 
        /// </summary>
        public int MinVersions
        {
            get
            {
                return this._minVersions < 1 ? 30 : this._minVersions;
            }

            set
            {
                this._minVersions = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates the minimum age of versions to 
        /// remove, in days. Actions that derive from this abstract base class
        /// will not process Versions updated within this number of days.
        /// </summary>
        public int MinUpdatedDays { get; set; }

        /// <summary>
        /// Apply the rule.
        /// </summary>
        /// <param name="ruleContext">Rule processing context.</param>
        public override void Apply(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");
            Assert.ArgumentNotNull(ruleContext.Item, "ruleContext.Item");

            // for each language available in the item
            foreach (SC.Globalization.Language lang in ruleContext.Item.Languages)
            {
                SC.Data.Items.Item item = ruleContext.Item.Database.GetItem(
                  ruleContext.Item.ID,
                  lang);

                if (item == null)
                {
                    continue;
                }

                // to prevent the while loop from reaching MinVersions,
                // only process this number of items
                int limit = item.Versions.Count - this.MinVersions;
                int i = 0;

                while (item.Versions.Count > this.MinVersions && i < limit)
                {
                    SC.Data.Items.Item version = item.Versions.GetVersions()[i++];
                    Assert.IsNotNull(version, "version");

                    if (this.MinUpdatedDays < 1
                      || version.Statistics.Updated.AddDays(this.MinUpdatedDays) < DateTime.Now)
                    {
                        this.HandleVersion(version);
                    }
                }
            }
        }

        /// <summary>
        /// Classes that derive from this abstract base class
        /// implement this method to remove versions.
        /// </summary>
        /// <param name="version">The version to process.</param>
        public abstract void HandleVersion(Sitecore.Data.Items.Item version);
    }

    /// <summary>
    /// Rules engine action that recycles old versions of items.
    /// </summary>
    public class RecycleOldVersions<T> :
      MinVersionsAction<T> where T : SC.Rules.RuleContext
    {
        /// <summary>
        /// Recycle the old version.
        /// </summary>
        /// <param name="version">The old version to recycle.</param>
        public override void HandleVersion(SC.Data.Items.Item version)
        {
            Assert.ArgumentNotNull(version, "version");
            SC.Diagnostics.Log.Audit(
              this,
              "Recycle version : {0}",
              new string[] { SC.Diagnostics.AuditFormatter.FormatItem(version) });
            version.RecycleVersion();
        }
    }

    /// <summary>
    /// Rules engine action that archives old versions of items.
    /// </summary>
    public class ArchiveOldVersions<T> :
      MinVersionsAction<T> where T : SC.Rules.RuleContext
    {
        /// <summary>
        /// Archives the old version.
        /// </summary>
        /// <param name="version">The old version to archive.</param>
        public override void HandleVersion(SC.Data.Items.Item version)
        {
            Assert.ArgumentNotNull(version, "version");
            SC.Data.Archiving.Archive archive = version.Database.Archives["archive"];

            if (archive != null)
            {
                SC.Diagnostics.Log.Audit(
                  new object(),
                  "Archive version: {0}",
                  new string[] { SC.Diagnostics.AuditFormatter.FormatItem(version) });
                archive.ArchiveVersion(version);
            }
            else
            {
                SC.Diagnostics.Log.Audit(
                  this,
                  "Recycle version : {0}",
                  new string[] { SC.Diagnostics.AuditFormatter.FormatItem(version) });
                version.RecycleVersion();
            }
        }
    }

    /// <summary>
    /// Rules engine action that deletes old versions of items.
    /// </summary>
    public class DeleteOldVersions<T> : MinVersionsAction<T> where T : SC.Rules.RuleContext
    {
        /// <summary>
        /// Deletes the old version.
        /// </summary>
        /// <param name="version">The old version to delete.</param>
        public override void HandleVersion(SC.Data.Items.Item version)
        {
            Assert.ArgumentNotNull(version, "version");
            SC.Diagnostics.Log.Audit(
              this,
              "Delete version : {0}",
              new string[] { SC.Diagnostics.AuditFormatter.FormatItem(version) });
            version.Versions.RemoveVersion();
        }
    }
}