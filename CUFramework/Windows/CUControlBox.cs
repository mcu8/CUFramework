
using CUFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CUFramework.Windows
{
    public class CUControlBox : Control
    {
        private bool _IsMaximizeButtonEnabled = true;
        [Browsable(true)]
        [Category("CUFramework")]
        public bool IsMaximizeButtonEnabled
        {
            get => _IsMaximizeButtonEnabled;
            set
            {
                _IsMaximizeButtonEnabled = value;
                ReinitializeButtons();
            }
        }

        private bool _IsMinimizeButtonEnabled = true;
        [Browsable(true)]
        [Category("CUFramework")]
        public bool IsMinimizeButtonEnabled
        {
            get => _IsMinimizeButtonEnabled;
            set
            {
                _IsMinimizeButtonEnabled = value;
                ReinitializeButtons();
            }
        }

        private bool _IsCloseButtonEnabled = true;
        [Browsable(true)]
        [Category("CUFramework")]
        public bool IsCloseButtonEnabled
        {
            get => _IsCloseButtonEnabled;
            set
            {
                _IsCloseButtonEnabled = value;
                ReinitializeButtons();
            }
        }

        private readonly Size ButtonSize = new Size(48, 32);
        private readonly CUButtonBorderless CloseButton;
        private readonly CUButtonBorderless MaximizeButton;
        private readonly CUButtonBorderless MinimizeButton;

        public CUControlBox()
        {
            CloseButton = new CUButtonBorderless();
            MaximizeButton = new CUButtonBorderless();
            MinimizeButton = new CUButtonBorderless();

            CloseButton.Image = Properties.Resources.close;
            MaximizeButton.Image = Properties.Resources.maximize;
            MinimizeButton.Image = Properties.Resources.minimize;

            this.BackColor = Color.Teal;

            this.Height = ButtonSize.Height;

            this.Margin = new Padding(0, 0, 0, 0);
            this.Padding = new Padding(0, 0, 0, 0);

            CloseButton.Click += (o, e) =>
            {
                this.FindForm().Close();
            };

            MinimizeButton.Click += (o, e) =>
            {
                this.FindForm().WindowState = FormWindowState.Minimized;
            };

            MaximizeButton.Click += (o, e) =>
            {
                this.FindForm().WindowState = this.FindForm().WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
            };


            this.MouseDown += CUControlBox_MouseDown;

            this.ParentChanged += CUControlBox_ParentChanged;
        }


        private void CUControlBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                this.Capture = false;
                const int WM_NCLBUTTONDOWN = 0xa1;
                const int HTCAPTION = 0x2;
                Message msg = Message.Create(this.FindForm().Handle, WM_NCLBUTTONDOWN, new IntPtr(HTCAPTION), IntPtr.Zero);
                this.DefWndProc(ref msg);
            }
        }

        private void CUControlBox_ParentChanged(object sender, EventArgs e)
        {
            ReinitializeButtons();
        }

        public void ReinitializeButtons()
        {
            this.SuspendLayout();
            this.Controls.Clear();

            List<CUButtonBorderless> buttons = new List<CUButtonBorderless>();

            if (IsCloseButtonEnabled)
                buttons.Add(CloseButton);
            if (IsMaximizeButtonEnabled)
                buttons.Add(MaximizeButton);
            if (IsMinimizeButtonEnabled)
                buttons.Add(MinimizeButton);

            Debug.WriteLine($"{IsCloseButtonEnabled}|{IsMaximizeButtonEnabled}|{IsMinimizeButtonEnabled}");

            buttons.Reverse();

            int i = 0;
            for (; i != buttons.Count; i++)
            {
                buttons[i].Size = ButtonSize;
                buttons[i].ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
                buttons[i].Location = new Point((i * ButtonSize.Width), 0);
                buttons[i].Parent = this;
                buttons[i].Text = "";
                buttons[i].NoFocus = true;
            }

            this.MinimumSize = new Size(ButtonSize.Width * i, this.MinimumSize.Height);
            this.MaximumSize = this.MinimumSize;
            this.Width = this.MinimumSize.Width;
            
            if (Parent != null)
            {
                this.Location = new Point(Parent.Width - this.Width, 0);
            }

            Debug.WriteLine(this.Width);

            if (this.FindForm() != null)
            {
                ReportWindowState(this.FindForm().WindowState);
            }

            this.ResumeLayout();
        }

        public void ReportWindowState(FormWindowState state)
        {
            MaximizeButton.Image = state == FormWindowState.Normal ? Properties.Resources.maximize : Properties.Resources.unmaximize;
        }
    }
}
