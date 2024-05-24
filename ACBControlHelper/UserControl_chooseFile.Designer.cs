namespace ACBControlHelper
{
    partial class UserControl_chooseFile
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Button_BrowseFolder = new Button();
            CheckedListBox_Files = new CheckedListBox();
            TextBox_Path = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // Button_BrowseFolder
            // 
            Button_BrowseFolder.Location = new Point(19, 21);
            Button_BrowseFolder.Name = "Button_BrowseFolder";
            Button_BrowseFolder.Size = new Size(98, 36);
            Button_BrowseFolder.TabIndex = 0;
            Button_BrowseFolder.Text = "...";
            Button_BrowseFolder.UseVisualStyleBackColor = true;
            Button_BrowseFolder.Click += Button_BrowseFolder_Click;
            // 
            // CheckedListBox_Files
            // 
            CheckedListBox_Files.FormattingEnabled = true;
            CheckedListBox_Files.Location = new Point(139, 86);
            CheckedListBox_Files.Name = "CheckedListBox_Files";
            CheckedListBox_Files.Size = new Size(290, 310);
            CheckedListBox_Files.TabIndex = 10;
            // 
            // TextBox_Path
            // 
            TextBox_Path.Location = new Point(139, 29);
            TextBox_Path.Name = "TextBox_Path";
            TextBox_Path.Size = new Size(290, 23);
            TextBox_Path.TabIndex = 2;
            TextBox_Path.TextChanged += TextBox_Path_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(139, 11);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 3;
            label1.Text = "Folder Path";
            // 
            // UserControl_chooseFile
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(TextBox_Path);
            Controls.Add(CheckedListBox_Files);
            Controls.Add(Button_BrowseFolder);
            Name = "UserControl_chooseFile";
            Size = new Size(483, 458);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Button_BrowseFolder;
        private CheckedListBox CheckedListBox_Files;
        private TextBox TextBox_Path;
        private Label label1;
    }
}
