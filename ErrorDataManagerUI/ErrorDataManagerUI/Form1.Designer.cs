namespace ErrorDataManagerUI
{
    partial class Form1
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
            this.Search_btn = new System.Windows.Forms.Button();
            this.Add_btn = new System.Windows.Forms.Button();
            this.Update_btn = new System.Windows.Forms.Button();
            this.Delete_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DeviceClass_cbx = new System.Windows.Forms.ComboBox();
            this.ErrorCode_tbx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Description_tbx = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Category_cbx = new System.Windows.Forms.ComboBox();
            this.ErrorDataGridView = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdatedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tag_tbx = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Reset_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // Search_btn
            // 
            this.Search_btn.Location = new System.Drawing.Point(651, 166);
            this.Search_btn.Name = "Search_btn";
            this.Search_btn.Size = new System.Drawing.Size(75, 31);
            this.Search_btn.TabIndex = 0;
            this.Search_btn.Text = "Search";
            this.Search_btn.UseVisualStyleBackColor = true;
            this.Search_btn.Click += new System.EventHandler(this.Search_btn_Click);
            // 
            // Add_btn
            // 
            this.Add_btn.Location = new System.Drawing.Point(651, 203);
            this.Add_btn.Name = "Add_btn";
            this.Add_btn.Size = new System.Drawing.Size(75, 31);
            this.Add_btn.TabIndex = 3;
            this.Add_btn.Text = "Add";
            this.Add_btn.UseVisualStyleBackColor = true;
            this.Add_btn.Click += new System.EventHandler(this.Add_btn_Click);
            // 
            // Update_btn
            // 
            this.Update_btn.Location = new System.Drawing.Point(651, 240);
            this.Update_btn.Name = "Update_btn";
            this.Update_btn.Size = new System.Drawing.Size(75, 31);
            this.Update_btn.TabIndex = 4;
            this.Update_btn.Text = "Update";
            this.Update_btn.UseVisualStyleBackColor = true;
            this.Update_btn.Click += new System.EventHandler(this.Update_btn_Click);
            // 
            // Delete_btn
            // 
            this.Delete_btn.Location = new System.Drawing.Point(651, 277);
            this.Delete_btn.Name = "Delete_btn";
            this.Delete_btn.Size = new System.Drawing.Size(75, 31);
            this.Delete_btn.TabIndex = 5;
            this.Delete_btn.Text = "Delete";
            this.Delete_btn.UseVisualStyleBackColor = true;
            this.Delete_btn.Click += new System.EventHandler(this.Delete_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "ATM Error Code Searcher";
            // 
            // DeviceClass_cbx
            // 
            this.DeviceClass_cbx.FormattingEnabled = true;
            this.DeviceClass_cbx.Items.AddRange(new object[] {
            "",
            "PTR",
            "IDC",
            "CDM",
            "PIN",
            "CHK",
            "DEP",
            "TTU",
            "SIU",
            "VDM",
            "CAM",
            "ALM",
            "CEU",
            "CIM",
            "CRD",
            "BCR",
            "IPM",
            "BIO"});
            this.DeviceClass_cbx.Location = new System.Drawing.Point(123, 80);
            this.DeviceClass_cbx.Name = "DeviceClass_cbx";
            this.DeviceClass_cbx.Size = new System.Drawing.Size(322, 24);
            this.DeviceClass_cbx.TabIndex = 7;
            // 
            // ErrorCode_tbx
            // 
            this.ErrorCode_tbx.Location = new System.Drawing.Point(123, 123);
            this.ErrorCode_tbx.Name = "ErrorCode_tbx";
            this.ErrorCode_tbx.Size = new System.Drawing.Size(322, 22);
            this.ErrorCode_tbx.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Error Code";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Description";
            // 
            // Description_tbx
            // 
            this.Description_tbx.Location = new System.Drawing.Point(123, 166);
            this.Description_tbx.Multiline = true;
            this.Description_tbx.Name = "Description_tbx";
            this.Description_tbx.Size = new System.Drawing.Size(481, 142);
            this.Description_tbx.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Device Class";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Category";
            // 
            // Category_cbx
            // 
            this.Category_cbx.FormattingEnabled = true;
            this.Category_cbx.Items.AddRange(new object[] {
            "",
            "Device",
            "XFSGeneral",
            "Simax"});
            this.Category_cbx.Location = new System.Drawing.Point(123, 40);
            this.Category_cbx.Name = "Category_cbx";
            this.Category_cbx.Size = new System.Drawing.Size(322, 24);
            this.Category_cbx.TabIndex = 12;
            // 
            // ErrorDataGridView
            // 
            this.ErrorDataGridView.AllowUserToAddRows = false;
            this.ErrorDataGridView.AllowUserToDeleteRows = false;
            this.ErrorDataGridView.AllowUserToOrderColumns = true;
            this.ErrorDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ErrorDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Code,
            this.Description,
            this.Category,
            this.DeviceClassName,
            this.Tag,
            this.CreatedBy,
            this.CreateDate,
            this.UpdatedBy,
            this.UpdateDate});
            this.ErrorDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ErrorDataGridView.Location = new System.Drawing.Point(9, 334);
            this.ErrorDataGridView.Name = "ErrorDataGridView";
            this.ErrorDataGridView.RowHeadersWidth = 51;
            this.ErrorDataGridView.RowTemplate.Height = 24;
            this.ErrorDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ErrorDataGridView.Size = new System.Drawing.Size(1500, 407);
            this.ErrorDataGridView.TabIndex = 13;
            this.ErrorDataGridView.SelectionChanged += new System.EventHandler(this.ErrorDataGridView_SelectionChanged);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.Visible = false;
            this.Id.Width = 125;
            // 
            // Code
            // 
            this.Code.HeaderText = "Code";
            this.Code.MinimumWidth = 6;
            this.Code.Name = "Code";
            this.Code.Width = 100;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.MinimumWidth = 6;
            this.Description.Name = "Description";
            this.Description.Width = 125;
            // 
            // Category
            // 
            this.Category.HeaderText = "Category";
            this.Category.MinimumWidth = 6;
            this.Category.Name = "Category";
            this.Category.Width = 100;
            // 
            // DeviceClassName
            // 
            this.DeviceClassName.HeaderText = "DeviceClass";
            this.DeviceClassName.MinimumWidth = 6;
            this.DeviceClassName.Name = "DeviceClassName";
            this.DeviceClassName.Width = 125;
            // 
            // Tag
            // 
            this.Tag.HeaderText = "Tag";
            this.Tag.MinimumWidth = 6;
            this.Tag.Name = "Tag";
            this.Tag.Width = 100;
            // 
            // CreatedBy
            // 
            this.CreatedBy.HeaderText = "CreatedBy";
            this.CreatedBy.MinimumWidth = 6;
            this.CreatedBy.Name = "CreatedBy";
            this.CreatedBy.Width = 125;
            // 
            // CreateDate
            // 
            this.CreateDate.HeaderText = "CreateDate";
            this.CreateDate.MinimumWidth = 6;
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.Width = 125;
            // 
            // UpdatedBy
            // 
            this.UpdatedBy.HeaderText = "UpdatedBy";
            this.UpdatedBy.MinimumWidth = 6;
            this.UpdatedBy.Name = "UpdatedBy";
            this.UpdatedBy.Width = 125;
            // 
            // UpdateDate
            // 
            this.UpdateDate.HeaderText = "UpdateDate";
            this.UpdateDate.MinimumWidth = 6;
            this.UpdateDate.Name = "UpdateDate";
            this.UpdateDate.Width = 125;
            // 
            // Tag_tbx
            // 
            this.Tag_tbx.Location = new System.Drawing.Point(569, 42);
            this.Tag_tbx.Name = "Tag_tbx";
            this.Tag_tbx.Size = new System.Drawing.Size(216, 22);
            this.Tag_tbx.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(521, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "Tag";
            // 
            // Reset_btn
            // 
            this.Reset_btn.Location = new System.Drawing.Point(857, 277);
            this.Reset_btn.Name = "Reset_btn";
            this.Reset_btn.Size = new System.Drawing.Size(91, 31);
            this.Reset_btn.TabIndex = 16;
            this.Reset_btn.Text = "Reset";
            this.Reset_btn.UseVisualStyleBackColor = true;
            this.Reset_btn.Click += new System.EventHandler(this.Reset_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 753);
            this.Controls.Add(this.Reset_btn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Tag_tbx);
            this.Controls.Add(this.ErrorDataGridView);
            this.Controls.Add(this.Category_cbx);
            this.Controls.Add(this.Description_tbx);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ErrorCode_tbx);
            this.Controls.Add(this.DeviceClass_cbx);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Delete_btn);
            this.Controls.Add(this.Update_btn);
            this.Controls.Add(this.Add_btn);
            this.Controls.Add(this.Search_btn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Search_btn;
        private System.Windows.Forms.Button Add_btn;
        private System.Windows.Forms.Button Update_btn;
        private System.Windows.Forms.Button Delete_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DeviceClass_cbx;
        private System.Windows.Forms.TextBox ErrorCode_tbx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Description_tbx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox Category_cbx;
        private System.Windows.Forms.DataGridView ErrorDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceClassName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tag;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdatedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdateDate;
        private System.Windows.Forms.TextBox Tag_tbx;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Reset_btn;
    }
}

