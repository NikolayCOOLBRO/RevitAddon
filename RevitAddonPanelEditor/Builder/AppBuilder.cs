using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddonPanelEditor.Builder
{
    public class AppBuilder : Builder<UIControlledApplication>
    {
        private readonly UIControlledApplication _app;

        private List<PushButtonData> m_ListButtons;

        private string m_TabName;
        private string m_PanelName;

        public AppBuilder(UIControlledApplication app)
        {
            _app = app;
            m_ListButtons= new List<PushButtonData>();
        }

        public AppBuilder SetTabName(string name)
        {
            m_TabName = name;
            return this;
        }

        public AppBuilder SetPanel(string panel)
        {
            m_PanelName = panel;
            return this;
        }

        public AppBuilder AddButton(PushButtonData button)
        {
            m_ListButtons.Add(button);
            return this;
        }

        public override UIControlledApplication Build()
        {
            _app.CreateRibbonTab(m_TabName);

            var panel = _app.CreateRibbonPanel(m_TabName, m_PanelName);


            foreach (var item in m_ListButtons)
            {
                panel.AddItem(item);
            }

            return _app;
        }
    }
}
