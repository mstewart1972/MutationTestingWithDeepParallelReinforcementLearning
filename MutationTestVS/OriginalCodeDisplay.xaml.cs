using MutantCommon;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace MutationTestVS
{
    /// <summary>
    /// Interaction logic for OriginalCodeDisplay.xaml.
    /// </summary>
    [ProvideToolboxControl("MutationTestVS.OriginalCodeDisplay", true)]
    public partial class OriginalCodeDisplay : UserControl
    {
        private Brush brush = new SolidColorBrush(Colors.PaleVioletRed);

        //Does not appear if this constructor is used
        public OriginalCodeDisplay()
        {
            InitializeComponent();
        }

        public void SetModel(IEnumerable<StringSectionModel> codeSectionModels)
        {
            var paragraph = new Paragraph();
            Run inline;
            foreach (var section in codeSectionModels)
            {
                inline = new StringSectionDisplay(section, brush);
                paragraph.Inlines.Add(inline);
            }
            var document = new FlowDocument(paragraph);
            DocumentReader.Document = document;
        }

        public void SetHighlightBrush(Brush highlighter)
        {
            brush = highlighter;
        }



    }
}
