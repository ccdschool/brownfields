using System;
using System.Windows;

namespace Wecker
{
    public class Program
    {
        [STAThread]
        public static void Main() {
            var ui = new Ui();
            var app = new Application {
                MainWindow = ui
            };

            app.Run(ui);
        }
    }
}