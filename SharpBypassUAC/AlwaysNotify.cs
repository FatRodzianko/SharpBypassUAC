using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace SharpBypassUAC
{
    public class AlwaysNotify
    {
        public AlwaysNotify()
        {
            RegistryKey alwaysNotify = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System");
            string consentPrompt = alwaysNotify.GetValue("ConsentPromptBehaviorAdmin").ToString();
            string secureDesktopPrompt = alwaysNotify.GetValue("PromptOnSecureDesktop").ToString();
            alwaysNotify.Close();

            if (consentPrompt == "2" & secureDesktopPrompt == "1")
            {
                System.Console.WriteLine("UAC is set to 'Always Notify.' This attack will fail. Exiting...");
                System.Environment.Exit(1);
            }
        }
    }
}
