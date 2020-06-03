namespace dreamscape
{
    partial class Hub
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
            this.m_userHoldTab = new System.Windows.Forms.TabControl();
            this.m_userTab = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.m_nickname = new System.Windows.Forms.TextBox();
            this.m_gameTab = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_Key = new System.Windows.Forms.TextBox();
            this.m_ipAddr = new System.Windows.Forms.TextBox();
            this.m_Connect = new System.Windows.Forms.Button();
            this.m_Games = new System.Windows.Forms.ListBox();
            this.m_userHoldTab.SuspendLayout();
            this.m_userTab.SuspendLayout();
            this.m_gameTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_userHoldTab
            // 
            this.m_userHoldTab.Controls.Add(this.m_userTab);
            this.m_userHoldTab.Controls.Add(this.m_gameTab);
            this.m_userHoldTab.Location = new System.Drawing.Point(12, 12);
            this.m_userHoldTab.Name = "m_userHoldTab";
            this.m_userHoldTab.SelectedIndex = 0;
            this.m_userHoldTab.Size = new System.Drawing.Size(265, 424);
            this.m_userHoldTab.TabIndex = 3;
            // 
            // m_userTab
            // 
            this.m_userTab.Controls.Add(this.label1);
            this.m_userTab.Controls.Add(this.m_nickname);
            this.m_userTab.Location = new System.Drawing.Point(4, 22);
            this.m_userTab.Name = "m_userTab";
            this.m_userTab.Padding = new System.Windows.Forms.Padding(3);
            this.m_userTab.Size = new System.Drawing.Size(257, 398);
            this.m_userTab.TabIndex = 0;
            this.m_userTab.Text = "User";
            this.m_userTab.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nickname";
            // 
            // m_nickname
            // 
            this.m_nickname.Location = new System.Drawing.Point(6, 6);
            this.m_nickname.Name = "m_nickname";
            this.m_nickname.Size = new System.Drawing.Size(241, 20);
            this.m_nickname.TabIndex = 3;
            // 
            // m_gameTab
            // 
            this.m_gameTab.Controls.Add(this.label3);
            this.m_gameTab.Controls.Add(this.label2);
            this.m_gameTab.Controls.Add(this.m_Key);
            this.m_gameTab.Controls.Add(this.m_ipAddr);
            this.m_gameTab.Controls.Add(this.m_Connect);
            this.m_gameTab.Location = new System.Drawing.Point(4, 22);
            this.m_gameTab.Name = "m_gameTab";
            this.m_gameTab.Padding = new System.Windows.Forms.Padding(3);
            this.m_gameTab.Size = new System.Drawing.Size(257, 398);
            this.m_gameTab.TabIndex = 1;
            this.m_gameTab.Text = "Game";
            this.m_gameTab.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(222, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Key";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "IP";
            // 
            // m_Key
            // 
            this.m_Key.Location = new System.Drawing.Point(6, 61);
            this.m_Key.Name = "m_Key";
            this.m_Key.Size = new System.Drawing.Size(210, 20);
            this.m_Key.TabIndex = 4;
            // 
            // m_ipAddr
            // 
            this.m_ipAddr.Location = new System.Drawing.Point(6, 35);
            this.m_ipAddr.Name = "m_ipAddr";
            this.m_ipAddr.Size = new System.Drawing.Size(218, 20);
            this.m_ipAddr.TabIndex = 3;
            // 
            // m_Connect
            // 
            this.m_Connect.Location = new System.Drawing.Point(6, 6);
            this.m_Connect.Name = "m_Connect";
            this.m_Connect.Size = new System.Drawing.Size(55, 23);
            this.m_Connect.TabIndex = 1;
            this.m_Connect.Text = "Connect";
            this.m_Connect.UseVisualStyleBackColor = true;
            this.m_Connect.Click += new System.EventHandler(this.M_Connect_Click);
            // 
            // m_Games
            // 
            this.m_Games.FormattingEnabled = true;
            this.m_Games.Location = new System.Drawing.Point(283, 12);
            this.m_Games.Name = "m_Games";
            this.m_Games.Size = new System.Drawing.Size(524, 420);
            this.m_Games.TabIndex = 4;
            this.m_Games.SelectedValueChanged += new System.EventHandler(this.M_Games_SelectedValueChanged);
            // 
            // Hub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 448);
            this.Controls.Add(this.m_Games);
            this.Controls.Add(this.m_userHoldTab);
            this.Name = "Hub";
            this.Text = "Dreamscape";
            this.Load += new System.EventHandler(this.Hub_Load);
            this.m_userHoldTab.ResumeLayout(false);
            this.m_userTab.ResumeLayout(false);
            this.m_userTab.PerformLayout();
            this.m_gameTab.ResumeLayout(false);
            this.m_gameTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl m_userHoldTab;
        private System.Windows.Forms.TabPage m_userTab;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_nickname;
        private System.Windows.Forms.TabPage m_gameTab;
        private System.Windows.Forms.Button m_Connect;
        private System.Windows.Forms.TextBox m_ipAddr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_Key;
        private System.Windows.Forms.ListBox m_Games;
    }
}

