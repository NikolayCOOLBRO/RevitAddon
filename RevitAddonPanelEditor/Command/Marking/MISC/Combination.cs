using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddonPanelEditor.Command.Marking.Entity
{
    public class Combination
    {
        public readonly Guid Id;

        public int Number { get; set; }

        public int GlobalCombination { get; set; }

        public List<ElementMarking> Elements { get; private set; }

        public Combination()
        {
            Id = Guid.NewGuid();
            Elements = new List<ElementMarking>();
        }

        public void Add(ElementMarking elementMarking)
        {

            Elements.Add(elementMarking);
        }
    }
}
