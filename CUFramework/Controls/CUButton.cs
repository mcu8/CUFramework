using CUFramework.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace CUFramework.Controls
{
    public class CUButton : CUButtonBase
    {
        public CUButton()
        {
            UseVisualStyleBackColor = true;
            this.FlatStyle = FlatStyle.Flat;
            this.BackColor = ThemeConstants.BorderColor;
            this.FlatAppearance.BorderColor = ThemeConstants.BorderColor;
            this.FlatAppearance.BorderSize = 1;
            this.ForeColor = ThemeConstants.ForegroundColor;
        }
    }
}
