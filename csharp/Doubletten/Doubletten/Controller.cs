using System;

namespace Doubletten
{
    public class Controller
    {
        private readonly Gui gui;
        private readonly DoublettenSuche doublettenSuche;

        public Controller(Gui gui, DoublettenSuche doublettenSuche) {
            this.gui = gui;
            this.doublettenSuche = doublettenSuche;
            gui.SucheDoubletten += Suchen;
        }

        private void Suchen(object sender, EventArgs e) {
            gui.Dateiliste = doublettenSuche.Suche(gui.DirectoryName);
        }
    }
}