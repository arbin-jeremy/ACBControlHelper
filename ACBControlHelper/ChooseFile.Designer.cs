namespace ACBControlHelper
{
    partial class ChooseFile
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
            label1 = new Label();
            TextBox_Path = new TextBox();
            CheckedListBox_Files = new CheckedListBox();
            SuspendLayout();
            // 
            // Button_BrowseFolder
            // 
            Button_BrowseFolder.Location = new Point(296, 55);
            Button_BrowseFolder.Name = "Button_BrowseFolder";
            Button_BrowseFolder.Size = new Size(48, 20);
            Button_BrowseFolder.TabIndex = 9;
            Button_BrowseFolder.Text = "...";
            Button_BrowseFolder.UseVisualStyleBackColor = true;
            Button_BrowseFolder.Click += Button_BrowseFolder_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(119, 63);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 8;
            label1.Text = "Folder Path";
            // 
            // TextBox_Path
            // 
            TextBox_Path.Location = new Point(85, 81);
            TextBox_Path.Name = "TextBox_Path";
            TextBox_Path.Size = new Size(381, 23);
            TextBox_Path.TabIndex = 7;
            TextBox_Path.TextChanged += textBox1_TextChanged;
            // 
            // CheckedListBox_Files
            // 
            CheckedListBox_Files.FormattingEnabled = true;
            CheckedListBox_Files.Location = new Point(85, 122);
            CheckedListBox_Files.Name = "CheckedListBox_Files";
            CheckedListBox_Files.Size = new Size(397, 364);
            CheckedListBox_Files.TabIndex = 10;
            // 
            // ChooseFile
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(CheckedListBox_Files);
            Controls.Add(Button_BrowseFolder);
            Controls.Add(label1);
            Controls.Add(TextBox_Path);
            Name = "ChooseFile";
            Size = new Size(654, 537);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Button_BrowseFolder;
        private Label label1;
        private TextBox TextBox_Path;
        private CheckedListBox CheckedListBox_Files;
    }
}
