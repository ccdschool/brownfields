using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System;

namespace Wecker
{
    public partial class Ui : Window
    {
        private MyTimer timer;
        private DateTime endeZeit;

        public Ui()
        {
            InitializeComponent();
            starten.Click += (s, e) => Starten(Zeitholen());
            timer = new MyTimer();
            timer.StoppableTick += TimerTick;
            timer.Tick += Uhrzeit;
        }

        public void Starten(DateTime endeZeit)
        {
            this.endeZeit = endeZeit;
            timer.Start();
        }

        public void TimerTick()
        {
            Weckzeit(endeZeit);
            if (DateTime.Now >= endeZeit)
            {
                Console.Beep();
                timer.Stop();
            }
        }

        public DateTime Zeitholen()
        {
            if (txtWann.Text == "")
            {
                txtWann.Text = DateTime.Now.ToLongTimeString();
            }
            return DateTime.Parse(txtWann.Text ?? "");
        }

        public void Weckzeit(DateTime weckenUm)
        {
            Dispatcher.UIThread.Post(() =>
            {
                lblUhrzeit.Text = DateTime.Now.ToLongTimeString();
                lblRest.Text = (weckenUm - DateTime.Now).ToString(@"hh\:mm\:ss");
            });
        }

        public void Uhrzeit()
        {
            Dispatcher.UIThread.Post(() =>
            {
                lblUhrzeit.Text = DateTime.Now.ToLongTimeString();
            });
        }
    }
}
