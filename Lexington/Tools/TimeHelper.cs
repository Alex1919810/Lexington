using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Lexington.Tools
{
    public class TimerHelper<T>
    {
        private T[] items;
        private int seconds;
        private int currentIndex;
        private DispatcherTimer timer;

        //微妙触发
        public TimerHelper(T[] items, int interval)
        {
            this.items = items;
            currentIndex = 0;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(interval);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        //秒触发
        public TimerHelper(int item, int interval)
        {
            this.seconds = item;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(interval);
            timer.Tick += Timer_Tick_Helper;
            timer.Start();
        }

        public event EventHandler<T> OnItemUpdated;

        public event EventHandler OnAnimationComplete;

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (currentIndex < items.Length)
            {
                OnItemUpdated?.Invoke(this, items[currentIndex]);
                currentIndex++;
            }
            else
            {
                timer.Stop();
                OnAnimationComplete?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Timer_Tick_Helper(object sender, EventArgs e)
        {
            if(this.seconds > 0)
            {
                this.seconds--;
            }
            else
            {
                timer.Stop();
                this.seconds = 0;
                OnAnimationComplete?.Invoke(this, EventArgs.Empty);
            }
        }

        public void StopTimerForText()
        {
            timer.Stop();
            OnAnimationComplete?.Invoke(this, EventArgs.Empty);


        }

        public void StopTimer()
        { 
            timer.Stop();
        }
    }
}
