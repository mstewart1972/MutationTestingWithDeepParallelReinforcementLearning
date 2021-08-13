using MutantCommon;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace MutationTestVS
{
    /// <summary>
    /// Interaction logic for MutantsPopup.xaml.
    /// </summary>
    [ProvideToolboxControl("MutationTestVS.MutantsPopup", true)]
    public partial class MutantsPopup : UserControl
    {
        private const string HeaderTextFormat = "Mutants of \"{0}\"";

        public MutantsPopup(StringSectionModel model)
        {
            InitializeComponent();
            AlternativesHeader.Header = String.Format(HeaderTextFormat, model.BaseString);
            foreach (var prefix in model.Prefixes.Values)
            {
                MutantAlternativesList.Items.Add(new { Alternative = prefix + model.BaseString });
            }
            foreach (var replacement in model.Alternatives.Values)
            {
                MutantAlternativesList.Items.Add(new { Alternative = replacement });
            }
        }
    }
}
