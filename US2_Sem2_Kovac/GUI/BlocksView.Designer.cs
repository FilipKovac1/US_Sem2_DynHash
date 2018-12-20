namespace GUI
{
    partial class BlocksView
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
            this.dg_CA = new System.Windows.Forms.DataGridView();
            this.dg_ID = new System.Windows.Forms.DataGridView();
            this.dg_Rec = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dg_CA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_ID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Rec)).BeginInit();
            this.SuspendLayout();
            // 
            // dg_CA
            // 
            this.dg_CA.AllowUserToAddRows = false;
            this.dg_CA.AllowUserToDeleteRows = false;
            this.dg_CA.AllowUserToOrderColumns = true;
            this.dg_CA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_CA.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dg_CA.Location = new System.Drawing.Point(403, 12);
            this.dg_CA.MultiSelect = false;
            this.dg_CA.Name = "dg_CA";
            this.dg_CA.ReadOnly = true;
            this.dg_CA.RowHeadersVisible = false;
            this.dg_CA.RowTemplate.ReadOnly = true;
            this.dg_CA.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_CA.ShowEditingIcon = false;
            this.dg_CA.Size = new System.Drawing.Size(382, 382);
            this.dg_CA.TabIndex = 10;
            this.dg_CA.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CA_CellContentClick);
            // 
            // dg_ID
            // 
            this.dg_ID.AllowUserToAddRows = false;
            this.dg_ID.AllowUserToDeleteRows = false;
            this.dg_ID.AllowUserToOrderColumns = true;
            this.dg_ID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_ID.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dg_ID.Location = new System.Drawing.Point(12, 12);
            this.dg_ID.MultiSelect = false;
            this.dg_ID.Name = "dg_ID";
            this.dg_ID.ReadOnly = true;
            this.dg_ID.RowHeadersVisible = false;
            this.dg_ID.RowTemplate.ReadOnly = true;
            this.dg_ID.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_ID.ShowEditingIcon = false;
            this.dg_ID.Size = new System.Drawing.Size(375, 382);
            this.dg_ID.TabIndex = 9;
            this.dg_ID.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_ID_CellContentClick);
            // 
            // dg_Rec
            // 
            this.dg_Rec.AllowUserToAddRows = false;
            this.dg_Rec.AllowUserToDeleteRows = false;
            this.dg_Rec.AllowUserToOrderColumns = true;
            this.dg_Rec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Rec.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dg_Rec.Location = new System.Drawing.Point(800, 11);
            this.dg_Rec.MultiSelect = false;
            this.dg_Rec.Name = "dg_Rec";
            this.dg_Rec.ReadOnly = true;
            this.dg_Rec.RowHeadersVisible = false;
            this.dg_Rec.RowTemplate.ReadOnly = true;
            this.dg_Rec.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_Rec.ShowEditingIcon = false;
            this.dg_Rec.Size = new System.Drawing.Size(382, 382);
            this.dg_Rec.TabIndex = 11;
            // 
            // BlocksView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 405);
            this.Controls.Add(this.dg_Rec);
            this.Controls.Add(this.dg_CA);
            this.Controls.Add(this.dg_ID);
            this.Name = "BlocksView";
            this.Text = "BlocksView";
            ((System.ComponentModel.ISupportInitialize)(this.dg_CA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_ID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Rec)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dg_CA;
        private System.Windows.Forms.DataGridView dg_ID;
        private System.Windows.Forms.DataGridView dg_Rec;
    }
}