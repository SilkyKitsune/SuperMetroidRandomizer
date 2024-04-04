using System.Drawing;
using System.Windows.Forms;

namespace SuperMetroidRandomizer
{
    partial class MainWindow
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
            openFileDialog = new OpenFileDialog();
            folderBrowserDialog = new FolderBrowserDialog();
            romPathTextBox = new TextBox();
            outputFolderTextBox = new TextBox();
            romPathButton = new Button();
            outputFolderButton = new Button();
            romLabel = new Label();
            outputFolderLabel = new Label();
            ipsCheckBox = new CheckBox();
            generateButton = new Button();
            seedLabel = new Label();
            seedTextBox = new TextBox();
            torizoCheckBox = new CheckBox();
            SuspendLayout();
            // 
            // romPathTextBox
            // 
            romPathTextBox.Location = new Point(12, 27);
            romPathTextBox.Name = "romPathTextBox";
            romPathTextBox.Size = new Size(269, 23);
            romPathTextBox.TabIndex = 0;
            // 
            // outputFolderTextBox
            // 
            outputFolderTextBox.Location = new Point(12, 71);
            outputFolderTextBox.Name = "outputFolderTextBox";
            outputFolderTextBox.Size = new Size(269, 23);
            outputFolderTextBox.TabIndex = 1;
            // 
            // romPathButton
            // 
            romPathButton.Location = new Point(287, 27);
            romPathButton.Name = "romPathButton";
            romPathButton.Size = new Size(25, 23);
            romPathButton.TabIndex = 2;
            romPathButton.Text = "...";
            romPathButton.UseVisualStyleBackColor = true;
            romPathButton.Click += romPathButton_Click;
            // 
            // outputFolderButton
            // 
            outputFolderButton.Location = new Point(287, 71);
            outputFolderButton.Name = "outputFolderButton";
            outputFolderButton.Size = new Size(25, 23);
            outputFolderButton.TabIndex = 3;
            outputFolderButton.Text = "...";
            outputFolderButton.UseVisualStyleBackColor = true;
            outputFolderButton.Click += outputFolderButton_Click;
            // 
            // romLabel
            // 
            romLabel.AutoSize = true;
            romLabel.Location = new Point(12, 9);
            romLabel.Name = "romLabel";
            romLabel.Size = new Size(232, 15);
            romLabel.TabIndex = 4;
            romLabel.Text = "Super Metroid Path (Optional if Export IPS)";
            // 
            // outputFolderLabel
            // 
            outputFolderLabel.AutoSize = true;
            outputFolderLabel.Location = new Point(12, 53);
            outputFolderLabel.Name = "outputFolderLabel";
            outputFolderLabel.Size = new Size(81, 15);
            outputFolderLabel.TabIndex = 5;
            outputFolderLabel.Text = "Output Folder";
            // 
            // ipsCheckBox
            // 
            ipsCheckBox.AutoSize = true;
            ipsCheckBox.Location = new Point(152, 172);
            ipsCheckBox.Name = "ipsCheckBox";
            ipsCheckBox.Size = new Size(79, 19);
            ipsCheckBox.TabIndex = 6;
            ipsCheckBox.Text = "Export IPS";
            ipsCheckBox.UseVisualStyleBackColor = true;
            ipsCheckBox.CheckedChanged += ipsCheckBox_CheckedChanged;
            // 
            // generateButton
            // 
            generateButton.Location = new Point(237, 169);
            generateButton.Name = "generateButton";
            generateButton.Size = new Size(75, 23);
            generateButton.TabIndex = 7;
            generateButton.Text = "Generate";
            generateButton.UseVisualStyleBackColor = true;
            generateButton.Click += generateButton_Click;
            // 
            // seedLabel
            // 
            seedLabel.AutoSize = true;
            seedLabel.Location = new Point(12, 97);
            seedLabel.Name = "seedLabel";
            seedLabel.Size = new Size(89, 15);
            seedLabel.TabIndex = 8;
            seedLabel.Text = "Seed (Optional)";
            // 
            // seedTextBox
            // 
            seedTextBox.Location = new Point(12, 115);
            seedTextBox.Name = "seedTextBox";
            seedTextBox.Size = new Size(300, 23);
            seedTextBox.TabIndex = 9;
            // 
            // torizoCheckBox
            // 
            torizoCheckBox.AutoSize = true;
            torizoCheckBox.Location = new Point(12, 144);
            torizoCheckBox.Name = "torizoCheckBox";
            torizoCheckBox.Size = new Size(252, 19);
            torizoCheckBox.TabIndex = 10;
            torizoCheckBox.Text = "Never place Speed Booster on Bomb Torizo";
            torizoCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(324, 201);
            Controls.Add(torizoCheckBox);
            Controls.Add(seedTextBox);
            Controls.Add(seedLabel);
            Controls.Add(generateButton);
            Controls.Add(ipsCheckBox);
            Controls.Add(outputFolderLabel);
            Controls.Add(romLabel);
            Controls.Add(outputFolderButton);
            Controls.Add(romPathButton);
            Controls.Add(outputFolderTextBox);
            Controls.Add(romPathTextBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainWindow";
            Text = "Super Metroid Major Item Randomizer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog openFileDialog;
        private FolderBrowserDialog folderBrowserDialog;
        private TextBox romPathTextBox;
        private TextBox outputFolderTextBox;
        private Button romPathButton;
        private Button outputFolderButton;
        private Label romLabel;
        private Label outputFolderLabel;
        private CheckBox ipsCheckBox;
        private Button generateButton;
        private Label seedLabel;
        private TextBox seedTextBox;
        private CheckBox torizoCheckBox;
    }
}
