namespace NoteMail_LironMeler
{
    partial class App
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(App));
            this.richtxtChat = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtBox = new System.Windows.Forms.TextBox();
            this.tmrCheckData = new System.Windows.Forms.Timer(this.components);
            this.btnAddChat = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.opnFile = new System.Windows.Forms.OpenFileDialog();
            this.pnlAtta = new System.Windows.Forms.Panel();
            this.lblAttachments = new System.Windows.Forms.Label();
            this.btnAttachment = new System.Windows.Forms.Button();
            this.pnlAtta.SuspendLayout();
            this.SuspendLayout();
            // 
            // richtxtChat
            // 
            this.richtxtChat.BackColor = System.Drawing.SystemColors.Window;
            this.richtxtChat.Enabled = false;
            this.richtxtChat.Font = new System.Drawing.Font("David", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richtxtChat.Location = new System.Drawing.Point(303, 94);
            this.richtxtChat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.richtxtChat.Name = "richtxtChat";
            this.richtxtChat.ReadOnly = true;
            this.richtxtChat.Size = new System.Drawing.Size(769, 575);
            this.richtxtChat.TabIndex = 0;
            this.richtxtChat.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(944, 683);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(130, 32);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtBox
            // 
            this.txtBox.Enabled = false;
            this.txtBox.Location = new System.Drawing.Point(303, 685);
            this.txtBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBox.Name = "txtBox";
            this.txtBox.Size = new System.Drawing.Size(630, 26);
            this.txtBox.TabIndex = 3;
            // 
            // tmrCheckData
            // 
            this.tmrCheckData.Enabled = true;
            this.tmrCheckData.Interval = 2500;
            this.tmrCheckData.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnAddChat
            // 
            this.btnAddChat.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddChat.Location = new System.Drawing.Point(-3, 92);
            this.btnAddChat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddChat.Name = "btnAddChat";
            this.btnAddChat.Size = new System.Drawing.Size(309, 66);
            this.btnAddChat.TabIndex = 0;
            this.btnAddChat.Text = "Add Chat";
            this.btnAddChat.UseVisualStyleBackColor = true;
            this.btnAddChat.Click += new System.EventHandler(this.btnAddChat_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Miriam Fixed", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(297, 37);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(357, 32);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "A very smart quote";
            this.lblStatus.Visible = false;
            // 
            // opnFile
            // 
            this.opnFile.FileName = "Attachment";
            // 
            // pnlAtta
            // 
            this.pnlAtta.Controls.Add(this.lblAttachments);
            this.pnlAtta.Location = new System.Drawing.Point(1083, 92);
            this.pnlAtta.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlAtta.Name = "pnlAtta";
            this.pnlAtta.Size = new System.Drawing.Size(206, 578);
            this.pnlAtta.TabIndex = 0;
            // 
            // lblAttachments
            // 
            this.lblAttachments.AutoSize = true;
            this.lblAttachments.Location = new System.Drawing.Point(4, 12);
            this.lblAttachments.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAttachments.Name = "lblAttachments";
            this.lblAttachments.Size = new System.Drawing.Size(100, 20);
            this.lblAttachments.TabIndex = 6;
            this.lblAttachments.Text = "Attachments";
            this.lblAttachments.Visible = false;
            // 
            // btnAttachment
            // 
            this.btnAttachment.Enabled = false;
            this.btnAttachment.Location = new System.Drawing.Point(1083, 683);
            this.btnAttachment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAttachment.Name = "btnAttachment";
            this.btnAttachment.Size = new System.Drawing.Size(206, 32);
            this.btnAttachment.TabIndex = 8;
            this.btnAttachment.Text = "Add Attachment";
            this.btnAttachment.UseVisualStyleBackColor = true;
            this.btnAttachment.Click += new System.EventHandler(this.btnAttachment_Click);
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1306, 751);
            this.Controls.Add(this.pnlAtta);
            this.Controls.Add(this.btnAttachment);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnAddChat);
            this.Controls.Add(this.txtBox);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.richtxtChat);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "App";
            this.Text = "NoteMail";
            this.Load += new System.EventHandler(this.App_Load);
            this.pnlAtta.ResumeLayout(false);
            this.pnlAtta.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtBox;
        private System.Windows.Forms.RichTextBox richtxtChat;
        private System.Windows.Forms.Timer tmrCheckData;
        private System.Windows.Forms.Button btnAddChat;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.OpenFileDialog opnFile;
        private System.Windows.Forms.Panel pnlAtta;
        private System.Windows.Forms.Button btnAttachment;
        private System.Windows.Forms.Label lblAttachments;
    }
}