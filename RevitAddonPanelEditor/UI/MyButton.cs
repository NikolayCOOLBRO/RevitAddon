using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddonPanelEditor.UI
{
    public class MyButton : PushButtonData
    {
        public MyButton(string name, string text, string assemblyName, string className) 
            : base(name, text, assemblyName, className)
        {

        }
    }
}
