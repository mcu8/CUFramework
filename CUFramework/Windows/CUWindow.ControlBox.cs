using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CUFramework.Windows
{
    public partial class CUWindow
    {
        bool BorderEnabled = true;
        CUControlBox CMyControlBox => TitleBar.ControlBox;

        [Browsable(true)]
        [Category("CUFramework")]
        public bool IsMaximizeButtonEnabled
        {
            get => CMyControlBox.IsMaximizeButtonEnabled;
            set
            {
                CMyControlBox.IsMaximizeButtonEnabled = value;
            }
        }

        [Browsable(true)]
        [Category("CUFramework")]
        public bool IsMinimizeButtonEnabled
        {
            get => CMyControlBox.IsMinimizeButtonEnabled;
            set
            {
                CMyControlBox.IsMinimizeButtonEnabled = value;
            }
        }

        [Browsable(true)]
        [Category("CUFramework")]
        public bool IsCloseButtonEnabled
        {
            get => CMyControlBox.IsCloseButtonEnabled;
            set
            {
                CMyControlBox.IsCloseButtonEnabled = value;
            }
        }

        private void CUWindow_SizeChanged(object sender, EventArgs e)
        {
            CMyControlBox.ReportWindowState(this.WindowState);
            //CMyControlBox.Location = new Point(TitleBar.Width - CMyControlBox.Width, 0);
            BorderEnabled = this.WindowState != System.Windows.Forms.FormWindowState.Maximized;
        }
    }
}
