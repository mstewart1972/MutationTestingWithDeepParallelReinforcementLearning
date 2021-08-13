using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using MutantCommon;
using Task = System.Threading.Tasks.Task;

namespace MutationTestVS
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class MainToolWindowCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("eccdddaf-6107-4c0b-813a-bef012258e3d");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainToolWindowCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private MainToolWindowCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static MainToolWindowCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in MainToolWindowCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new MainToolWindowCommand(package, commandService);
        }

        /// <summary>
        /// Shows the tool window when the menu item is clicked.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            // Get the instance number 0 of this tool window. This window is single instance so this instance
            // is actually the only one.
            // The last flag is set to true so that if the tool window does not exists it will be created.
            ToolWindowPane window = this.package.FindToolWindow(typeof(MainToolWindow), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException("Cannot create tool window");
            }

            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }

        public void OpenCodeDisplay(IEnumerable<StringSectionModel> codeModel)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            ToolWindowPane window = this.package.FindToolWindow(typeof(OriginalCodeDisplayToolWindow), 0, true); // True means: crate if not found. 0 means there is only 1 instance of this tool window
            if (null == window || null == window.Frame)
                throw new NotSupportedException("Cannot create tool window");

            ((OriginalCodeDisplayToolWindowControl)window.Content).CodeDisplay.SetModel(codeModel);

            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }

        public async Task<string> GetActiveSolutionOutputFileName()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            DTE dte = (DTE)await ServiceProvider.GetServiceAsync(typeof(DTE));
            string outputFileName = String.Empty;
            if (dte == null)
            {
                return String.Empty;
            }
            Project activeProject = null;
            Array activeSolutionProjects;
            try
            {
                activeSolutionProjects = dte.ActiveSolutionProjects as Array;
            }
            catch
            {
                activeSolutionProjects = null;
            }
            if (activeSolutionProjects != null && activeSolutionProjects.Length > 0)
            {
                activeProject = activeSolutionProjects.GetValue(0) as Project;
            }

            if(activeProject != null)
            {
                var properties = new List<string>();
                var values = new List<string>();
                foreach(Property prop in activeProject.Properties)
                {

                    properties.Add(prop.Name);
                    //values.Add(prop.Value.ToString());
                }
                //var allProperties = (List<Property>)activeProject.Properties.GetEnumerator().;
                outputFileName = activeProject.Properties.Item("OutputFileName").Value.ToString();
                var fullPath = activeProject.Properties.Item("FullPath").Value.ToString();
            }

            return outputFileName;
        }

        public async Task<string> GetSolutionPathAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            DTE dte = (DTE) await ServiceProvider.GetServiceAsync(typeof(DTE));
            Solution solution = null;
            if(dte == null)
            {
                return String.Empty;
            }

            Project activeProject = null;
            solution = dte.Solution;

            if(solution == null)
            {
                return String.Empty;
            }
            return dte.Solution.FileName;
        } 
    }
}
