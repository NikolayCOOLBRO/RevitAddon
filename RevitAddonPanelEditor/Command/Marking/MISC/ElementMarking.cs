using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddonPanelEditor.Command.Marking.Entity
{
    public class ElementMarking
    {
        public readonly FamilyInstance Element;

        public int SequenceNumber { get; set; }

        public int NumberCombination { get; set; }

        public ElementMarking(FamilyInstance instace)
        {
            Element = instace;
        }
    }
}
