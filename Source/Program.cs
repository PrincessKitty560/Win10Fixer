using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;

namespace W10_Installation_Fixer
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (IsUserAdministrator() == true)
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Directory.CreateDirectory("C:/temp/Win10_Fix/");
                File.WriteAllBytes("C:/temp/Win10_Fix/Auto_Decrapify.ps1", Properties.Resources.Auto_DeCrapify);
                File.WriteAllBytes("C:/temp/Win10_Fix/Install_Chrome.ps1", Properties.Resources.Install_Chrome);
                File.WriteAllBytes("C:/temp/Win10_Fix/Install_Firefox.ps1", Properties.Resources.Install_FireFox);
                File.WriteAllBytes("C:/temp/Win10_Fix/EdgeDeflector_Setup.exe", Properties.Resources.setup);
                File.WriteAllBytes("C:/temp/Win10_Fix/SetUserFTA.exe", Properties.Resources.SetUserFTA);
                Application.Run(new Form1());
            } else {
                MessageBox.Show("The application was not run as an administrator. To prevent errors, the application must be run as administrator!", "Win10 Fixer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }

        public static void RunScripts(int browser, bool instED)
        {
            //Install Browsers
            switch (browser)
            {
                case 0:
                    Process chrome = new Process();
                    chrome.StartInfo = new ProcessStartInfo("powershell.exe", "-executionpolicy unrestricted -command C:/temp/Win10_Fix/Install_Chrome.ps1");
                    chrome.Start();
                    chrome.WaitForExit();
                    break;
                case 1:
                    Process firefox = new Process();
                    firefox.StartInfo = new ProcessStartInfo("powershell.exe", "-executionpolicy unrestricted -command C:/temp/Win10_Fix/Install_Firefox.ps1");
                    firefox.Start();
                    firefox.WaitForExit();
                    break;
                case 2:
                    break;
            }
            if (instED == true)
            {
                //Install EdgeDeflector
                Process eDS = new Process();
                eDS.StartInfo = new ProcessStartInfo("C:/temp/EdgeDeflector_Setup.exe");
                eDS.Start();
                eDS.WaitForExit();

                //Use SetUserFTA to change edge protocol to open EdgeDeflector
                Process sUFTA = new Process();
                sUFTA.StartInfo = new ProcessStartInfo("C:/temp/Win10_Fix/SetUserFTA.exe", "microsoft-edge EdgeUriDeflector");
                sUFTA.Start();
                sUFTA.WaitForExit();
            }

            //Run Auto_Decrapify script
            Process aD = new Process();
            aD.StartInfo = new ProcessStartInfo("powershell.exe", "-executionpolicy unrestricted -command C:/temp/Win10_Fix/Auto_Decrapify.ps1");
            aD.Start();
            aD.WaitForExit();

            //Message Complete
            MessageBox.Show("Click OK to restart your PC", "Win10 Fixer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Directory.Delete("C:/temp/Win10_Fix", true);
            Process restart = new Process();
            restart.StartInfo = new ProcessStartInfo("cmd.exe", "shutdown /r /t 10");
            Application.Exit();
        }
        
        public static bool IsUserAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            WindowsIdentity user = null;
            try
            {
                //get the currently logged in user
                user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                isAdmin = false;
            }
            catch (Exception ex)
            {
                isAdmin = false;
            }
            finally
            {
                if (user != null)
                    user.Dispose();
            }
            return isAdmin;
        }

    }
}
