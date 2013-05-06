using System;
using System.Diagnostics;
using System.Reflection;
using TradePlatform.MT4.Core;

namespace TradePlatform.MT4.SDK.Shell
{
    //using TradePlatform.MT4.Data;


    internal class Program
    {
        private static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //SetupConsole();

            Bridge.InitializeHosts();
        }

        private static void SetupConsole()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Title = string.Format(".NET Bridge Shell {0}", Assembly.GetExecutingAssembly().GetName().Version);

            Console.SetWindowPosition(0, 0);
            Console.WindowTop = 0;
            Console.WindowLeft = 0;

            Console.WindowWidth = 130;
            Console.SetWindowSize(130, 15);
            Console.WindowHeight = 15;

            Console.BufferHeight = Int16.MaxValue - 1;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (!Debugger.IsAttached)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(((Exception) e.ExceptionObject).Message);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();

                Environment.Exit(1);
            }
        }
    }
}