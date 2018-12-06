namespace GUI
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dg_ID = new System.Windows.Forms.DataGridView();
            this.btn_Search = new System.Windows.Forms.Button();
            this.dg_CA = new System.Windows.Forms.DataGridView();
            this.cb_SearchBy = new System.Windows.Forms.ComboBox();
            this.btn_Create = new System.Windows.Forms.Button();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_ID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_CA)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.generateToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.generateToolStripMenuItem.Text = "Generate";
            this.generateToolStripMenuItem.Click += new System.EventHandler(this.generateToolStripMenuItem_Click);
            // 
            // dg_ID
            // 
            this.dg_ID.AllowUserToAddRows = false;
            this.dg_ID.AllowUserToDeleteRows = false;
            this.dg_ID.AllowUserToOrderColumns = true;
            this.dg_ID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_ID.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dg_ID.Location = new System.Drawing.Point(12, 56);
            this.dg_ID.MultiSelect = false;
            this.dg_ID.Name = "dg_ID";
            this.dg_ID.ReadOnly = true;
            this.dg_ID.RowHeadersVisible = false;
            this.dg_ID.RowTemplate.ReadOnly = true;
            this.dg_ID.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_ID.ShowEditingIcon = false;
            this.dg_ID.Size = new System.Drawing.Size(375, 382);
            this.dg_ID.TabIndex = 1;
            this.dg_ID.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_ID_CellContentClick);
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(215, 27);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(75, 23);
            this.btn_Search.TabIndex = 3;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            // 
            // dg_CA
            // 
            this.dg_CA.AllowUserToAddRows = false;
            this.dg_CA.AllowUserToDeleteRows = false;
            this.dg_CA.AllowUserToOrderColumns = true;
            this.dg_CA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_CA.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dg_CA.Location = new System.Drawing.Point(406, 56);
            this.dg_CA.MultiSelect = false;
            this.dg_CA.Name = "dg_CA";
            this.dg_CA.ReadOnly = true;
            this.dg_CA.RowHeadersVisible = false;
            this.dg_CA.RowTemplate.ReadOnly = true;
            this.dg_CA.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_CA.ShowEditingIcon = false;
            this.dg_CA.Size = new System.Drawing.Size(382, 382);
            this.dg_CA.TabIndex = 4;
            this.dg_CA.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CA_CellContentClick);
            // 
            // cb_SearchBy
            // 
            this.cb_SearchBy.FormattingEnabled = true;
            this.cb_SearchBy.Items.AddRange(new object[] {
            "Search by ID",
            "Search by Cadastral Area and ID "});
            this.cb_SearchBy.Location = new System.Drawing.Point(12, 27);
            this.cb_SearchBy.Name = "cb_SearchBy";
            this.cb_SearchBy.Size = new System.Drawing.Size(197, 21);
            this.cb_SearchBy.TabIndex = 5;
            // 
            // btn_Create
            // 
            this.btn_Create.Location = new System.Drawing.Point(713, 27);
            this.btn_Create.Name = "btn_Create";
            this.btn_Create.Size = new System.Drawing.Size(75, 23);
            this.btn_Create.TabIndex = 6;
            this.btn_Create.Text = "New";
            this.btn_Create.UseVisualStyleBackColor = true;
            this.btn_Create.Click += new System.EventHandler(this.btn_Create_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_Create);
            this.Controls.Add(this.cb_SearchBy);
            this.Controls.Add(this.dg_CA);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.dg_ID);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_ID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_CA)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
        private System.Windows.Forms.DataGridView dg_ID;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.DataGridView dg_CA;
        private System.Windows.Forms.ComboBox cb_SearchBy;
        private System.Windows.Forms.Button btn_Create;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
    }
}

