using System;
using System.Diagnostics;
using System.Globalization;
using System.Timers;
using Kamsar.WebConsole;
using Sitecore.SecurityModel;
using Unicorn.ControlPanel.Responses;

namespace Helixbase.Feature.Fun.Unicorn.Pipelines
{
    /// <summary>
    /// https://github.com/SitecoreUnicorn/Unicorn/blob/master/src/Unicorn/ControlPanel/Responses/WebConsoleResponse.cs
    /// </summary>
    public class WebConsoleResponseFun : WebConsoleResponse
    {
        private readonly string _title;
        private readonly Action<IProgressStatus> _processAction;
        private readonly bool _isAutomatedTool;
        private readonly HeadingServiceFun _headingService;

        public WebConsoleResponseFun(string title, bool isAutomatedTool, HeadingServiceFun headingService, Action<IProgressStatus> processAction) : base(title, isAutomatedTool, headingService, processAction)
        {
            _title = title;
            _isAutomatedTool = isAutomatedTool;
            _headingService = headingService;
            _processAction = processAction;
        }

        protected override void ProcessInternal(IProgressStatus progress)
        {
            if (_headingService != null && !_isAutomatedTool)
            {
                progress.ReportStatus(_headingService.GetHeadingHtml());
            }

            // note: these logs are intentionally to progress and not loggingConsole as we don't need them in the Sitecore logs

            progress.ReportTransientStatus("Executing.");

            var heartbeat = new Timer(3000);

            var timer = new Stopwatch();
            timer.Start();

            heartbeat.AutoReset = true;
            heartbeat.Elapsed += (sender, args) =>
            {
                var elapsed = Math.Round(timer.ElapsedMilliseconds / 1000d);

                try
                {
                    progress.ReportTransientStatus("Executing for {0} sec.", elapsed.ToString(CultureInfo.InvariantCulture));
                }
                catch
                {
                        // e.g. HTTP connection disconnected - prevent infinite looping
                        heartbeat.Stop();
                }
            };

            heartbeat.Start();

            try
            {
                using (new SecurityDisabler())
                {
                    _processAction(progress);
                }
            }
            finally
            {
                heartbeat.Stop();
            }

            timer.Stop();

            progress.Report(100);
            progress.ReportTransientStatus("Operation completed.");
            progress.ReportStatus(_isAutomatedTool ? "\r\n" : "<br>");
            progress.ReportStatus(_isAutomatedTool ? $"Completed in {timer.ElapsedMilliseconds}ms." : $"Operation completed in {timer.ElapsedMilliseconds}ms. Want to <a href=\"?verb=\">return to the control panel?</a>");
        }

    }
}