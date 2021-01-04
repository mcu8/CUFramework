using CUFramework.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CUFramework.Controls
{
    public class CUConsoleControl : RichTextBox
    {
        public CUConsoleControl()
        {
            this.BackColor = System.Drawing.Color.Black;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Plum;
            this.ReadOnly = true;
        }

        public void Log(string msg, LogLevel level)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => Log(msg, level)));
                return;
            }

            var color = GetLevelColor(level);

            AppendLoggerText($"[{GetLoggerDate()}][{level.ToString()}] {msg}{Environment.NewLine}", color);
            this.SelectionStart = this.Text.Length;
            this.ScrollToCaret();
        }

        public static Color GetLevelColor(LogLevel level)
        {
            var color = Color.White;
            switch (level)
            {
                case LogLevel.Error:
                    color = Color.Red;
                    break;
                case LogLevel.Warn:
                    color = Color.Yellow;
                    break;
                case LogLevel.Info:
                    color = Color.White;
                    break;
                case LogLevel.Success:
                    color = Color.Green;
                    break;
                case LogLevel.Verbose:
                    color = Color.Gray;
                    break;
            }
            return color;
        }

        public static string GetLoggerDate()
        {
            System.DateTime regDate = System.DateTime.Now;
            return regDate.ToString("HH:mm:ss");
        }

        public void AppendLoggerText(string text, Color color)
        {
            this.SelectionStart = this.TextLength;
            this.SelectionLength = 0;

            this.SelectionColor = color;
            this.AppendText(text);
            this.SelectionColor = this.ForeColor;
        }
    }
}
