using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CUFramework.Controls
{
    public class CUButtonBase : Button
    {
        [Browsable(true)]
        [Category("CUFramework")]
        public bool NoFocus { get; set; } = false;

        public new Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                if (value != Color.Transparent)
                    if (Parent != null && Parent.BackColor != Color.Transparent)
                        base.FlatAppearance.BorderColor = Parent.BackColor;
                    else
                        base.FlatAppearance.BorderColor = value;
            }
        }

        protected override bool ShowFocusCues
        {
            get
            {
                return !NoFocus;
            }
        }
    }
}
