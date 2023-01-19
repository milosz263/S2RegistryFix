using System;
using System.Reflection;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;

namespace Sims2RegFix
{
    internal static class RegBuilder
    {
        private static string filePath = "";

        private const string valEnd = "\\Fun with Pets\\SP9\\TSBin\\Sims2EP9.exe";
        public static void GenerateRegFile()
        {

            var reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\Sims2EP9.exe", false);
            string exePath = reg.GetValue("").ToString();
            if (File.Exists(exePath))
            {
                Console.WriteLine("Detected game exe: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("{0}", exePath);
                Console.ResetColor();

                string templatePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                if (!templatePath.EndsWith("\\"))
                    templatePath += "\\";

                string gamePath = exePath.Substring(0, exePath.Length - valEnd.Length);

                Console.WriteLine("Game installation path: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("{0}", gamePath);
                Console.ResetColor();

                string template = File.ReadAllText(templatePath + "Sims2paths.dat");
                template.Replace("{GamePath}", gamePath);
                filePath = templatePath + "Sims2pathFix.reg";

                if (File.Exists(filePath))
                    File.Delete(filePath);
                File.WriteAllText(filePath, template, System.Text.Encoding.Unicode);

                Console.WriteLine("Finished generation of *.reg file: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("{0}", filePath);
                Console.ResetColor();

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: Could not detect game location. File: {0} does not exist.", exePath);
                Console.ResetColor();
            }
        }

        public static void ExecuteRegFile()
        {
            if (File.Exists(filePath))
            {
                Console.WriteLine("Calling RegEdit...");
                Process reg = Process.Start("regedit.exe", "\"" + filePath + "\"");
                reg.WaitForExit();
            }
        }
        public static string WriteRegFile()
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
