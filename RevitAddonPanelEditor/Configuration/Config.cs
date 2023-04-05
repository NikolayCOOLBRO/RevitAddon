using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddonPanelEditor.Configuration
{
    public class Config
    {
        private readonly string _assambleyPath;

        public Config(string assambleyPath)
        {
            _assambleyPath = assambleyPath;
        }

        public PushButtonData GetMarkingButton()
        {
            var button = new PushButtonData("Marks",
                                            "Маркировать",
                                            _assambleyPath,
                                            "RevitAddonPanelEditor.Command.Marking.MarkingCommand");

            return button;
        }

        public PushButtonData GetBRUHButton()
        {
            var button = new PushButtonData("Bruh",
                                            "Bruh",
                                            _assambleyPath,
                                            "RevitAddonPanelEditor.Command.Marking.MarkingCommand");

            return button;
        }
    }
}
