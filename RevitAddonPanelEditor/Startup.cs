using Autodesk.Revit.UI;
using RevitAddonPanelEditor.Builder;
using RevitAddonPanelEditor.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using RevitAddonPanelEditor.Configuration;

namespace RevitAddonPanelEditor
{
    public sealed class Startup
    {
        private readonly UIControlledApplication _app;

        public Startup(UIControlledApplication app)
        {
            _app = app;
        }

        public void Start()
        {
            var path = Assembly.GetExecutingAssembly().Location;

            var builder = new AppBuilder(_app);
            var config = new Config(path);

            builder
                .SetTabName("Cool")
                .SetPanel("My panel")
                .AddButton(config.GetMarkingButton())
                .AddButton(config.GetBRUHButton())
                .Build();
        }
    }
}
