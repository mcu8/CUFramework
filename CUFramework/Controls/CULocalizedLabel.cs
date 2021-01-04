using CUFramework.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CUFramework.Controls
{
    public class CULocalizedLabel : Label
    {
        private string _UnlocalizedText;

        [Browsable(true)]
        [Category("CUFramework")]
        public string UnlocalizedText
        {
            get
            {
                return _UnlocalizedText;
            }
            set
            {
                _UnlocalizedText = value;
                Text = GetLocalized();
                Invalidate();
            }
        }

        public string GetLocalized()
        {
            if (DesignMode) return _UnlocalizedText;
            return LocManager.GetLocaleFor("Labels", LocManager.Locale.INT).GetLocalizedString(_UnlocalizedText);
        }
    }
}
