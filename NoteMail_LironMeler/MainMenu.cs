using System;
using System.Windows.Forms;

namespace NoteMail_LironMeler
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            comboxType.SelectedIndex = 0;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (comboxType.SelectedItem.ToString() == "Admin" && DBHandlerLiron.GetInstance().GetPass("Admin", txtName.Text) == txtPassword.Text)
            {
                Hide();
                new AdminMenu().ShowDialog();
                Show();
                txtName.Text = "";
                txtPassword.Text = "";
                comboxType.SelectedIndex = 0;
            }
            else if (comboxType.SelectedItem.ToString() != "Admin" && DBHandlerLiron.GetInstance().GetPass("Member", txtName.Text) == txtPassword.Text)
            {
                Hide();
                if (comboxType.SelectedItem.ToString() == "Member")
                    new App(DBHandlerLiron.GetInstance().GetIdWithName(txtName.Text)).ShowDialog();
                else if (comboxType.SelectedItem.ToString() == "Settings")
                    new Settings(DBHandlerLiron.GetInstance().GetIdWithName(txtName.Text)).ShowDialog();
                Show();
                txtName.Text = "";
                txtPassword.Text = "";
                comboxType.SelectedIndex = 0;
            }
            else
                MessageBox.Show("The name or password were incorrect");
        }
    }
}
