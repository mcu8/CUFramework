using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CUFramework.Windows
{
    partial class CUWindow : Form
    {
        private Form p_ToCenterParent = null;
        public Form ToCenterParent
        {
            get => p_ToCenterParent; set
            {
                if (this.Icon == new Form().Icon) // hack, but works
                    this.Icon = value.Icon;
                Debug.WriteLine(this.Icon);
                p_ToCenterParent = value;
            }
        }

        struct Resolution
        {
            public int Width;
            public int Height;
        }

        int previous = -1;
        int current = -1;

        private bool CheckScreenChanged()
        {
            bool changed = false;
            current = GetScreenIndex();

            if (current != -1 && previous != -1 && current != previous) // form changed screen.
            {
                changed = true;
            }

            previous = current;

            return changed;
        }

        private int GetScreenIndex()
        {
            return Array.IndexOf(Screen.AllScreens, Screen.FromControl(this));
        }

        private Resolution GetCurrentResolution()
        {
            Screen screen = Screen.FromControl(this);
            Resolution res = new Resolution();
            res.Width = screen.Bounds.Width;
            res.Height = screen.Bounds.Height;

            return res;
        }

        private void SetResolutionLabel()
        {
            Resolution res = GetCurrentResolution();
            Debug.WriteLine(String.Format("Width: {0}, Height: {1}", res.Width, res.Height));
            this.MaximumSize = Screen.GetWorkingArea(this).Size;
        }

        private void ScreenChanged()
        {
            Debug.WriteLine("Screen " + current.ToString());
            this.MaximumSize = Screen.GetWorkingArea(this).Size;
        }

        private void Form_Moved(object sender, System.EventArgs e)
        {
            bool changed = CheckScreenChanged();
            if (changed == true)
            {
                ScreenChanged();
                SetResolutionLabel();
            }
        }

        public void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            SetResolutionLabel();
        }

        public void InitializeResolutionHandler()
        {
            this.Move += Form_Moved;
            SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;

            previous = GetScreenIndex();
            current = GetScreenIndex();
            ScreenChanged();
            SetResolutionLabel();
        }

        // Windows Forms sucks
        public void InitWindowLoc()
        {
            if (DesignMode) return;
            var screen = Screen.GetWorkingArea(this);
            Size sz = screen.Size;
            Point pos = screen.Location;//new Point(sz.Width / 2, sz.Height / 2);

            if (StartPosition == FormStartPosition.CenterParent && this.ToCenterParent != null)
            {
                Debug.WriteLine("Parent form detected!");
                sz = this.ToCenterParent.Size;
                pos = this.ToCenterParent.Location;

                var x = pos.X + ((sz.Width - Width) / 2);
                var y = pos.Y + ((sz.Height - Height) / 2);

                //var x = pos.X + ((sz.Width / 2) - (Width / 2));
                //var y = pos.Y + ((sz.Height / 2) - (Height / 2));

                Location = new Point(x, y);
            }
            else if (
                StartPosition == FormStartPosition.CenterScreen || 
                StartPosition == FormStartPosition.CenterParent ||
                StartPosition == FormStartPosition.WindowsDefaultBounds ||
                StartPosition == FormStartPosition.WindowsDefaultLocation
            )
            {
                var x = pos.X + ((sz.Width / 2) - (Width / 2));
                var y = pos.Y + ((sz.Height / 2) - (Height / 2));

                Location = new Point(x, y);
            }
            Debug.WriteLine("-----");
            Debug.WriteLine(sz);
            Debug.WriteLine(pos);
            Debug.WriteLine(StartPosition);
            Debug.WriteLine(Location);
            Debug.WriteLine("-----");
        }
    }
}
