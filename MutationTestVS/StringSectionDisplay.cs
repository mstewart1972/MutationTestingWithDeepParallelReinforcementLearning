using MutantCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace MutationTestVS
{
    public class StringSectionDisplay:Run
    {
        private readonly StringSectionModel model;
        public Brush mutantHighlighter { get; set; }
        public StringSectionDisplay(StringSectionModel model, Brush mutantHighlighter) : base(model.BaseString)
        {
            this.model = model;
            if (model.HasDiffs)
            {
                Background = mutantHighlighter;
                ToolTip = new MutantsPopup(model);
            }
        }
    }
}
