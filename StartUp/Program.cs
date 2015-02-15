using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICSharpCode.Core;
using System.Reflection;
using System.IO;
using System.Resources;
using Frame.GUI.WorkBench;

namespace Implementation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LoggingService.Info("Starting App...");
            Assembly exe = typeof(Program).Assembly;
            FileUtility.ApplicationRootPath = Path.GetDirectoryName(exe.Location);

            CoreStartup c = new CoreStartup("Frame.Core");
            c.ConfigDirectory = FileUtility.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".michael-zhang", "Frame.Core") + Path.DirectorySeparatorChar;
            LoggingService.Info("Starting core services...");
            c.StartCoreServices();
            ResourceService.RegisterNeutralStrings(new ResourceManager("StartUp.Properties.SDRes", exe));
            ResourceService.RegisterNeutralImages(new ResourceManager("StartUp.Properties.SDRes", exe));

            AddInTree.Doozers.Add("Pad", new Frame.Core.Pad.PadDoozer());
            AddInTree.Doozers.Add("DisplayBinding", new Frame.Core.ViewContent.DisplayBindingDoozer());

            LoggingService.Debug("Looking for Addins...");
            c.AddAddInsFromDirectory(FileUtility.ApplicationRootPath);
            //c.ConfigureExternalAddIns(...);
            LoggingService.Debug("Loading AddinTre...");
            c.RunInitialization();

            LoggingService.Debug("Initializing workbench...");
            WorkbenchSingleton.InitializeWorkbench();
            LoggingService.Debug("Starting workbench...");
            Form f = (Form)WorkbenchSingleton.Workbench;
            Application.Run(f);

            PropertyService.Save();
            LoggingService.Info("Leaving app");
        }
    }
}