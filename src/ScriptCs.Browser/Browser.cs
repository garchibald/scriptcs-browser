using System;
using System.Linq;
using ScriptCs.Contracts;

namespace ScriptCs.Browser
{
    public class Browser : IScriptPackContext
    {
        /// <summary>
        /// Starts the web pages with the default browser
        /// </summary>
        /// <param name="address">The the address to be started</param>
        public static void Open(string address)
        {
            Uri link;
            if (ValidateAddress(address, out link)) return;

            System.Diagnostics.Process.Start(link.ToString());
        }


        /// <summary>
        /// Starts the web pages with the default browser with one or more browsers
        /// </summary>
        /// <param name="types">The browser types to open</param>
        /// <param name="address">The the address to be started</param>
        public static void Open(BrowserType types, string address)
        {
            Uri link;
            if (ValidateAddress(address, out link)) return;


            var values = types.ToString()
                  .Split(new[] { ", ", "| " }, StringSplitOptions.None)
                  .Select(v => (BrowserType)Enum.Parse(typeof(BrowserType), v));

            var subKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Clients\\StartMenuInternet\\");
            if (subKey == null)
            {
                Console.WriteLine("Cannot find any registered browsers");
                return;
            }

            foreach (var browserType in values)
            {
                var keyType = String.Empty;
                switch (browserType)
                {
                    case BrowserType.Chrome:
                        keyType = "Google Chrome";
                        break;
                    case BrowserType.InternetExplorer:
                        keyType = "IEXPLORE.EXE";
                        break;
                    case BrowserType.Firefox:
                        keyType = "FIREFOX.EXE";
                        break;
                    case BrowserType.Safari:
                        keyType = "Safari.exe";
                        break;
                }

                if (string.IsNullOrEmpty(keyType))
                {
                    continue;
                }

                var type = subKey.GetSubKeyNames().Where(name => name == keyType);

                if (!type.Any())
                {
                    Console.WriteLine("Unable to start {0} with {1}, Browser not found", address, browserType);
                    continue;
                }

                var command = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(String.Format("SOFTWARE\\Clients\\StartMenuInternet\\{0}\\shell\\open\\command", keyType));
                
                if (command == null)
                {
                    Console.WriteLine("Unable to find command registry entry for {0}", browserType);
                    continue;
                }
                var exe = command.GetValue("") as string;
                System.Diagnostics.Process.Start(exe, address);    
            }
            
        }

        private static bool ValidateAddress(string address, out Uri link)
        {
            if (!Uri.TryCreate(address, UriKind.Absolute, out link))
            {
                Console.WriteLine("Invalid address {0}, Must be absolute uri", address);
                return true;
            }
            return false;
        }
    }

    [Flags]
    public enum BrowserType
    {
        Chrome = 2,
        Firefox = 4,
        Safari = 8,
        IE = 16,
        InternetExplorer = 16,
        All = Chrome | Firefox | Safari | InternetExplorer
    }
}