namespace Lab4
{
    partial class MainForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.fileEncryptButton = new System.Windows.Forms.Button();
            this.fileDecryptButton = new System.Windows.Forms.Button();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sendToServerButton = new System.Windows.Forms.Button();
            this.folderEncryptButton = new System.Windows.Forms.Button();
            this.folderDecryptButton = new System.Windows.Forms.Button();
            this.labelInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // fileEncryptButton
            // 
            this.fileEncryptButton.Location = new System.Drawing.Point(20, 70);
            this.fileEncryptButton.Name = "fileEncryptButton";
            this.fileEncryptButton.Size = new System.Drawing.Size(111, 33);
            this.fileEncryptButton.TabIndex = 0;
            this.fileEncryptButton.Text = "File Encrypt";
            this.fileEncryptButton.UseVisualStyleBackColor = true;
            this.fileEncryptButton.Click += new System.EventHandler(this.FileEncrypt_Button_1);
            // 
            // fileDecryptButton
            // 
            this.fileDecryptButton.Location = new System.Drawing.Point(20, 127);
            this.fileDecryptButton.Name = "fileDecryptButton";
            this.fileDecryptButton.Size = new System.Drawing.Size(111, 33);
            this.fileDecryptButton.TabIndex = 1;
            this.fileDecryptButton.Text = "File Decrypt";
            this.fileDecryptButton.UseVisualStyleBackColor = true;
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Items.AddRange(new object[] {
            "Aes",
            "DES",
            "RC2",
            "Rijndael",
            "TripleDES"});
            this.comboBox.Location = new System.Drawing.Point(20, 43);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(228, 21);
            this.comboBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Encryption Method";
            // 
            // sendToServerButton
            // 
            this.sendToServerButton.Location = new System.Drawing.Point(20, 214);
            this.sendToServerButton.Name = "sendToServerButton";
            this.sendToServerButton.Size = new System.Drawing.Size(228, 33);
            this.sendToServerButton.TabIndex = 6;
            this.sendToServerButton.Text = "Send to Server";
            this.sendToServerButton.UseVisualStyleBackColor = true;
            // 
            // folderEncryptButton
            // 
            this.folderEncryptButton.Location = new System.Drawing.Point(137, 70);
            this.folderEncryptButton.Name = "folderEncryptButton";
            this.folderEncryptButton.Size = new System.Drawing.Size(111, 33);
            this.folderEncryptButton.TabIndex = 7;
            this.folderEncryptButton.Text = "Folder Encrypt";
            this.folderEncryptButton.UseVisualStyleBackColor = true;
            this.folderEncryptButton.Click += new System.EventHandler(this.FolderEncrypt_Button_1);
            // 
            // folderDecryptButton
            // 
            this.folderDecryptButton.Location = new System.Drawing.Point(137, 127);
            this.folderDecryptButton.Name = "folderDecryptButton";
            this.folderDecryptButton.Size = new System.Drawing.Size(111, 33);
            this.folderDecryptButton.TabIndex = 8;
            this.folderDecryptButton.Text = "Folder Decrypt";
            this.folderDecryptButton.UseVisualStyleBackColor = true;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(17, 234);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(0, 13);
            this.labelInfo.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 260);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.folderDecryptButton);
            this.Controls.Add(this.folderEncryptButton);
            this.Controls.Add(this.sendToServerButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.fileDecryptButton);
            this.Controls.Add(this.fileEncryptButton);
            this.Name = "MainForm";
            this.Text = "App";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button fileEncryptButton;
        private System.Windows.Forms.Button fileDecryptButton;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button sendToServerButton;
        private System.Windows.Forms.Button folderEncryptButton;
        private System.Windows.Forms.Button folderDecryptButton;
        private System.Windows.Forms.Label labelInfo;
    }
}