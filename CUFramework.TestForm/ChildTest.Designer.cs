namespace CUFramework.TestForm
{
    partial class ChildTest
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cuButton1 = new CUFramework.Controls.CUButton();
            this.SuspendLayout();
            // 
            // cuButton1
            // 
            this.cuButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.cuButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.cuButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cuButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cuButton1.ForeColor = System.Drawing.Color.White;
            this.cuButton1.Image = global::CUFramework.TestForm.Properties.Resources.unknown;
            this.cuButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cuButton1.Location = new System.Drawing.Point(5, 37);
            this.cuButton1.Name = "cuButton1";
            this.cuButton1.NoFocus = false;
            this.cuButton1.Size = new System.Drawing.Size(357, 127);
            this.cuButton1.TabIndex = 1;
            this.cuButton1.Text = "CONVERT UE3 TO UE5";
            this.cuButton1.UseVisualStyleBackColor = false;
            // 
            // ChildTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 169);
            this.Controls.Add(this.cuButton1);
            this.IsMaximizeButtonEnabled = false;
            this.IsMinimizeButtonEnabled = false;
            this.IsResizable = false;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "ChildTest";
            this.Text = "UlbtraDK3toReal5";
            this.Controls.SetChildIndex(this.cuButton1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CUButton cuButton1;
    }
}