using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NoteMail_LironMeler
{
    public partial class AddMember : Form
    {
        string path { get; set; }
        public AddMember()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtPassword.Text != "")
            {
                int new_id = DBHandlerLiron.GetInstance().InsertMember(txtName.Text, txtPassword.Text);
                if (path != "")
                {
                    MemoryStream stream = new MemoryStream();
                tryagain:
                    try
                    {
                        Bitmap image = new Bitmap(path);
                        image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        goto tryagain;
                    }
                    DBHandlerLiron.GetInstance().InsertProfilePicture(stream.ToArray(), new_id);
                }
                if (txtStatus.Text != "")
                    DBHandlerLiron.GetInstance().InsertStatus(new_id, txtStatus.Text);
                Close();
            }
            else
            {
                MessageBox.Show("Please input name and password!");
            }
        }

        private void AddMember_Load(object sender, EventArgs e)
        {
            path = "";
        }
        private void btnPP_Click(object sender, EventArgs e)
        {
            DialogResult result = opnProfile.ShowDialog();
            if (result == DialogResult.OK)
            {
                string extn = System.IO.Path.GetExtension(opnProfile.FileName);
                if (extn == ".png" || extn == ".jpg" || extn == ".jpeg" || extn == ".jpe" || extn == ".jfif")
                    path = opnProfile.FileName;
                else
                    MessageBox.Show("Please select image file");
            }
        }
    }
}
