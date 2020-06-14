namespace NoteMail_LironMeler
{
    partial class AddChat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddChat));
            this.comboxChats = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.lblPar = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboxChats
            // 
            this.comboxChats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboxChats.FormattingEnabled = true;
            this.comboxChats.Location = new System.Drawing.Point(18, 66);
            this.comboxChats.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboxChats.Name = "comboxChats";
            this.comboxChats.Size = new System.Drawing.Size(126, 28);
            this.comboxChats.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(228, 62);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(128, 35);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCreate.Location = new System.Drawing.Point(18, 138);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(338, 35);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Create Chat";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // lblPar
            // 
            this.lblPar.AutoSize = true;
            this.lblPar.Location = new System.Drawing.Point(14, 14);
            this.lblPar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPar.Name = "lblPar";
            this.lblPar.Size = new System.Drawing.Size(0, 20);
            this.lblPar.TabIndex = 3;
            // 
            // AddChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 192);
            this.Controls.Add(this.lblPar);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.comboxChats);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AddChat";
            this.Text = "Add Chat";
            this.Load += new System.EventHandler(this.AddChat_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.ComboBox comboxChats;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label lblPar;
    }
}