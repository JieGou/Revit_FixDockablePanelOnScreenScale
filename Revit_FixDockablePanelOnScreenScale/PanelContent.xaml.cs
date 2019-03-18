namespace Revit_FixDockablePanelOnScreenScale
{
    using System.Windows.Controls;
    using Autodesk.Revit.UI;

    public partial class PanelContent : Page, IDockablePaneProvider
    {
        public PanelContent()
        {
            InitializeComponent();
        }

        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this;
            data.InitialState = new DockablePaneState
            {
                DockPosition = DockPosition.Tabbed,
                TabBehind = DockablePanes.BuiltInDockablePanes.ProjectBrowser
            };

        }
    }
}
