namespace GUI
{
    partial class Test
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
            this.trieDepth = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.blockSize = new System.Windows.Forms.TextBox();
            this.caCount = new System.Windows.Forms.TextBox();
            this.propCount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dirPath = new System.Windows.Forms.TextBox();
            this.btn_Test = new System.Windows.Forms.Button();
            this.btn_Load = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // trieDepth
            // 
            this.trieDepth.Location = new System.Drawing.Point(197, 15);
            this.trieDepth.Name = "trieDepth";
            this.trieDepth.Size = new System.Drawing.Size(100, 20);
            this.trieDepth.TabIndex = 0;
            this.trieDepth.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hlbka stromu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Velkost blocku";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Pocet katastralnych uzemi";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Pocet nehnutelnosti v KU";
            // 
            // blockSize
            // 
            this.blockSize.Location = new System.Drawing.Point(197, 46);
            this.blockSize.Name = "blockSize";
            this.blockSize.Size = new System.Drawing.Size(100, 20);
            this.blockSize.TabIndex = 5;
            this.blockSize.Text = "10";
            // 
            // caCount
            // 
            this.caCount.Location = new System.Drawing.Point(197, 74);
            this.caCount.Name = "caCount";
            this.caCount.Size = new System.Drawing.Size(100, 20);
            this.caCount.TabIndex = 6;
            this.caCount.Text = "100";
            // 
            // propCount
            // 
            this.propCount.Location = new System.Drawing.Point(197, 102);
            this.propCount.Name = "propCount";
            this.propCount.Size = new System.Drawing.Size(100, 20);
            this.propCount.TabIndex = 7;
            this.propCount.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Cesta k suborom";
            // 
            // dirPath
            // 
            this.dirPath.Location = new System.Drawing.Point(197, 131);
            this.dirPath.Name = "dirPath";
            this.dirPath.Size = new System.Drawing.Size(100, 20);
            this.dirPath.TabIndex = 9;
            this.dirPath.Text = "C:\\Users\\Admin\\Documents\\S\\US2\\Semestralka2\\subory";
            // 
            // btn_Test
            // 
            this.btn_Test.Location = new System.Drawing.Point(221, 172);
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.Size = new System.Drawing.Size(75, 23);
            this.btn_Test.TabIndex = 10;
            this.btn_Test.Text = "Test";
            this.btn_Test.UseVisualStyleBackColor = true;
            this.btn_Test.Click += new System.EventHandler(this.btn_Test_Click);
            // 
            // btn_Load
            // 
            this.btn_Load.Location = new System.Drawing.Point(126, 172);
            this.btn_Load.Name = "btn_Load";
            this.btn_Load.Size = new System.Drawing.Size(75, 23);
            this.btn_Load.TabIndex = 11;
            this.btn_Load.Text = "Load";
            this.btn_Load.UseVisualStyleBackColor = true;
            this.btn_Load.Click += new System.EventHandler(this.btn_Load_Click);
            // 
            // Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 207);
            this.Controls.Add(this.btn_Load);
            this.Controls.Add(this.btn_Test);
            this.Controls.Add(this.dirPath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.propCount);
            this.Controls.Add(this.caCount);
            this.Controls.Add(this.blockSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trieDepth);
            this.Name = "Test";
            this.Text = "Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox trieDepth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox blockSize;
        private System.Windows.Forms.TextBox caCount;
        private System.Windows.Forms.TextBox propCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox dirPath;
        private System.Windows.Forms.Button btn_Test;
        private System.Windows.Forms.Button btn_Load;
    }
}