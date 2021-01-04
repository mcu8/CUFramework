using CUFramework.Dialogs;
using CUFramework.Dialogs.Validators;
using CUFramework.Windows;
using System;
using System.Windows.Forms;

namespace ModdingTools.Windows
{
    public partial class CUInputWindow : CUWindow
    {
        public IValidator Validator = null;

        public CUInputWindow()
        {
            InitializeComponent();
        }

        public static string Ask(Control parent, string title, string text, IValidator validator = null, string def = "", bool multiline = false)
        {
            
            if (Form.ActiveForm != null && Form.ActiveForm.InvokeRequired)
            {
                var output = "";
                Form.ActiveForm.Invoke(new MethodInvoker(() =>
                {
                    output = CUInputWindow.Ask(parent, title, text, validator);
                }));
                return output;
            }

            var t = new CUInputWindow();
            if (parent == null) parent = Form.ActiveForm;

            t.Validator = validator;
            t.label1.Text = text;
            t.Text = title;
            t.InputBox.Text = def;
            t.StartPosition = FormStartPosition.CenterParent;

            if (multiline)
            {
                t.InputBox.Location = new System.Drawing.Point(t.InputBox.Location.X, t.InputBox.Location.Y - t.InputBox.Height);
                t.InputBox.Multiline = true;
                t.label1.Height -= t.InputBox.Height;
                t.InputBox.ScrollBars = ScrollBars.Vertical;
                t.InputBox.WordWrap = true;
                t.InputBox.Height *= 2;
            }

            if (t.ShowDialog(parent.FindForm()) == DialogResult.OK)
            {
                return t.InputBox.Text;
            }

            return null;
        }

        private void mButton2_Click(object sender, EventArgs e)
        {
            if (Validator != null)
            {
                var val = Validator.Validate(InputBox.Text);
                if (val != null)
                {
                    CUMessageBox.Show(this, val, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void mButton3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mButton2_Click(null, null);
            }
        }
    }
}
