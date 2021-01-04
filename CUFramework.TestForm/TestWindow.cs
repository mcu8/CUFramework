using CUFramework.Dialogs;
using CUFramework.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CUFramework.TestForm
{
    public partial class TestWindow : CUWindow
    {
        public TestWindow()
        {
            InitializeComponent();
        }

        private void cuProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void cuProgressBar1_Click_1(object sender, EventArgs e)
        {

        }

        private void cuButton2_Click(object sender, EventArgs e)
        {
            var test = new ChildTest();
            test.StartPosition = FormStartPosition.CenterParent;
            test.ToCenterParent = this;
            test.Show();
        }

        private void cuButton3_Click(object sender, EventArgs e)
        {
            CUMessageBox.Show("test");
        }
    }
}
