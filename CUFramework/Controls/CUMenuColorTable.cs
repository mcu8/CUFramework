using CUFramework.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CUFramework.Controls
{
    public class CUMenuColorTable : ProfessionalColorTable
    {
        public CUMenuColorTable()
        {
            // see notes
            base.UseSystemColors = false;
        }
        public override System.Drawing.Color MenuBorder
        {
            get { return ThemeConstants.BorderColor; }
        }
        public override System.Drawing.Color MenuItemBorder
        {
            get { return ThemeConstants.BorderColor; }
        }
        public override Color MenuItemSelected
        {
            get { return ThemeConstants.BorderColor; }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return ThemeConstants.BorderColor; }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return ThemeConstants.BorderColor; }
        }
        public override Color MenuStripGradientBegin
        {
            get { return ThemeConstants.BackgroundColor; }
        }
        public override Color MenuStripGradientEnd
        {
            get { return ThemeConstants.BackgroundColor; }
        }
        public override Color ToolStripBorder { get => ThemeConstants.BorderColor; }
        public override Color ToolStripDropDownBackground { get => ThemeConstants.BackgroundColor; }
        public override Color ToolStripGradientBegin { get => ThemeConstants.BackgroundColor; }
        public override Color ToolStripGradientMiddle { get => ThemeConstants.BackgroundColor; }
        public override Color ToolStripGradientEnd { get => ThemeConstants.BackgroundColor; }
        public override Color ToolStripContentPanelGradientBegin { get => ThemeConstants.BackgroundColor; }
        public override Color ToolStripContentPanelGradientEnd { get => ThemeConstants.BackgroundColor; }
        public override Color ToolStripPanelGradientBegin { get => ThemeConstants.BackgroundColor; }
        public override Color ToolStripPanelGradientEnd { get => ThemeConstants.BackgroundColor; }
        public override Color OverflowButtonGradientBegin { get => ThemeConstants.BackgroundColor; }
        public static void Apply(MenuStrip strip)
        {
            strip.Renderer = new ToolStripProfessionalRenderer(new CUMenuColorTable());
            strip.RenderMode = ToolStripRenderMode.Professional;
            strip.BackColor = ThemeConstants.BackgroundColor;
            strip.ForeColor = ThemeConstants.ForegroundColor;
            Revalidate(strip);
        }

        public static void Revalidate(MenuStrip strip, ToolStripItemCollection col = null)
        {
            if (col == null)
                col = strip.Items;
            foreach (var item in col)
            {
                if (item is ToolStripMenuItem)
                {
                    var it = ((ToolStripMenuItem)item);
                    it.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    it.BackColor = ThemeConstants.BackgroundColor;
                    it.ForeColor = ThemeConstants.ForegroundColor;

                    if (it.HasDropDownItems)
                    {
                        Revalidate(strip, it.DropDownItems);
                    }
                }
            }
        }
    }
}
