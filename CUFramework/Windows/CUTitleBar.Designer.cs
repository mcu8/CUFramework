namespace CUFramework.Windows
{
    partial class CUTitleBar
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cuControlBox1 = new CUFramework.Windows.CUControlBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.label1.Size = new System.Drawing.Size(579, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "TITLE TEXT";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cuControlBox1
            // 
            this.cuControlBox1.BackColor = System.Drawing.Color.Teal;
            this.cuControlBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.cuControlBox1.IsCloseButtonEnabled = true;
            this.cuControlBox1.IsMaximizeButtonEnabled = true;
            this.cuControlBox1.IsMinimizeButtonEnabled = true;
            this.cuControlBox1.Location = new System.Drawing.Point(435, 0);
            this.cuControlBox1.Margin = new System.Windows.Forms.Padding(0);
            this.cuControlBox1.MinimumSize = new System.Drawing.Size(144, 32);
            this.cuControlBox1.Name = "cuControlBox1";
            this.cuControlBox1.Size = new System.Drawing.Size(144, 32);
            this.cuControlBox1.TabIndex = 1;
            this.cuControlBox1.Text = "cuControlBox1";
            // 
            // CUTitleBar
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.cuControlBox1);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(0, 32);
            this.MinimumSize = new System.Drawing.Size(0, 32);
            this.Name = "CUTitleBar";
            this.Size = new System.Drawing.Size(579, 32);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private CUControlBox cuControlBox1;
    }
}
