using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddonPanelEditor.Command.Marking.Entity
{
    public class Cluster
    {
        public List<Combination> Combinations { get; private set; }

        public Cluster(List<Combination> combinations) 
        {
            Combinations = combinations;
        }
    }
}
