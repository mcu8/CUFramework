using CUFramework.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CUFramework.Controls
{
    public class CUProgressBar : Control
    {
        
        private float _MaxValue = 100;
        private float _Value = 0;

        [Browsable(true)]
        [Category("CUFramework")]
        public float MaxValue
        {
            get => _MaxValue;
            set
            {
                _MaxValue = value;
                if (value > _MaxValue)
                {
                    _Value = value;
                }
            }
        }
        [Browsable(true)]
        [Category("CUFramework")]
        public float Value
        {
            get => _Value;
            set
            {
                _Value = value > _MaxValue ? _MaxValue : value;
            }
        }
        [Browsable(true)]
        [Category("CUFramework")]
        public int BorderSize { get; set; } = 2;
        [Browsable(true)]
        [Category("CUFramework")]
        public Color ProgressBarColor { get; set; } 

        public CUProgressBar()
        {
            ForeColor = ThemeConstants.ProgressBarForeColor;
            ProgressBarColor = ThemeConstants.ProgressBarColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //if (this.DesignMode) return;

            var border = new Pen(ThemeConstants.BorderColor, BorderSize);
            var blueBrush = new SolidBrush(ProgressBarColor);

            var area = new Rectangle(new Point(0, 0), new Size(this.Size.Width - BorderSize, this.Size.Height - BorderSize));
            var areaPG = new Rectangle(new Point(BorderSize * 2, BorderSize * 2), new Size((int)((this.Size.Width - (BorderSize * 4)) * (Value/MaxValue)), this.Size.Height - (BorderSize * 4)));

            if (!string.IsNullOrEmpty(this.Text))
            {
                try
                {
                    string drawString = this.Text;
                    Font drawFont = new Font(this.Font.FontFamily, this.Font.Size, this.Font.Style, this.Font.Unit);//this.Font;
                    
                    SolidBrush drawBrush = new SolidBrush(this.ForeColor);
                    float x = this.Size.Width / 2;
                    float y = (this.Size.Height / 2) - (drawFont.GetHeight() / 2);
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.Alignment = StringAlignment.Center;

                    e.Graphics.FillRectangle(blueBrush, areaPG);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                    e.Graphics.DrawRectangle(border, area);

                    drawFont.Dispose();
                    drawBrush.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.ToString());
                }
            }
            else
            {
                e.Graphics.DrawRectangle(border, area);
                e.Graphics.FillRectangle(blueBrush, areaPG);
            }
        }
    }
}
