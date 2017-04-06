using System;
using System.Media;
using System.Threading;
using System.Windows;

namespace Wecker
{
    public partial class Ui : Window
    {
        private MyTimer timer;
        private DateTime endeZeit;

        public Ui() {
            InitializeComponent();
            starten.Click += (s, e) => Starten(Zeitholen());
            timer = new MyTimer();
            timer.StoppableTick += TimerTick;
            timer.Tick += Uhrzeit;
        }

        public void Starten(DateTime endeZeit) {
            this.endeZeit = endeZeit;
            timer.Start();
        }

        public void TimerTick() {
            Weckzeit(endeZeit);
            if (DateTime.Now >= endeZeit) {
                using (var soundPlayer = new SoundPlayer(@"c:\Windows\Media\chimes.wav")) {
                    soundPlayer.Play();
                }
                timer.Stop();
            }
        }

        public DateTime Zeitholen() {
            if (txtWann.Text == "") {
                txtWann.Text = DateTime.Now.ToLongTimeString();
            }
            return DateTime.Parse(txtWann.Text);
        }

        public void Weckzeit(DateTime weckenUm) {
            if (lblUhrzeit.Dispatcher.CheckAccess()) {
                lblUhrzeit.Content = DateTime.Now.ToLongTimeString();
                lblRest.Content = (weckenUm - DateTime.Now).ToString(@"hh\:mm\:ss");
            }
            else {
                lblUhrzeit.Dispatcher.Invoke(() => {
                    lblUhrzeit.Content = DateTime.Now.ToLongTimeString();
                    lblRest.Content = (weckenUm - DateTime.Now).ToString(@"hh\:mm\:ss");
                });
            }
        }

        public void Uhrzeit()
        {
            if (lblUhrzeit.Dispatcher.CheckAccess())
            {
                lblUhrzeit.Content = DateTime.Now.ToLongTimeString();
            }
            else
            {
                lblUhrzeit.Dispatcher.Invoke(() => {
                    lblUhrzeit.Content = DateTime.Now.ToLongTimeString();
                });
            }
        }


    }
}