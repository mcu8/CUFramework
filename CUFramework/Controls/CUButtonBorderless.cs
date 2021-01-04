﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CUFramework.Controls
{
    public class CUButtonBorderless : CUButtonBase
    {
        public CUButtonBorderless()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.FlatAppearance.BorderSize = 0;
            this.ForeColor = System.Drawing.Color.FromArgb(52, 152, 219);
        }
    }
}
