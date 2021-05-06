using System;
using System.Reflection;
using System.Windows.Forms;

namespace Furry
{
    internal partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
        }
    
        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
