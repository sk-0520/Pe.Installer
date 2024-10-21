using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pe.Installer
{
    public interface IProgressLogger
    {
        #region function

        void Reset(int step);
        void Stepup();
        void Increment(int value);
        void Set(int value);
        void Wait();

        #endregion
    }

    internal class ProgressLogger: IProgressLogger
    {
        public ProgressLogger(ProgressBar progressBar)
        {
            ProgressBar = progressBar;
        }

        #region property

        private ProgressBar ProgressBar { get; }

        #endregion

        #region function

        void Do(Action<ProgressBar> action)
        {
            if(ProgressBar.InvokeRequired) {
                ProgressBar.BeginInvoke(new Action(() => {
                    action(ProgressBar);
                }));
            } else {
                action(ProgressBar);
            }
        }

        #endregion

        #region IProgressLogger

        public void Reset(int step)
        {
            Do(p => {
                p.Style = ProgressBarStyle.Continuous;
                p.Value = 0;
                p.Step = step;
                p.Maximum = 100;
            });
        }

        public void Stepup()
        {
            Do(p => {
                p.PerformStep();
            });
        }

        public void Increment(int value)
        {
            Do(p => {
                p.Increment(value);
            });
        }
        public void Set(int value)
        {
            Do(p => {
                p.Value = value;
            });
        }

        public void Wait()
        {
            Do(p => {
                p.Style = ProgressBarStyle.Marquee;
                p.Step = 10;
                p.Maximum = 100;
                p.MarqueeAnimationSpeed = 20;
            });
        }

        #endregion
    }
}
