using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CUFramework.Shared;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics;

namespace CUFramework.Windows
{
    public partial class CUTitleBar : UserControl
    {
        bool LastFocusState = true;
        private Color p_Color = ThemeConstants.TitleBarBackground;
        public Color Color
        {
            get
            {
                return p_Color;
            }
            set
            {
                p_Color = value;
                UpdateFocusState(LastFocusState);
                if (this.ParentForm is CUWindow)
                {
                    ((CUWindow)this.ParentForm).TriggerTitlebarColorChanged();
                }
            }
        }

        public Color VisibleColor => this.BackColor;

        public CUTitleBar()
        {
            InitializeComponent();
            Color = ThemeConstants.TitleBarBackground;
            this.ForeColor = ThemeConstants.ForegroundColor;
            this.label1.MouseDown += Header_MouseDown;
            this.label1.MouseDoubleClick += Label1_MouseDoubleClick;
            this.cuControlBox1.BackColor = ThemeConstants.ControlBoxButtons;
        }

        private void Label1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var form = FindForm();
            form.WindowState = form.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        public CUControlBox ControlBox => cuControlBox1;

        public void UpdateFocusState(bool focused)
        {
            this.LastFocusState = focused;
            this.BackColor = focused ? p_Color : ControlPaint.Dark(p_Color);
            if (this.ParentForm is CUWindow)
            {
                ((CUWindow)this.ParentForm).TriggerTitlebarColorChanged();
            }
        }

        public override string Text 
        {
            get
            {
                return label1.Text;
            }
            set
            {
                label1.Text = value;
            }
        }

        private void Header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1 && sender == label1 )
            {
                this.label1.Capture = false;
                const int WM_NCLBUTTONDOWN = 0xa1;
                const int HTCAPTION = 0x2;
                Message msg = Message.Create(this.FindForm().Handle, WM_NCLBUTTONDOWN, new IntPtr(HTCAPTION), IntPtr.Zero);
                this.DefWndProc(ref msg);
            }
        }
    }
}
