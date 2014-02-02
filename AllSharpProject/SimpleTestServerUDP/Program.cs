using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SimpleTestServerUDP.Internet;
using TestLibrary;

namespace SimpleTestServerUDP
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartNewForm());
        }
    }

    internal static class Tester
    {
        public static void Test_ClientDetailForm()
        {
        }
    }
}
