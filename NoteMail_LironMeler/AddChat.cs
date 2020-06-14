using System;
using System.Windows.Forms;

namespace NoteMail_LironMeler
{
    public partial class AddChat : Form
    {
        int m_id { get; set; }
        public int[] ids { get; set; }
        public int CountId { get; set; }
        public AddChat(int m_id)
        {
            this.m_id = m_id;
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (comboxChats.Items.Count != 0)
            {
                lblPar.Text += comboxChats.SelectedItem.ToString() + " ";
                ids[CountId++] = DBHandlerLiron.GetInstance().GetIdWithName(comboxChats.SelectedItem.ToString());
                comboxChats.Items.Remove(comboxChats.SelectedItem);
                try { comboxChats.SelectedIndex = 0; }
                catch { }
            }
        }

        private void AddChat_Load(object sender, EventArgs e)
        {
            int[] ids = DBHandlerLiron.GetInstance().GetAllMembers(m_id);
            foreach (int id in ids)
            {
                comboxChats.Items.Add(DBHandlerLiron.GetInstance().GetNameWithId(id));
            }
            if (ids.Length > 0)
                comboxChats.SelectedIndex = 0;
            this.ids = new int[ids.Length];
            CountId = 0;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
