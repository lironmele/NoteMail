using System;
using System.Windows.Forms;

namespace NoteMail_LironMeler
{
    public partial class DeleteMember : Form
    {
        public DeleteMember()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (comboxMember.SelectedItem != null)
                DBHandlerLiron.GetInstance().DeletePlayer(DBHandlerLiron.GetInstance().GetIdWithName(comboxMember.Text));
        }

        private void DeleteMember_Load(object sender, EventArgs e)
        {
            int[] ids = DBHandlerLiron.GetInstance().GetAllMembers(0);
            for (int i = 0; i < ids.Length; i++)
                comboxMember.Items.Add(DBHandlerLiron.GetInstance().GetNameWithId(ids[i]));
        }
    }
}
