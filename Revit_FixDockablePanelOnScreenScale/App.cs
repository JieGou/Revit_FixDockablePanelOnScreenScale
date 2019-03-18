namespace Revit_FixDockablePanelOnScreenScale
{
    using System;
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.UI.Events;

    public class App : IExternalApplication
    {
        private UIControlledApplication _application;

        public Result OnStartup(UIControlledApplication application)
        {
            _application = application;
            application.Idling += ApplicationOnIdling;

            return Result.Succeeded;
        }

        private void ApplicationOnIdling(object sender, IdlingEventArgs e)
        {
            if (sender is UIApplication uiApp)
            {
                DockablePaneProviderData data = new DockablePaneProviderData();
                PanelContent panelContent = new PanelContent();
                data.InitialState = new DockablePaneState();
                DockablePaneId dockablePaneId = new DockablePaneId(Guid.Parse("0f0f25f5-712d-4b37-ac50-45ed901e77fc"));
                uiApp.RegisterDockablePane(dockablePaneId, "Test Dockable Panel", (IDockablePaneProvider)panelContent);
            }
            else
            {
                _application.Idling -= ApplicationOnIdling;
            }

        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }

    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var dpid = new DockablePaneId(Guid.Parse("0f0f25f5-712d-4b37-ac50-45ed901e77fc"));
            if (!DockablePane.PaneIsRegistered(dpid))
            {
                TaskDialog.Show("Test panel", "Not Registered!");
            }
            else
            {
                var dp = commandData.Application.GetDockablePane(dpid);

                if (dp.IsShown())
                    dp.Hide();
                else
                    dp.Show();
            }

            return Result.Succeeded;
        }
    }
}
