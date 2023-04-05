using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddonPanelEditor.Command.Marking.Collector
{
    public class CollecttorFamilyInstaceAndHost
    {
        public readonly string Host;

        public List<FamilyInstance> Objects;

        public CollecttorFamilyInstaceAndHost(string host)
        {
            Host = host;
            Objects = new List<FamilyInstance>();
        }
    }
}
