namespace ACBControlHelper
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ButtonCertificate = new Button();
            SelectCsvs_Main = new ChooseFile();
            SelectCsvs_Verfication = new ChooseFile();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // ButtonCertificate
            // 
            ButtonCertificate.Location = new Point(442, 556);
            ButtonCertificate.Name = "ButtonCertificate";
            ButtonCertificate.Size = new Size(128, 40);
            ButtonCertificate.TabIndex = 0;
            ButtonCertificate.Text = "Generate Certificate";
            ButtonCertificate.UseVisualStyleBackColor = true;
            ButtonCertificate.Click += ButtonCertificate_Click;
            // 
            // SelectCsvs_Main
            // 
            SelectCsvs_Main.Location = new Point(-67, 31);
            SelectCsvs_Main.Name = "SelectCsvs_Main";
            SelectCsvs_Main.Size = new Size(503, 519);
            SelectCsvs_Main.TabIndex = 1;
            // 
            // SelectCsvs_Verfication
            // 
            SelectCsvs_Verfication.Location = new Point(442, 31);
            SelectCsvs_Verfication.Name = "SelectCsvs_Verfication";
            SelectCsvs_Verfication.Size = new Size(503, 519);
            SelectCsvs_Verfication.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(192, 31);
            label1.Name = "label1";
            label1.Size = new Size(34, 15);
            label1.TabIndex = 3;
            label1.Text = "Main";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(700, 31);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 4;
            label2.Text = "Verification";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(942, 608);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(SelectCsvs_Verfication);
            Controls.Add(SelectCsvs_Main);
            Controls.Add(ButtonCertificate);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ButtonCertificate;
        private ChooseFile SelectCsvs_Main;
        private ChooseFile SelectCsvs_Verfication;
        private Label label1;
        private Label label2;
    }
}