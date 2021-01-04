using CUFramework.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace CUFramework.Controls.Tabs
{
    [Designer(typeof(TabControllerDesigner))]
    public class CUTabController : Control
    {
        private TabControl _ptc = null;

        [Browsable(true)]
        [Category("CUFramework")]
        public TabControl ParentTabControl
        {
            get
            {
                return _ptc;
            }
            set
            {
                if (_ptc != null)
                {
                    _ptc.SelectedIndexChanged -= _ptc_SelectedIndexChanged;
                }
                _ptc = value;
                if (_ptc != null)
                {
                    _ptc.SelectedIndexChanged += _ptc_SelectedIndexChanged;
                }
            }
        }

        private void _ptc_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        [Browsable(true)]
        [Category("CUFramework")]
        public Color SelectedColor { get; set; } = ThemeConstants.BorderColor;

        [Browsable(true)]
        [Category("CUFramework")]
        public Color InactiveColor { get; set; } = Color.Transparent;

        [Browsable(true)]
        [Category("CUFramework")]
        public Color DisabledColor { get; set; } = Color.DarkGray;

        [Browsable(true)]
        [Category("CUFramework")]
        public event EventHandler<TabControllerPageChangeEventArgs> PageChange;
        public class TabControllerPageChangeEventArgs : EventArgs
        {
            public int PageIndex { get; set; }
            public bool IsCancelled { get; set; }
        }

        public CUTabController()
        {
            this.HandleCreated += TabController_ParentChanged;
        }

        private void TabController_ParentChanged(object sender, EventArgs e)
        {

            if (this.BackColor == Color.Transparent)
            {
                this.BackColor = this.Parent.BackColor;
                UpdateButtonStates();
            }
            Init();
        }

        private void UpdateButtonStates()
        {
            foreach (var c in Controls)
            {
                if (c is TableLayoutPanel)
                {
                    foreach (var ctrl in ((TableLayoutPanel)c).Controls)
                    {
                        if (ctrl is CUButton)
                        {
                            var button = (CUButton)ctrl;
                            var page = (IndexPage)((button).Tag);
                            if (page.Enabled)
                            {
                                if (page.Index == ParentTabControl.SelectedIndex)
                                {
                                    button.BackColor = ColorConditional(SelectedColor);
                                    button.FlatAppearance.BorderColor = ColorConditional(SelectedColor);
                                }
                                else
                                {
                                    button.BackColor = ColorConditional(InactiveColor);
                                    button.FlatAppearance.BorderColor = ColorConditional(InactiveColor);
                                }
                            }
                            else
                            {
                                Debug.Write("DISABLED???");
                                button.BackColor = ColorConditional(DisabledColor);
                                button.FlatAppearance.BorderColor = ColorConditional(DisabledColor);
                            }
                        }
                    }
                    break;
                }
            }
        }

        private Color ColorConditional(Color c)
        {
            //Debug.WriteLine(c);
            if (c == Color.Transparent)
            {
                //Debug.WriteLine(this.BackColor);
                return this.BackColor;
            }
            return c;
        }

        public struct IndexPage
        {
            public int Index;
            public TabPage Page;
            public bool Enabled;
        }

        public CUButton GetButtonForTab(TabControl tab)
        {
            return null;
        }

        public void Init()
        {
            this.Controls.Clear();
            if (ParentTabControl == null) return;
            var list = new List<CUButton>();
            int i = 0;
            foreach (var p in ParentTabControl.TabPages)
            {
                var page = (TabPage)p;
                var btn = new CUButton
                {
                    Text = page.Text,
                    Tag = new IndexPage()
                    {
                        Index = i,
                        Page = page,
                        Enabled = true
                    },
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0, 0, 0, 1),
                    ImageAlign = ContentAlignment.TopCenter,
                    TextImageRelation = TextImageRelation.ImageAboveText
                };
                btn.FlatAppearance.BorderSize = 0;
                if (ParentTabControl.ImageList != null)
                {  
                    btn.ImageList = ParentTabControl.ImageList;
                    btn.ImageIndex = page.ImageIndex;
                }
                list.Add(btn);
                i++;
            }
            TableLayoutPanel panel = new TableLayoutPanel();
            panel.RowStyles.Clear();
            for (i = 0; i != list.Count(); i++)
            {
                panel.RowStyles.Add(new RowStyle()
                {
                    SizeType = SizeType.Percent,
                    Height = 100 / list.Count()
                });
                panel.Controls.Add(list[i], 0, i);
                list[i].Click += TabController_Click;
            }
            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(0, 0);
            this.Controls.Add(panel);
            UpdateButtonStates();
        }

        private void TabController_Click(object sender, EventArgs e)
        {
            var page = (IndexPage)(((Button)sender).Tag);
            var eventArg = new TabControllerPageChangeEventArgs()
            {
                PageIndex = page.Index,
                IsCancelled = false
            };
            PageChange?.Invoke(this, eventArg);
            if (!eventArg.IsCancelled)
            {
                ParentTabControl.SelectedTab = page.Page;
            }
            UpdateButtonStates();
        }
    }

    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class TabControllerDesigner : ControlDesigner
    {

        public TabControllerDesigner()
        {
        }

        protected override bool GetHitTest(System.Drawing.Point point)
        {
            return true;
        }
    }
}
