namespace dreamscape
{
    partial class Game
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
            this.m_Graphics = new System.Windows.Forms.PictureBox();
            this.m_LogHold = new System.Windows.Forms.GroupBox();
            this.m_Log = new System.Windows.Forms.WebBrowser();
            this.m_Tabs = new System.Windows.Forms.TabControl();
            this.m_Default = new System.Windows.Forms.TabPage();
            this.m_TextInput = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.bw_Net = new System.ComponentModel.BackgroundWorker();
            this.m_toolstrip = new System.Windows.Forms.StatusStrip();
            this.m_toolstrip_ping = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.m_Graphics)).BeginInit();
            this.m_LogHold.SuspendLayout();
            this.m_Tabs.SuspendLayout();
            this.m_toolstrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_Graphics
            // 
            this.m_Graphics.Location = new System.Drawing.Point(12, 12);
            this.m_Graphics.Name = "m_Graphics";
            this.m_Graphics.Size = new System.Drawing.Size(433, 426);
            this.m_Graphics.TabIndex = 0;
            this.m_Graphics.TabStop = false;
            // 
            // m_LogHold
            // 
            this.m_LogHold.Controls.Add(this.m_Log);
            this.m_LogHold.Location = new System.Drawing.Point(451, 235);
            this.m_LogHold.Name = "m_LogHold";
            this.m_LogHold.Size = new System.Drawing.Size(380, 203);
            this.m_LogHold.TabIndex = 2;
            this.m_LogHold.TabStop = false;
            this.m_LogHold.Text = "Log";
            // 
            // m_Log
            // 
            this.m_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_Log.Location = new System.Drawing.Point(3, 16);
            this.m_Log.MinimumSize = new System.Drawing.Size(20, 20);
            this.m_Log.Name = "m_Log";
            this.m_Log.Size = new System.Drawing.Size(374, 184);
            this.m_Log.TabIndex = 0;
            // 
            // m_Tabs
            // 
            this.m_Tabs.Controls.Add(this.m_Default);
            this.m_Tabs.Location = new System.Drawing.Point(451, 12);
            this.m_Tabs.Name = "m_Tabs";
            this.m_Tabs.SelectedIndex = 0;
            this.m_Tabs.Size = new System.Drawing.Size(380, 217);
            this.m_Tabs.TabIndex = 3;
            // 
            // m_Default
            // 
            this.m_Default.Location = new System.Drawing.Point(4, 22);
            this.m_Default.Name = "m_Default";
            this.m_Default.Padding = new System.Windows.Forms.Padding(3);
            this.m_Default.Size = new System.Drawing.Size(372, 191);
            this.m_Default.TabIndex = 0;
            this.m_Default.Text = "Default";
            this.m_Default.UseVisualStyleBackColor = true;
            // 
            // m_TextInput
            // 
            this.m_TextInput.Location = new System.Drawing.Point(12, 444);
            this.m_TextInput.Name = "m_TextInput";
            this.m_TextInput.Size = new System.Drawing.Size(735, 20);
            this.m_TextInput.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(753, 442);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Act";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // bw_Net
            // 
            this.bw_Net.WorkerReportsProgress = true;
            this.bw_Net.WorkerSupportsCancellation = true;
            this.bw_Net.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            // 
            // m_toolstrip
            // 
            this.m_toolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_toolstrip_ping});
            this.m_toolstrip.Location = new System.Drawing.Point(0, 470);
            this.m_toolstrip.Name = "m_toolstrip";
            this.m_toolstrip.Size = new System.Drawing.Size(843, 22);
            this.m_toolstrip.TabIndex = 6;
            this.m_toolstrip.Text = "statusStrip1";
            // 
            // m_toolstrip_ping
            // 
            this.m_toolstrip_ping.Name = "m_toolstrip_ping";
            this.m_toolstrip_ping.Size = new System.Drawing.Size(59, 17);
            this.m_toolstrip_ping.Text = "Ping: 0ms";
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 492);
            this.Controls.Add(this.m_toolstrip);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.m_TextInput);
            this.Controls.Add(this.m_Tabs);
            this.Controls.Add(this.m_LogHold);
            this.Controls.Add(this.m_Graphics);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Game";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Game_FormClosing);
            this.Load += new System.EventHandler(this.Game_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_Graphics)).EndInit();
            this.m_LogHold.ResumeLayout(false);
            this.m_Tabs.ResumeLayout(false);
            this.m_toolstrip.ResumeLayout(false);
            this.m_toolstrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox m_Graphics;
        private System.Windows.Forms.GroupBox m_LogHold;
        private System.Windows.Forms.TabControl m_Tabs;
        private System.Windows.Forms.TabPage m_Default;
        private System.Windows.Forms.TextBox m_TextInput;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.BackgroundWorker bw_Net;
        private System.Windows.Forms.WebBrowser m_Log;
        private System.Windows.Forms.StatusStrip m_toolstrip;
        private System.Windows.Forms.ToolStripStatusLabel m_toolstrip_ping;
    }
}