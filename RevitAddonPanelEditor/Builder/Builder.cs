using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddonPanelEditor.Builder
{
    public abstract class Builder<T>
    {
        public abstract T Build();
    }
}
