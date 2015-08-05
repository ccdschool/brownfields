using System;
using System.Media;
using System.Threading;
using System.Windows;

namespace Wecker
{
    public partial class Ui : Window
    {
        private Timer timer;
        private DateTime endeZeit;

        public Ui() {
            InitializeComponent();
            starten.Click += (s, e) => Starten(Zeitholen());
        }

        public void Starten(DateTime endeZeit) {
            this.endeZeit = endeZeit;
            timer = new Timer(state => TimerTick());
            timer.Change(1000, 1000);
        }

        public void TimerTick() {
            Uhrzeit(endeZeit);
            if (DateTime.Now >= endeZeit) {
                using (var soundPlayer = new SoundPlayer(@"c:\Windows\Media\chimes.wav")) {
                    soundPlayer.Play();
                }
                timer.Dispose();
            }
        }

        public DateTime Zeitholen() {
            if (txtWann.Text == "") {
                txtWann.Text = DateTime.Now.ToLongTimeString();
            }
            return DateTime.Parse(txtWann.Text);
        }

        public void Uhrzeit(DateTime weckenUm) {
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
    }
}