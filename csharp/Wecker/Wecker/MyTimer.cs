using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wecker
{
    public class MyTimer
    {
        private Timer timer;
        private bool started;

        public event Action Tick;

        public event Action StoppableTick;

        public MyTimer()
        {
            timer = new Timer(state => TimerTick());
            timer.Change(1000, 1000);
        }

        public void TimerTick()
        {
            Tick?.Invoke();
            if(started)
            {
                StoppableTick?.Invoke();
            }
        }

        public void Start()
        {
            started = true;
        }

        public void Stop()
        {
            started = false;
        }
    }
}
