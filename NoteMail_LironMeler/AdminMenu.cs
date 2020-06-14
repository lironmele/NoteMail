using System;
using System.Windows.Forms;

namespace NoteMail_LironMeler
{
    public partial class AdminMenu : Form
    {
        public AdminMenu()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Hide();
            new AddMember().ShowDialog();
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Hide();
            new DeleteMember().ShowDialog();
            Close();
        }
    }
}
