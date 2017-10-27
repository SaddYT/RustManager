namespace RustManager.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ServerList = new System.Windows.Forms.ComboBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.ManageButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ConnectAllButton = new System.Windows.Forms.Button();
            this.TabPanel = new System.Windows.Forms.TabControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ServerList
            // 
            this.ServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ServerList.FormattingEnabled = true;
            this.ServerList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ServerList.Location = new System.Drawing.Point(3, 2);
            this.ServerList.Name = "ServerList";
            this.ServerList.Size = new System.Drawing.Size(267, 21);
            this.ServerList.TabIndex = 0;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ConnectButton.Location = new System.Drawing.Point(438, 0);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 3;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // ManageButton
            // 
            this.ManageButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ManageButton.Location = new System.Drawing.Point(276, 0);
            this.ManageButton.Name = "ManageButton";
            this.ManageButton.Size = new System.Drawing.Size(75, 23);
            this.ManageButton.TabIndex = 1;
            this.ManageButton.Text = "Manage";
            this.ManageButton.UseVisualStyleBackColor = true;
            this.ManageButton.Click += new System.EventHandler(this.ManageButton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.ConnectAllButton);
            this.panel1.Controls.Add(this.ServerList);
            this.panel1.Controls.Add(this.ManageButton);
            this.panel1.Controls.Add(this.ConnectButton);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(516, 27);
            this.panel1.TabIndex = 3;
            // 
            // ConnectAllButton
            // 
            this.ConnectAllButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ConnectAllButton.Location = new System.Drawing.Point(357, 0);
            this.ConnectAllButton.Name = "ConnectAllButton";
            this.ConnectAllButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectAllButton.TabIndex = 2;
            this.ConnectAllButton.Text = "Connect All";
            this.ConnectAllButton.UseVisualStyleBackColor = true;
            this.ConnectAllButton.Click += new System.EventHandler(this.ConnectAllButton_Click);
            // 
            // TabPanel
            // 
            this.TabPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabPanel.Location = new System.Drawing.Point(12, 45);
            this.TabPanel.Name = "TabPanel";
            this.TabPanel.SelectedIndex = 0;
            this.TabPanel.Size = new System.Drawing.Size(516, 395);
            this.TabPanel.TabIndex = 4;
            this.TabPanel.SelectedIndexChanged += new System.EventHandler(this.TabPanel_SelectedIndexChanged);
            this.TabPanel.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.TabPanel_ControlAdded);
            this.TabPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TabPanel_MouseClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 452);
            this.Controls.Add(this.TabPanel);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "MainForm";
            this.Text = "RustManager";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ServerList;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button ManageButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ConnectAllButton;
        private System.Windows.Forms.TabControl TabPanel;
    }
}

