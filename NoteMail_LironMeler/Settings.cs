using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NoteMail_LironMeler
{
    public partial class Settings : Form
    {
        int id { get; set; }
        string path { get; set; }
        public Settings(int id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            Text = "Update " + DBHandlerLiron.GetInstance().GetNameWithId(id);
            path = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool update = false;
            if (txtName.Text != "")
            {
                DBHandlerLiron.GetInstance().UpdateName(id, txtName.Text);
                update = true;
            }
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
                DBHandlerLiron.GetInstance().InsertProfilePicture(stream.ToArray(), id);
                update = true;
            }
            if (txtStatus.Text != "")
            {
                DBHandlerLiron.GetInstance().InsertStatus(id, txtStatus.Text);
                update = true;
            }
            if (update)
                MessageBox.Show("Updated successfully\n          *\n         * *\n        *   *\n     ***      ***\n      *        *\n       *      *\n      *    *   *\n     *           *\n");
            Close();
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
