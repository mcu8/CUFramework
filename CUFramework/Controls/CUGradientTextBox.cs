using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CUFramework.Controls
{
    public class CUGradientTextBox : Control
    {
        const int BufferSize = 100;
        const int TimerInterval = 10;

        private List<StringColor> Lines { get; set; } = new List<StringColor>();
        private bool PendingUpdate = false;
        private Timer UpdateTimer;
        private string LastString;
        public ETextAlign TextAlign { get; set; } = ETextAlign.Left;

        struct StringColor
        {
            public string Text;
            public Color Color;
        }

        public enum ETextAlign
        {
            Center, Left
        }

        public void Insert(string v)
        {
            Insert(v, Color.Empty);
        }

        public void Clear()
        {
            LastString = null;
            Lines.Clear();
            PendingUpdate = true;
        }

        public void Insert(string v, Color color)
        {
            if (v == LastString) return; // don't
            if (color == Color.Empty)
                color = ForeColor;
            if (Lines.Count >= BufferSize)
                Lines.RemoveRange(BufferSize, Lines.Count - BufferSize);
            LastString = v;
            Lines.Insert(0, new StringColor()
            {
                Text = v,
                Color = color
            });
            PendingUpdate = true;
        }

        public CUGradientTextBox()
        {
            this.DoubleBuffered = true;
            this.Paint += this.OnPaint;
            UpdateTimer = new System.Windows.Forms.Timer();
            UpdateTimer.Interval = TimerInterval;
            UpdateTimer.Tick += UpdateTimer_Tick;
            UpdateTimer.Start();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (PendingUpdate)
                this.Refresh();
            PendingUpdate = false;
        }

        protected void OnPaint(object sender, PaintEventArgs e)
        {
            Font drawFont = new Font(this.Font.FontFamily, this.Font.Size, this.Font.Style, this.Font.Unit);//this.Font;
            SolidBrush drawBrush = new SolidBrush(this.ForeColor);
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = TextAlign == ETextAlign.Center ? StringAlignment.Center : StringAlignment.Near;

            int fontH = (int)drawFont.GetHeight();
            int spacing = 4;

            Debug.WriteLine("FontH: " + fontH);
            int limit = ((this.Height - spacing) / (spacing + fontH));

            Debug.WriteLine("Limit: " + limit);

            int index = 0;
            int x = TextAlign == ETextAlign.Center ? this.Width / 2 : 0;

            int y = (this.Height - spacing / 2) - fontH - spacing / 2;
            float off;
            float r, g, b;

            StringColor[] lines = new StringColor[this.Lines.Count];
            this.Lines.CopyTo(lines); // prevent concurrent exception here
            foreach (var ln in lines)
            {
                off = 1f - ((float)index / (float)limit);
                r = Math.Max(ln.Color.R * off, this.BackColor.R);
                g = Math.Max(ln.Color.G * off, this.BackColor.G);
                b = Math.Max(ln.Color.B * off, this.BackColor.B);
                drawBrush.Color = Color.FromArgb((int)r, (int)g, (int)b);
                e.Graphics.DrawString(ln.Text, drawFont, drawBrush, x, y, drawFormat);
                y -= fontH + spacing;
                index++;
                if (index == limit) break;   
                Debug.WriteLine(off);             
            }

            drawFont.Dispose();
        }
    }
}
