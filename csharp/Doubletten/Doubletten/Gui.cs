using System;

namespace Doubletten
{
    public class Gui
    {
        private readonly string[] args;

        public Gui(string[] args) {
            this.args = args;
        }

        public string DirectoryName {
            get { return args[0]; }
        }

        public event EventHandler SucheDoubletten;
        public bool FortschrittAnzeigen {
            set { throw new NotImplementedException(); }
        }

        public int FortschrittInProzent {
            set { throw new NotImplementedException(); }
        }

        public string[] Dateiliste {
            set {
                foreach (var s in value) {
                    System.Console.WriteLine(s);
                }
            }
        }

        public void Run() {
            if (SucheDoubletten != null) {
                SucheDoubletten(this, EventArgs.Empty);
            }
        }
    }
}