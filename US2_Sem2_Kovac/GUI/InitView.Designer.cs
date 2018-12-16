namespace GUI
{
    partial class InitView
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
            this.dirPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.blockSize = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trieDepth = new System.Windows.Forms.TextBox();
            this.btn_Continue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dirPath
            // 
            this.dirPath.Location = new System.Drawing.Point(116, 70);
            this.dirPath.Name = "dirPath";
            this.dirPath.Size = new System.Drawing.Size(100, 20);
            this.dirPath.TabIndex = 19;
            this.dirPath.Text = "C:\\Users\\Admin\\Documents\\S\\US2\\Semestralka2\\subory";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Cesta k suborom";
            // 
            // blockSize
            // 
            this.blockSize.Location = new System.Drawing.Point(116, 37);
            this.blockSize.Name = "blockSize";
            this.blockSize.Size = new System.Drawing.Size(100, 20);
            this.blockSize.TabIndex = 15;
            this.blockSize.Text = "10";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Velkost blocku";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Hlbka stromu";
            // 
            // trieDepth
            // 
            this.trieDepth.Location = new System.Drawing.Point(116, 6);
            this.trieDepth.Name = "trieDepth";
            this.trieDepth.Size = new System.Drawing.Size(100, 20);
            this.trieDepth.TabIndex = 10;
            this.trieDepth.Text = "10";
            // 
            // btn_Continue
            // 
            this.btn_Continue.Location = new System.Drawing.Point(141, 108);
            this.btn_Continue.Name = "btn_Continue";
            this.btn_Continue.Size = new System.Drawing.Size(75, 23);
            this.btn_Continue.TabIndex = 20;
            this.btn_Continue.Text = "Continue";
            this.btn_Continue.UseVisualStyleBackColor = true;
            this.btn_Continue.Click += new System.EventHandler(this.btn_Continue_Click);
            // 
            // InitView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 139);
            this.Controls.Add(this.btn_Continue);
            this.Controls.Add(this.dirPath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.blockSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trieDepth);
            this.Name = "InitView";
            this.Text = "InitView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox dirPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox blockSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox trieDepth;
        private System.Windows.Forms.Button btn_Continue;
    }
}