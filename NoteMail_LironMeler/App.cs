using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NoteMail_LironMeler
{
    public partial class App : Form
    {
        TblMember Member;
        Button[] button_chat;
        PictureBox[] picture_chat;
        LinkLabel[] label_chat;
        int c_index;
        public void InitializeChats(TblMember member)
        {
            button_chat = new Button[member.c_ids.Length];
            picture_chat = new PictureBox[member.c_ids.Length];
            const int x = -2;
            int y = richtxtChat.Location.Y;
            for (int i = 0; i < member.c_ids.Length; i++)
            {
                button_chat[i] = new Button();
                button_chat[i].Location = new Point(x, y);
                button_chat[i].Name = "btnChat_" + i.ToString();
                button_chat[i].Text = DBHandlerLiron.GetInstance().GetChatName(member.MemberId, member.c_ids[i]);
                button_chat[i].Size = new Size(btnAddChat.Size.Width, 43);
                button_chat[i].Font = new Font("Arial Narrow", 16);
                button_chat[i].Padding = new Padding(0);
                button_chat[i].Click += new EventHandler(button_click);
                if (member.chats[i].Receiver.Length > 1)
                {
                    SizeButtonFont(button_chat[i]);
                }
                Controls.Add(button_chat[i]);

                if (member.chats[i].Receiver.Length == 1)
                {
                    picture_chat[i] = new PictureBox();
                    picture_chat[i].Name = "pPic_" + i.ToString();
                    if (member.chats[i].Receiver.Length == 1)
                        picture_chat[i].Image = DBHandlerLiron.GetInstance().GetProfilePathWithMember(Member.chats[i].Receiver[0]);
                    picture_chat[i].BringToFront();
                    picture_chat[i].Location = new Point(x + 5, y + 7);
                    picture_chat[i].SizeMode = PictureBoxSizeMode.StretchImage;
                    picture_chat[i].Size = new Size(30, 30);
                    picture_chat[i].Visible = true;
                    Controls.Add(picture_chat[i]);
                    button_chat[i].SendToBack();
                }
                y += 43;
            }
            btnAddChat.Location = new Point(x, y);
            btnAddChat.SendToBack();
        }
        private void SizeButtonFont(Button btn)
        {
            int best_size = 100;

            int wid = btn.DisplayRectangle.Width - 10;
            int hgt = btn.DisplayRectangle.Height - 10;

            using (Graphics gr = btn.CreateGraphics())
            {
                for (int i = 1; i <= 100; i++)
                {
                    using (Font test_font = new Font(btn.Font.FontFamily, i))
                    {
                        SizeF text_size = gr.MeasureString(btn.Text, test_font);
                        if (text_size.Width > wid || text_size.Height > hgt)
                        {
                            best_size = i - 1;
                            break;
                        }
                    }
                }
            }

            btn.Font = new Font(btn.Font.FontFamily, best_size);
        }
        public string AddMessage(TblMessage[] messages)
        {
            string message = "";
            for (int i = 0; i < messages.Length; i++)
                if (messages[i].s_id == Member.MemberId)
                    message += "Me: " + messages[i].content + "\n";
                else
                    message += DBHandlerLiron.GetInstance().GetNameWithId(messages[i].s_id) + ": " + messages[i].content + "\n";
            return message;
        }
        public void AddAttachments(int[] a_ids)
        {
            label_chat = new LinkLabel[a_ids.Length];
            int x = 3, y = 40;
            for (int i = 0; i < a_ids.Length; i++)
            {
                label_chat[i] = new LinkLabel();
                label_chat[i].Text = DBHandlerLiron.GetInstance().SelectFileDetails(a_ids[i]);
                label_chat[i].Name = "lblLink_" + i.ToString();
                label_chat[i].Location = new Point(x, y);
                label_chat[i].BringToFront();
                label_chat[i].Visible = true;
                label_chat[i].Click += linklabel_clicked;
                label_chat[i].AutoSize = true;
                pnlAtta.Controls.Add(label_chat[i]);
                y += 32;
            }
        }
        public App(int id)
        {
            Member = new TblMember(id);
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            richtxtChat.Text += $"Me: {txtBox.Text}\n";
            DBHandlerLiron.GetInstance().InsertMessage(txtBox.Text, Member.MemberId, Member.chats[c_index].Id);
            txtBox.Clear();
        }
        private void button_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            c_index = Array.IndexOf(button_chat, btn);
            richtxtChat.Visible = true;
            richtxtChat.Text = AddMessage(Member.chats[c_index].Messages);
            richtxtChat.Enabled = true;
            if (Member.chats[c_index].Receiver.Length == 1)
                lblStatus.Text = $"\"{DBHandlerLiron.GetInstance().GetStatus(Member.chats[c_index].Receiver[0])}\" ~ {DBHandlerLiron.GetInstance().GetNameWithId(Member.chats[c_index].Receiver[0])}";
            else
                lblStatus.Text = "";
            txtBox.Enabled = true;
            btnAttachment.Enabled = true;
            btnSend.Enabled = true;
            lblStatus.Visible = true;
            lblAttachments.Visible = true;
            foreach (var control in pnlAtta.Controls.OfType<LinkLabel>().ToList())
                pnlAtta.Controls.Remove(control);
            Member.chats[c_index].Att_ids = DBHandlerLiron.GetInstance().GetAttaIds(Member.c_ids[c_index]);
            AddAttachments(Member.chats[c_index].Att_ids);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Member.chats.Length > 0 && DBHandlerLiron.GetInstance().CountIds(Member.chats[c_index].Id) != Member.chats[c_index].Messages.Length)
            {
                Member.chats[c_index] = DBHandlerLiron.GetInstance().GetChat(Member.chats[c_index].Id, DBHandlerLiron.GetInstance().CountIds(Member.chats[c_index].Id));
                Member.UpdateReceiver(c_index);
                richtxtChat.Text = AddMessage(Member.chats[c_index].Messages);
            }
            if (Member.chats.Length > 0 && DBHandlerLiron.GetInstance().CountAttIds(Member.chats[c_index].Id) != Member.chats[c_index].Att_ids.Length)
            {
                foreach (var control in pnlAtta.Controls.OfType<LinkLabel>().ToList())
                    pnlAtta.Controls.Remove(control);
                AddAttachments(Member.chats[c_index].Att_ids);
            }
            if (Member.chats.Length > 0 && DBHandlerLiron.GetInstance().CountChatIds(Member.MemberId) != Member.chats.Length && this == ActiveMdiChild)
            {
                Visible = false;
                tmrCheckData.Stop();
                new App(Member.MemberId).ShowDialog();
                Close();
            }
        }

        private void App_Load(object sender, EventArgs e)
        {
            InitializeChats(Member);
            Text = DBHandlerLiron.GetInstance().GetNameWithId(Member.MemberId);
        }

        private void btnAddChat_Click(object sender, EventArgs e)
        {
            AddChat input = new AddChat(Member.MemberId);
            if (input.ShowDialog(this) == DialogResult.OK && input.CountId != 0)
            {
                int[] ids = new int[input.CountId];
                for (int i = 0; i < input.CountId; i++)
                {
                    ids[i] = input.ids[i];
                }
                DBHandlerLiron.GetInstance().InsertChat(Member.MemberId, ids);
                Visible = false;
                Member.Active = false;
                tmrCheckData.Stop();
                new App(Member.MemberId).ShowDialog();
                Close();
            }
        }

        private void btnAttachment_Click(object sender, EventArgs e)
        {
            DialogResult result = opnFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                FileStream fs = new FileStream(opnFile.FileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                byte[] fileContents = br.ReadBytes((int)fs.Length);
                DBHandlerLiron.GetInstance().InsertAttachment(fileContents, opnFile.SafeFileName, Member.c_ids[c_index], Member.MemberId);
                Member.chats[c_index].Att_ids = DBHandlerLiron.GetInstance().GetAttaIds(Member.c_ids[c_index]);
                AddAttachments(Member.chats[c_index].Att_ids);
            }
        }
        public void linklabel_clicked(object sender, EventArgs e)
        {
            LinkLabel lbl = (LinkLabel)sender;
            int lbl_index = Array.IndexOf(label_chat, lbl);
            DBHandlerLiron.GetInstance().DownloadAttachment(Member.chats[c_index].Att_ids[lbl_index]);
            MessageBox.Show("Download installed successfully!");
        }
    }
}
