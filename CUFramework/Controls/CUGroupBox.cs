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

namespace CUFramework.Controls
{
    [Designer(typeof(CUGroupBoxDesigner))]
    public class CUGroupBox : Panel
    {
        [Browsable(true)]
        [Category("CUFramework")]
        public Color HeaderColor { get; set; } = ThemeConstants.GroupBoxHeaderColor;
        [Browsable(true)]
        [Category("CUFramework")]
        public Color HeaderFontColor { get; set; } = ThemeConstants.GroupBoxHeaderForeColor;
        private int p_BorderSize = 3;
        [Browsable(true)]
        [Category("CUFramework")]
        public int BorderSize
        {
            get => p_BorderSize; set
            {
                p_BorderSize = value;
                CUGroupBox_ParentChanged(null, null);
            }
        }
        [Browsable(true)]
        [Category("CUFramework")]
        public int HeaderHeight { get; set; } = 25;

        private string p_HeaderText;
        [Browsable(true)]
        [Category("CUFramework")]
        public string HeaderText
        {
            get
            {
                return string.IsNullOrEmpty(p_HeaderText) ? Name : p_HeaderText;
            }
            set
            {
                p_HeaderText = value;
            }
        }

        public CUGroupBox()
        {
            this.ParentChanged += CUGroupBox_ParentChanged;
            this.HeaderText = this.Name;
        }

        private void CUGroupBox_ParentChanged(object sender, EventArgs e)
        {
            this.Padding = new Padding(BorderSize, BorderSize + HeaderHeight, BorderSize, BorderSize);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //if (this.DesignMode) return;

            var border = new Pen(ThemeConstants.BorderColor, BorderSize);
            var headBrush = new SolidBrush(HeaderColor);

            var area = new Rectangle(new Point(0, 0), new Size(this.Size.Width - BorderSize, this.Size.Height - BorderSize));
            var head = new Rectangle(new Point(BorderSize, BorderSize), new Size(this.Size.Width - BorderSize * 2, HeaderHeight + BorderSize));

            try
            {
                string drawString = HeaderText;
                Font drawFont = new Font(this.Font.FontFamily, this.Font.Size, this.Font.Style, this.Font.Unit);//this.Font;

                SolidBrush drawBrush = new SolidBrush(this.HeaderFontColor);
                float x = BorderSize + (drawFont.GetHeight() / 2);
                float y = HeaderHeight / 2 - drawFont.GetHeight() / 2 + BorderSize;
                StringFormat drawFormat = new StringFormat();
                drawFormat.Alignment = StringAlignment.Near;
                Debug.WriteLine(drawString);

                e.Graphics.FillRectangle(headBrush, head);
                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                if (BorderSize > 0) e.Graphics.DrawRectangle(border, area);

                drawFont.Dispose();
                drawBrush.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.ToString());
            }
        }
    }

    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class CUGroupBoxDesigner : ScrollableControlDesigner
    {
        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            //base.OnPaintAdornments(pe);
        }
    }
}
