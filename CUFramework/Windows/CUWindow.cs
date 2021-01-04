
using CUFramework.Dialogs;
using CUFramework.Shared;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CUFramework.Windows
{
    // TODO: fix icon apperance on taskbar!!!
    public partial class CUWindow : Form
    {
        CUTitleBar TitleBar;

        public event EventHandler TitlebarColorChanged;
        public void TriggerTitlebarColorChanged()
        {
            TitlebarColorChanged?.Invoke(this, new EventArgs());
        }

        private bool p_IsControlBoxVisible = true;
        [Browsable(true)]
        [Category("CUFramework")]
        public bool ControlBoxVisible
        {
            get
            {
                return p_IsControlBoxVisible;
            }
            set
            {
                p_IsControlBoxVisible = value;
                this.CMyControlBox.Visible = p_IsControlBoxVisible;
            }
        }

        private Color p_TitlebarColor = ThemeConstants.TitleBarBackground;
        [Browsable(true)]
        [Category("CUFramework")]
        //[DefaultValue("0;0;0")]
        public Color TitlebarColor
        {
            get
            {
                return p_TitlebarColor;
            }
            set
            {
                p_TitlebarColor = value;
                if (this.TitleBar != null)
                    this.TitleBar.Color = p_TitlebarColor;
            }
        }

        public Color VisibleTitlebarColor {
            get
            {
                if (TitleBar == null)
                    return p_TitlebarColor;
                return TitleBar.VisibleColor;
            }
        }

        private bool p_IsResizable = true;
        [Browsable(true)]
        [Category("CUFramework")]
        public bool IsResizable
        {
            get
            {
                return p_IsResizable;
            }
            set
            {
                p_IsResizable = value;
            }
        }

        public CUWindow()
        {
            ApplyShadows();

            this.FormBorderStyle = FormBorderStyle.None;
            //this.MinimumSize = new Size(800, 480);
            
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = FormStartPosition.WindowsDefaultBounds;

            this.ShowInTaskbar = true;

            TitleBar = new CUTitleBar();
            TitleBar.Margin = new Padding(0);
            this.Padding = new Padding(BorderThickness / 2);
            TitleBar.Dock = DockStyle.Top;
            TitleBar.Text = this.Text;
            TitleBar.Color = p_TitlebarColor;
            this.Controls.Add(TitleBar);

            CMyControlBox.ReportWindowState(this.WindowState);

            InitializeResolutionHandler();

            this.Resize += (o, e) =>
            {
                this.Invalidate();
            };

            this.Paint += BaseWindow_Paint;

            this.ForeColor = ThemeConstants.ForegroundColor;
            this.BackColor = ThemeConstants.BackgroundColor;

            if (!p_IsControlBoxVisible)
            {
                this.CMyControlBox.Visible = false;
            }

            this.FormClosing += BaseWindow_FormClosing;
            this.SizeChanged += CUWindow_SizeChanged;
            this.Load += CUWindow_Load;

            this.Deactivate += CUWindow_Deactivate;
            this.Activated += CUWindow_Activated;

            this.MouseDown += CUWindow_MouseDown;

            //FocusChanged?.Invoke(this, new EventArgs());
        }

        public void ToggleTitleBar(bool v)
        {
            if (v)
            {
                this.TitleBar.Show();
            }
            else
            {
                this.TitleBar.Hide();
            }
        }

        private void CUWindow_Activated(object sender, EventArgs e)
        {
            Debug.WriteLine("Focus");
            TitleBar.UpdateFocusState(true);
           //TitlebarColorChanged?.Invoke(this, new EventArgs());
        }

        private void CUWindow_Deactivate(object sender, EventArgs e)
        {
            Debug.WriteLine("FocusLost");
            TitleBar.UpdateFocusState(false);
            //TitlebarColorChanged?.Invoke(this, new EventArgs());
        }

        private void CUWindow_Load(object sender, EventArgs e)
        {
            InitWindowLoc();
            TriggerTitlebarColorChanged();
        }

        private void BaseWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != Owner)
            {
                Owner.Activate();
            }
        }

        private void BaseWindow_Paint(object sender, PaintEventArgs e)
        {
             DrawBorder(e);
        }

        private int _border = 4;
        public int BorderThickness
        {
            get => _border;
            set
            {
                _border = value;
                Refresh();
            }
        }

        public void DrawBorder(PaintEventArgs e)
        {
            var pen = new Pen(BorderEnabled ? ThemeConstants.BorderColor : TitleBar.BackColor);
            pen.Width = _border;
            var rect = new Rectangle(new Point(0, 0), new Size(this.Width, this.Height));
            e.Graphics.DrawRectangle(pen, rect);
        }
        

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                if (TitleBar != null)
                    TitleBar.Text = value;
            }
        }


        public void Delay(Action a, int time)
        {
            Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(time);
                this.Invoke(new MethodInvoker(a));
            });
        }
    }
}
