using Sitecore.Diagnostics;
using Sitecore.Security.Accounts;
using Sitecore.Security.Authentication;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Shell.Framework.Commands.UserManager;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Sitecore.Web.UI.XamlSharp.Continuations;
using System.Collections.Specialized;

namespace Helixbase.Extensions.UserManager.Commands.UserManager
{
    public class LoginAsUser : Command, ISupportsContinuation
    {
        public LoginAsUser()
        {
        }

        /// <summary>
        /// Executes the command in the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");

            var item = context.Parameters["username"];

            if (!ValidationHelper.ValidateUserWithMessage(item))
                return;

            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection["username"] = item;

            ClientPipelineArgs clientPipelineArg = new ClientPipelineArgs(nameValueCollection);
            ContinuationManager.Current.Start(this, "Run", clientPipelineArg);
        }

        /// <summary>
        /// Queries the state of the command.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override CommandState QueryState(CommandContext context)
        {
            Assert.ArgumentNotNull(context, "context");

            return base.QueryState(context);
        }

        /// <summary>
        /// Runs the pipeline.
        /// </summary>
        /// <param name="args">The args.</param>
        protected static void Run(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            ListString listStrings = new ListString(args.Parameters["username"]);

            if (args.IsPostBack)
            {
                AjaxScriptManager.Current.Dispatch("usermanager:refresh");
                return;
            }

            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                var username = listStrings[0];

                if (User.Exists(username))
                {
                    AuthenticationManager.Logout();
                    AuthenticationManager.Login(username, true);

                    AjaxScriptManager.Current.SetLocation("/sitecore/client/Applications/Launchpad");
                }
            }
        }
    }
}
