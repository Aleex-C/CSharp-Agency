using Npgsql;
using AgentieTurismC.Domain;
using AgentieTurismC.Repository;
using log4net.Config;
using log4net;

namespace AgentieTurismC
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm());
            //string path = System.AppDomain.CurrentDomain.BaseDirectory;
            //XmlConfigurator.Configure(new System.IO.FileInfo(path + "log4net.config"));
        }
    }
}