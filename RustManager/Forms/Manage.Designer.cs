namespace RustManager.Forms
{
    partial class Manage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Manage));
            this.ServerList = new System.Windows.Forms.ComboBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ConnectOnStartupCheck = new System.Windows.Forms.CheckBox();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.RCONPort = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.ServerPort = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.AddressBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CreateButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RCONPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServerPort)).BeginInit();
            this.SuspendLayout();
            // 
            // ServerList
            // 
            this.ServerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ServerList.FormattingEnabled = true;
            this.ServerList.Location = new System.Drawing.Point(13, 13);
            this.ServerList.Name = "ServerList";
            this.ServerList.Size = new System.Drawing.Size(186, 21);
            this.ServerList.TabIndex = 0;
            this.ServerList.SelectedIndexChanged += new System.EventHandler(this.ServerList_SelectedIndexChanged);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(108, 41);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(91, 23);
            this.DeleteButton.TabIndex = 2;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NameBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.SaveButton);
            this.groupBox1.Controls.Add(this.ConnectOnStartupCheck);
            this.groupBox1.Controls.Add(this.PasswordBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.RCONPort);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ServerPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.AddressBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 278);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(7, 37);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(174, 20);
            this.NameBox.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Name";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(7, 245);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(174, 23);
            this.SaveButton.TabIndex = 9;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ConnectOnStartupCheck
            // 
            this.ConnectOnStartupCheck.AutoSize = true;
            this.ConnectOnStartupCheck.Location = new System.Drawing.Point(7, 221);
            this.ConnectOnStartupCheck.Name = "ConnectOnStartupCheck";
            this.ConnectOnStartupCheck.Size = new System.Drawing.Size(116, 17);
            this.ConnectOnStartupCheck.TabIndex = 8;
            this.ConnectOnStartupCheck.Text = "Connect on startup";
            this.ConnectOnStartupCheck.UseVisualStyleBackColor = true;
            // 
            // PasswordBox
            // 
            this.PasswordBox.Location = new System.Drawing.Point(7, 194);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.PasswordChar = '*';
            this.PasswordBox.Size = new System.Drawing.Size(174, 20);
            this.PasswordBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Password";
            // 
            // RCONPort
            // 
            this.RCONPort.Location = new System.Drawing.Point(7, 155);
            this.RCONPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.RCONPort.Name = "RCONPort";
            this.RCONPort.Size = new System.Drawing.Size(174, 20);
            this.RCONPort.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "RCON Port";
            // 
            // ServerPort
            // 
            this.ServerPort.Location = new System.Drawing.Point(7, 116);
            this.ServerPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.ServerPort.Name = "ServerPort";
            this.ServerPort.Size = new System.Drawing.Size(174, 20);
            this.ServerPort.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // IPBox
            // 
            this.AddressBox.Location = new System.Drawing.Point(7, 77);
            this.AddressBox.Name = "IPBox";
            this.AddressBox.Size = new System.Drawing.Size(174, 20);
            this.AddressBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address";
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(13, 41);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(89, 23);
            this.CreateButton.TabIndex = 4;
            this.CreateButton.Text = "Create";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // Manage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(212, 356);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.ServerList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Manage";
            this.Text = "Servers";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Manage_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RCONPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServerPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ServerList;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.CheckBox ConnectOnStartupCheck;
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown RCONPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown ServerPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox AddressBox;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button CreateButton;
    }
}