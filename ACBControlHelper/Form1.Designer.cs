﻿namespace ACBControlHelper
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
            label3 = new Label();
            RichTextBox_Customer = new RichTextBox();
            TextBox_SN = new TextBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // ButtonCertificate
            // 
            ButtonCertificate.Location = new Point(750, 555);
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
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 546);
            label3.Name = "label3";
            label3.Size = new Size(59, 15);
            label3.TabIndex = 5;
            label3.Text = "Customer";
            // 
            // RichTextBox_Customer
            // 
            RichTextBox_Customer.Location = new Point(77, 530);
            RichTextBox_Customer.Name = "RichTextBox_Customer";
            RichTextBox_Customer.Size = new Size(171, 66);
            RichTextBox_Customer.TabIndex = 6;
            RichTextBox_Customer.Text = "";
            // 
            // TextBox_SN
            // 
            TextBox_SN.Location = new Point(463, 555);
            TextBox_SN.Name = "TextBox_SN";
            TextBox_SN.Size = new Size(100, 23);
            TextBox_SN.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(432, 558);
            label4.Name = "label4";
            label4.Size = new Size(25, 15);
            label4.TabIndex = 8;
            label4.Text = "SN:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(942, 608);
            Controls.Add(label4);
            Controls.Add(TextBox_SN);
            Controls.Add(RichTextBox_Customer);
            Controls.Add(label3);
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
        private Label label3;
        private RichTextBox RichTextBox_Customer;
        private TextBox TextBox_SN;
        private Label label4;
    }
}