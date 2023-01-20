using Microsoft.EntityFrameworkCore;
using ProductsAzyavchikava.Controllers;
using ProductsAzyavchikava.Views;
using ProductsAzyavchikava.Views.Intefraces;

namespace ProductsAzyavchikava
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            IMainView view = new MainView();
            new MainController(view);
            Application.Run((Form)view);
        }
    }
}