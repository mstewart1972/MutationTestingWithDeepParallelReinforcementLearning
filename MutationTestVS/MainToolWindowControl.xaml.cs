namespace MutationTestVS
{
    using EnvDTE;
    using MutantCommon;
    using MutantTester;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainToolWindowControl.
    /// </summary>
    public partial class MainToolWindowControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainToolWindowControl"/> class.
        /// </summary>
        public MainToolWindowControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "MainToolWindow");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Run_Button_Click(object sender, RoutedEventArgs e)
        {
            RunButton.IsEnabled = false;
            CancelButton.IsEnabled = true;
            var input = inputParameters;
            mutationTester = MutationTester.CreateMutationTester(input);
            var progress = new Progress<MutationTestingStateModel>();
            progress.ProgressChanged += OnMutationTestingProgress;
            //mutationTester.MutationTest(progress);
            await RunTestingAsync(progress);
            CancelButton.IsEnabled = false;
            RunButton.IsEnabled = true;
        }

        private async void GetInputParameters_Button_Click(object sender, RoutedEventArgs e)
        {
            InputParameters input;
            try
            {
                input = await GetInputAsync();
            }
            catch
            {
                input = null;
            }
            if (input == null || String.IsNullOrEmpty(input.SourceFileName) || String.IsNullOrEmpty(input.SolutionPath))
            {
                RunButton.IsEnabled = false;
            }
            else
            {
                inputParameters = input;
                SolutionPathLabel.Content = input.SolutionPath;
                CompiledSourceFilenameLabel.Content = input.SourceFileName;
                RunButton.IsEnabled = true;
            }
        }

        private async Task RunTestingAsync(IProgress<MutationTestingStateModel> progress)
        {
            await mutationTester.MutationTest(progress).ConfigureAwait(false);
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            mutationTester.Cancel();
            RunButton.IsEnabled = true;
            CancelButton.IsEnabled = false;
        }

        private void Show_Mutants_Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            var selectedClass = ClassSelectionBox.SelectedItem as Class;
            
            IList<StringSectionModel> selectedDiff;
            if (selectedClass != null && diffs.TryGetValue(selectedClass, out selectedDiff))
            {
                MainToolWindowCommand.Instance.OpenCodeDisplay(selectedDiff);
            }
        }

        private InputParameters inputParameters;
        private IEnumerable<MutantModel> mutants = new List<MutantModel>();
        private IMutationTester mutationTester;
        private IEnumerable<StringSectionModel> mutantSections = new List<StringSectionModel>();/*
        {
            new StringSectionModel{ BaseString = "Cake\n\n\tYes Cake", Prefixes = new Dictionary<string, string>(), Alternatives = new Dictionary<string, string>() },
            new StringSectionModel{ BaseString = "Cake Yes Cake", Prefixes = new Dictionary<string, string>{ { "1","N"} }, Alternatives = new Dictionary<string, string>() },
            new StringSectionModel{ BaseString = "Cake\n\n\tYes Cake", Prefixes = new Dictionary<string, string>(), Alternatives = new Dictionary<string, string>() },
            new StringSectionModel{ BaseString = "Cake Yes Cake", Prefixes = new Dictionary<string, string>(), Alternatives = new Dictionary<string, string>{ { "2", "W" } } },
            new StringSectionModel{ BaseString = "Cake\n\n\tYes Cake", Prefixes = new Dictionary<string, string>(), Alternatives = new Dictionary<string, string>() },

        };*/

        private IDictionary<Class, IList<StringSectionModel>> diffs = new Dictionary<Class, IList<StringSectionModel>>();

        private void OnMutationTestingProgress(object sender, MutationTestingStateModel state)
        {
            MutantCountValueLabel.Content = state.TotalMutants.ToString();
            ProgressBar.Value = state.PercentComplete * 100;
            CurrentActivityLabel.Content = state.CurrentOperation.ToString();
            MutantKilledCountValueLabel.Content = state.KilledMutants.ToString();
            if (state.TotalMutants > 0)
            { MutationScoreValueLabel.Content = $"{state.KilledMutants}/{state.TotalMutants}={Convert.ToSingle(state.KilledMutants / state.TotalMutants)}"; }
            else
            { MutationScoreValueLabel.Content = $"{state.KilledMutants}/{state.TotalMutants}"; };
            MutationScoreBar.Value = state.MutationScore * 100;

            diffs = state.Diffs;
            ClassSelectionBox.ItemsSource = diffs.Keys;
        }

        private void UpdateShowMutantsButtonIsEnabled()
        {
            var selectedClass = ClassSelectionBox.SelectedItem as Class;

            IList<StringSectionModel> selectedDiff;
            if (selectedClass != null && diffs.TryGetValue(selectedClass, out selectedDiff))
            {

                ShowMutantsButton.IsEnabled = true;
            }
            else
            {
                ShowMutantsButton.IsEnabled = false;
            }
        }

        private void OnClassSelect(object obj, RoutedEventArgs e)
        {
            UpdateShowMutantsButtonIsEnabled();
        }

        private async Task<InputParameters> GetInputAsync()
        {
            var mainToolWindow = MainToolWindowCommand.Instance;
            var solutionPath = await mainToolWindow.GetSolutionPathAsync();
            var tempFolder = System.IO.Path.GetTempPath();
            var workingDir = System.IO.Path.Combine(tempFolder, "MutationTesting");
            var reportDir = System.IO.Path.Combine(tempFolder, "MutationTestingReport");
            var sourceFilename = await mainToolWindow.GetActiveSolutionOutputFileName();
            var input = new InputParameters();
            input.ReportFolder = reportDir;//"C:\\Users\\Angus\\Coursework\\ReportFolder";
            input.SolutionPath = solutionPath;//"C:\\Users\\Angus\\Coursework\\fourth-year-project\\Source\\TestSolution\\TestSolution.sln";
            input.SourceFileName = sourceFilename;//"ClassLibrary.dll";
            input.TestFileName = "XUnitTestProject.dll";
            input.WorkingDirectory = workingDir;//"C:\\Users\\Angus\\Coursework\\fourth-year-project\\Source\\slnMutantTesting\\MutantTestCmdLine\\bin\\Debug";
            input.BuildSolution = true;
            input.ShouldRun = true;
            input.Verbose = false;
            input.TestBatchSize = 50;
            return input;
        }

        private void cbOperator_CheckedChanged(object sender, RoutedEventArgs e)
        {

        }

        private void cbAllOperators_CheckedChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}