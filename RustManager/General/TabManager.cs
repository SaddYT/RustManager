using System.Windows.Forms;

namespace RustManager.General
{
    class TabManager
    {
        private TabPage defaultPage;
        public Label sayLabel = new Label();
        public Label commandLabel = new Label();
        public TextBox outputBox = new TextBox();
        public TextBox sayBox = new TextBox();
        public TextBox commandBox = new TextBox();

        static private TabManager _instance;
        public static TabManager Instance
        {
            get
            {
                return new TabManager();
            }
        }

        public TabPage DefaultPage
        {
            get
            {
                return defaultPage ?? GenerateDefaultPage();
            }
        }

        TabPage GenerateDefaultPage()
        {
            defaultPage = new TabPage();
            defaultPage.Controls.Add(sayLabel);
            defaultPage.Controls.Add(commandLabel);
            defaultPage.Controls.Add(outputBox);
            defaultPage.Controls.Add(sayBox);
            defaultPage.Controls.Add(commandBox);
            defaultPage.Location = new System.Drawing.Point(4, 22);
            defaultPage.Name = "tabPage1";
            defaultPage.Padding = new Padding(3);
            defaultPage.Size = new System.Drawing.Size(508, 369);
            defaultPage.TabIndex = 0;
            defaultPage.Text = "No Connection";
            defaultPage.UseVisualStyleBackColor = true;

            sayLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            sayLabel.AutoSize = true;
            sayLabel.Location = new System.Drawing.Point(7, 278);
            sayLabel.Name = "SayLabel";
            sayLabel.Size = new System.Drawing.Size(34, 13);
            sayLabel.TabIndex = 4;
            sayLabel.Text = "Say...";
            
            commandLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            commandLabel.AutoSize = true;
            commandLabel.Location = new System.Drawing.Point(10, 323);
            commandLabel.Name = "CommandLabel";
            commandLabel.Size = new System.Drawing.Size(63, 13);
            commandLabel.TabIndex = 5;
            commandLabel.Text = "Command...";
            
            outputBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            outputBox.Location = new System.Drawing.Point(7, 7);
            outputBox.Multiline = true;
            outputBox.Name = "OutputBox";
            outputBox.ReadOnly = true;
            outputBox.ScrollBars = ScrollBars.Vertical;
            outputBox.Size = new System.Drawing.Size(495, 264);
            outputBox.TabIndex = 1;
            
            sayBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            sayBox.Location = new System.Drawing.Point(7, 296);
            sayBox.Name = "SayBox";
            sayBox.Size = new System.Drawing.Size(495, 20);
            sayBox.TabIndex = 2;

            commandBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            commandBox.Location = new System.Drawing.Point(7, 339);
            commandBox.Name = "CommandBox";
            commandBox.Size = new System.Drawing.Size(495, 20);
            commandBox.TabIndex = 3;

            return defaultPage;
        }
    }
}