namespace BluetoothAuscultation.Forms
{
    partial class FormStethoscope
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStethoscope));
            this.dgvStet = new BluetoothAuscultation.ExtendControl.DataGridViewEx();
            this.StetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PCName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StetType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StetChineseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StetOwner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FuncDescript = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StetRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StetStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEdit = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnDelete = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSetting = new BluetoothAuscultation.ExtendControl.ButtonEx();
            this.cboStethoscope = new BluetoothAuscultation.ExtendControl.ComboBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStet)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvStet
            // 
            this.dgvStet.AllowUserToAddRows = false;
            this.dgvStet.AllowUserToDeleteRows = false;
            this.dgvStet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvStet.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvStet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvStet.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvStet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StetName,
            this.MAC,
            this.PCName,
            this.StetType,
            this.StetChineseName,
            this.StetOwner,
            this.FuncDescript,
            this.StetRemark,
            this.StetStatus,
            this.btnEdit,
            this.btnDelete});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStet.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvStet.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.dgvStet.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvStet.Location = new System.Drawing.Point(6, 98);
            this.dgvStet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvStet.Name = "dgvStet";
            this.dgvStet.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStet.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvStet.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvStet.RowTemplate.Height = 32;
            this.dgvStet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStet.Size = new System.Drawing.Size(850, 379);
            this.dgvStet.TabIndex = 0;
            this.dgvStet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStet_CellContentClick);
            // 
            // StetName
            // 
            this.StetName.HeaderText = "听诊器编号";
            this.StetName.Name = "StetName";
            this.StetName.ReadOnly = true;
            this.StetName.Width = 104;
            // 
            // MAC
            // 
            this.MAC.HeaderText = "MAC";
            this.MAC.Name = "MAC";
            this.MAC.ReadOnly = true;
            this.MAC.Visible = false;
            this.MAC.Width = 61;
            // 
            // PCName
            // 
            this.PCName.HeaderText = "计算机名";
            this.PCName.Name = "PCName";
            this.PCName.ReadOnly = true;
            this.PCName.Width = 90;
            // 
            // StetType
            // 
            this.StetType.HeaderText = "听诊类型";
            this.StetType.Name = "StetType";
            this.StetType.ReadOnly = true;
            this.StetType.Width = 90;
            // 
            // StetChineseName
            // 
            this.StetChineseName.HeaderText = "听诊器名字";
            this.StetChineseName.Name = "StetChineseName";
            this.StetChineseName.ReadOnly = true;
            this.StetChineseName.Width = 104;
            // 
            // StetOwner
            // 
            this.StetOwner.HeaderText = "听诊器所属人";
            this.StetOwner.Name = "StetOwner";
            this.StetOwner.ReadOnly = true;
            this.StetOwner.Width = 118;
            // 
            // FuncDescript
            // 
            this.FuncDescript.HeaderText = "听诊器描述";
            this.FuncDescript.Name = "FuncDescript";
            this.FuncDescript.ReadOnly = true;
            this.FuncDescript.Width = 104;
            // 
            // StetRemark
            // 
            this.StetRemark.HeaderText = "听诊器备注";
            this.StetRemark.Name = "StetRemark";
            this.StetRemark.ReadOnly = true;
            this.StetRemark.Width = 104;
            // 
            // StetStatus
            // 
            this.StetStatus.HeaderText = "连接状态";
            this.StetStatus.Name = "StetStatus";
            this.StetStatus.ReadOnly = true;
            this.StetStatus.Visible = false;
            this.StetStatus.Width = 81;
            // 
            // btnEdit
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle2.NullValue")));
            this.btnEdit.DefaultCellStyle = dataGridViewCellStyle2;
            this.btnEdit.HeaderText = "编辑";
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.ReadOnly = true;
            this.btnEdit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnEdit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.btnEdit.Width = 62;
            // 
            // btnDelete
            // 
            this.btnDelete.HeaderText = "删除";
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ReadOnly = true;
            this.btnDelete.Width = 43;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnSetting);
            this.groupBox1.Controls.Add(this.cboStethoscope);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(6, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(850, 79);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择听诊器";
            // 
            // btnSetting
            // 
            this.btnSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(185)))), ((int)(((byte)(213)))));
            this.btnSetting.FlatAppearance.BorderSize = 0;
            this.btnSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetting.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetting.Location = new System.Drawing.Point(366, 33);
            this.btnSetting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(89, 29);
            this.btnSetting.TabIndex = 13;
            this.btnSetting.Text = "配置";
            this.btnSetting.UseVisualStyleBackColor = false;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // cboStethoscope
            // 
            this.cboStethoscope.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboStethoscope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStethoscope.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboStethoscope.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboStethoscope.FormattingEnabled = true;
            this.cboStethoscope.ItemHeight = 20;
            this.cboStethoscope.Location = new System.Drawing.Point(33, 32);
            this.cboStethoscope.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboStethoscope.Name = "cboStethoscope";
            this.cboStethoscope.Size = new System.Drawing.Size(308, 26);
            this.cboStethoscope.TabIndex = 1;
            this.cboStethoscope.DropDown += new System.EventHandler(this.cboStethoscope_DropDown);
            // 
            // FormStethoscope
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 484);
            this.Controls.Add(this.dgvStet);
            this.Controls.Add(this.groupBox1);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideOnClose = true;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "FormStethoscope";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "听诊器配置";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStet)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private ExtendControl.ComboBoxEx cboStethoscope;
        private ExtendControl.DataGridViewEx dgvStet;

        private ExtendControl.ButtonEx btnSetting;
        private System.Windows.Forms.DataGridViewTextBoxColumn StetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAC;
        private System.Windows.Forms.DataGridViewTextBoxColumn PCName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StetType;
        private System.Windows.Forms.DataGridViewTextBoxColumn StetChineseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StetOwner;
        private System.Windows.Forms.DataGridViewTextBoxColumn FuncDescript;
        private System.Windows.Forms.DataGridViewTextBoxColumn StetRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn StetStatus;
        private System.Windows.Forms.DataGridViewImageColumn btnEdit;
        private System.Windows.Forms.DataGridViewImageColumn btnDelete;





    }
}