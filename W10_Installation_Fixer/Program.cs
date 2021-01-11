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

        public static void RunScripts(int Browser)
        {
            //Install Browsers
            switch (Browser)
            {
                case 0:
                    Process Chrome = new Process();
                    Chrome.StartInfo = new ProcessStartInfo("powershell.exe", "-executionpolicy unrestricted -command C:/temp/Win10_Fix/Install_Chrome.ps1");
                    Chrome.Start();
                    Chrome.WaitForExit();
                    break;
                case 1:
                    Process Firefox = new Process();
                    Firefox.StartInfo = new ProcessStartInfo("powershell.exe", "-executionpolicy unrestricted -command C:/temp/Win10_Fix/Install_Firefox.ps1");
                    Firefox.Start();
                    Firefox.WaitForExit();
                    break;
                case 2:
                    break;
            }
            //Install EdgeDeflector
            Process EDS = new Process();
            EDS.StartInfo = new ProcessStartInfo("C:/temp/EdgeDeflector_Setup.exe");
            EDS.Start();
            EDS.WaitForExit();

            //Use SetUserFTA to change edge protocol to open EdgeDeflector
            Process SUFTA = new Process();
            SUFTA.StartInfo = new ProcessStartInfo("C:/temp/Win10_Fix/SetUserFTA.exe", "microsoft-edge EdgeUriDeflector");
            SUFTA.Start();
            SUFTA.WaitForExit();

            //Run Auto_Decrapify script
            Process AD = new Process();
            AD.StartInfo = new ProcessStartInfo("powershell.exe", "-executionpolicy unrestricted -command C:/temp/Win10_Fix/Auto_Decrapify.ps1");
            AD.Start();
            AD.WaitForExit();

            //Message Complete
            MessageBox.Show("Click OK to restart your PC", "Win10 Fixer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Directory.Delete("C:/temp/Win10_Fix", true);
            Process Restart = new Process();
            Restart.StartInfo = new ProcessStartInfo("cmd.exe", "shutdown /r /t 10");
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
