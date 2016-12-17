using BluetoothAuscultation.ExtendControl;
namespace BluetoothAuscultation.Forms
{
    partial class FormEducation
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabLocal = new System.Windows.Forms.TabControl();
            this.tpLocal = new System.Windows.Forms.TabPage();
            this.dgvLocal = new BluetoothAuscultation.ExtendControl.DataGridViewEx();
            this.tzlyPatientGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tzlyPatientType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tzlyPatientID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tzlyPatientName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tzlyDocName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tzlyDocDiagnose = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tzlyRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tzlyFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tzlyCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDetail = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnDelete = new System.Windows.Forms.DataGridViewImageColumn();
            this.tpDown = new System.Windows.Forms.TabPage();
            this.dgv_DownLoad = new BluetoothAuscultation.ExtendControl.DataGridViewEx();
            this.dgvDownGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDownPatientID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDownUserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDownUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDownSharer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDownShareTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDownTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDownDetail = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnDownDelete = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboStethoscope = new BluetoothAuscultation.ExtendControl.ComboBoxEx();
            this.btnAuscultate = new BluetoothAuscultation.ExtendControl.ButtonEx();
            this.contextMenuStrip1.SuspendLayout();
            this.tabLocal.SuspendLayout();
            this.tpLocal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocal)).BeginInit();
            this.tpDown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DownLoad)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.刷新ToolStripMenuItem.Text = "刷新";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.刷新ToolStripMenuItem_Click);
            // 
            // tabLocal
            // 
            this.tabLocal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabLocal.Controls.Add(this.tpLocal);
            this.tabLocal.Controls.Add(this.tpDown);
            this.tabLocal.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabLocal.Location = new System.Drawing.Point(6, 91);
            this.tabLocal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabLocal.Name = "tabLocal";
            this.tabLocal.SelectedIndex = 0;
            this.tabLocal.Size = new System.Drawing.Size(950, 501);
            this.tabLocal.TabIndex = 26;
            this.tabLocal.SelectedIndexChanged += new System.EventHandler(this.tabLocal_SelectedIndexChanged);
            // 
            // tpLocal
            // 
            this.tpLocal.Controls.Add(this.dgvLocal);
            this.tpLocal.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tpLocal.Location = new System.Drawing.Point(4, 29);
            this.tpLocal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpLocal.Name = "tpLocal";
            this.tpLocal.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpLocal.Size = new System.Drawing.Size(942, 468);
            this.tpLocal.TabIndex = 0;
            this.tpLocal.Text = "本地录音";
            this.tpLocal.UseVisualStyleBackColor = true;
            // 
            // dgvLocal
            // 
            this.dgvLocal.AllowUserToAddRows = false;
            this.dgvLocal.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvLocal.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLocal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLocal.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvLocal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLocal.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLocal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLocal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocal.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tzlyPatientGUID,
            this.tzlyPatientType,
            this.tzlyPatientID,
            this.tzlyPatientName,
            this.tzlyDocName,
            this.tzlyDocDiagnose,
            this.tzlyRemark,
            this.tzlyFlag,
            this.tzlyCreateTime,
            this.btnDetail,
            this.btnDelete});
            this.dgvLocal.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLocal.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLocal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLocal.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.dgvLocal.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvLocal.Location = new System.Drawing.Point(3, 4);
            this.dgvLocal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvLocal.MultiSelect = false;
            this.dgvLocal.Name = "dgvLocal";
            this.dgvLocal.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLocal.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvLocal.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvLocal.RowTemplate.Height = 32;
            this.dgvLocal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLocal.Size = new System.Drawing.Size(936, 460);
            this.dgvLocal.TabIndex = 25;
            this.dgvLocal.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLocal_CellContentClick);
            // 
            // tzlyPatientGUID
            // 
            this.tzlyPatientGUID.HeaderText = "GUID";
            this.tzlyPatientGUID.Name = "tzlyPatientGUID";
            this.tzlyPatientGUID.Visible = false;
            this.tzlyPatientGUID.Width = 64;
            // 
            // tzlyPatientType
            // 
            this.tzlyPatientType.HeaderText = "患者类型";
            this.tzlyPatientType.Name = "tzlyPatientType";
            this.tzlyPatientType.Width = 81;
            // 
            // tzlyPatientID
            // 
            this.tzlyPatientID.HeaderText = "患者ID";
            this.tzlyPatientID.Name = "tzlyPatientID";
            this.tzlyPatientID.Visible = false;
            this.tzlyPatientID.Width = 70;
            // 
            // tzlyPatientName
            // 
            this.tzlyPatientName.HeaderText = "患者姓名";
            this.tzlyPatientName.Name = "tzlyPatientName";
            this.tzlyPatientName.Width = 81;
            // 
            // tzlyDocName
            // 
            this.tzlyDocName.HeaderText = "医生姓名";
            this.tzlyDocName.Name = "tzlyDocName";
            this.tzlyDocName.Width = 81;
            // 
            // tzlyDocDiagnose
            // 
            this.tzlyDocDiagnose.HeaderText = "初步诊断";
            this.tzlyDocDiagnose.Name = "tzlyDocDiagnose";
            this.tzlyDocDiagnose.Width = 81;
            // 
            // tzlyRemark
            // 
            this.tzlyRemark.HeaderText = "备注";
            this.tzlyRemark.Name = "tzlyRemark";
            this.tzlyRemark.Width = 57;
            // 
            // tzlyFlag
            // 
            this.tzlyFlag.HeaderText = "标识";
            this.tzlyFlag.Name = "tzlyFlag";
            this.tzlyFlag.Visible = false;
            this.tzlyFlag.Width = 57;
            // 
            // tzlyCreateTime
            // 
            this.tzlyCreateTime.HeaderText = "创建时间";
            this.tzlyCreateTime.Name = "tzlyCreateTime";
            this.tzlyCreateTime.Width = 81;
            // 
            // btnDetail
            // 
            this.btnDetail.HeaderText = "详情";
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Width = 38;
            // 
            // btnDelete
            // 
            this.btnDelete.HeaderText = "删除";
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Width = 38;
            // 
            // tpDown
            // 
            this.tpDown.Controls.Add(this.dgv_DownLoad);
            this.tpDown.Location = new System.Drawing.Point(4, 29);
            this.tpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpDown.Name = "tpDown";
            this.tpDown.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpDown.Size = new System.Drawing.Size(942, 468);
            this.tpDown.TabIndex = 1;
            this.tpDown.Text = "已下载听诊";
            this.tpDown.UseVisualStyleBackColor = true;
            // 
            // dgv_DownLoad
            // 
            this.dgv_DownLoad.AllowUserToAddRows = false;
            this.dgv_DownLoad.AllowUserToDeleteRows = false;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgv_DownLoad.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_DownLoad.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_DownLoad.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgv_DownLoad.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_DownLoad.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_DownLoad.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_DownLoad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_DownLoad.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvDownGUID,
            this.dgvDownPatientID,
            this.dgvDownUserID,
            this.dgvDownUserName,
            this.dgvDownSharer,
            this.dgvDownShareTime,
            this.dgvDownTime,
            this.btnDownDetail,
            this.btnDownDelete});
            this.dgv_DownLoad.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_DownLoad.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgv_DownLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_DownLoad.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.dgv_DownLoad.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgv_DownLoad.Location = new System.Drawing.Point(3, 4);
            this.dgv_DownLoad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgv_DownLoad.MultiSelect = false;
            this.dgv_DownLoad.Name = "dgv_DownLoad";
            this.dgv_DownLoad.ReadOnly = true;
            this.dgv_DownLoad.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_DownLoad.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgv_DownLoad.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgv_DownLoad.RowTemplate.Height = 32;
            this.dgv_DownLoad.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_DownLoad.Size = new System.Drawing.Size(936, 460);
            this.dgv_DownLoad.TabIndex = 28;
            this.dgv_DownLoad.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_DownLoad_CellContentClick);
            // 
            // dgvDownGUID
            // 
            this.dgvDownGUID.HeaderText = "tzlyGUID";
            this.dgvDownGUID.Name = "dgvDownGUID";
            this.dgvDownGUID.ReadOnly = true;
            this.dgvDownGUID.Visible = false;
            this.dgvDownGUID.Width = 83;
            // 
            // dgvDownPatientID
            // 
            this.dgvDownPatientID.HeaderText = "患者类型";
            this.dgvDownPatientID.Name = "dgvDownPatientID";
            this.dgvDownPatientID.ReadOnly = true;
            this.dgvDownPatientID.Width = 81;
            // 
            // dgvDownUserID
            // 
            this.dgvDownUserID.HeaderText = "患者ID";
            this.dgvDownUserID.Name = "dgvDownUserID";
            this.dgvDownUserID.ReadOnly = true;
            this.dgvDownUserID.Visible = false;
            this.dgvDownUserID.Width = 70;
            // 
            // dgvDownUserName
            // 
            this.dgvDownUserName.HeaderText = "患者姓名";
            this.dgvDownUserName.Name = "dgvDownUserName";
            this.dgvDownUserName.ReadOnly = true;
            this.dgvDownUserName.Width = 81;
            // 
            // dgvDownSharer
            // 
            this.dgvDownSharer.HeaderText = "分享者";
            this.dgvDownSharer.Name = "dgvDownSharer";
            this.dgvDownSharer.ReadOnly = true;
            this.dgvDownSharer.Width = 69;
            // 
            // dgvDownShareTime
            // 
            this.dgvDownShareTime.HeaderText = "分享时间";
            this.dgvDownShareTime.Name = "dgvDownShareTime";
            this.dgvDownShareTime.ReadOnly = true;
            this.dgvDownShareTime.Width = 81;
            // 
            // dgvDownTime
            // 
            this.dgvDownTime.HeaderText = "下载时间";
            this.dgvDownTime.Name = "dgvDownTime";
            this.dgvDownTime.ReadOnly = true;
            this.dgvDownTime.Width = 81;
            // 
            // btnDownDetail
            // 
            this.btnDownDetail.HeaderText = "详情";
            this.btnDownDetail.Name = "btnDownDetail";
            this.btnDownDetail.ReadOnly = true;
            this.btnDownDetail.Width = 38;
            // 
            // btnDownDelete
            // 
            this.btnDownDelete.HeaderText = "删除";
            this.btnDownDelete.Name = "btnDownDelete";
            this.btnDownDelete.ReadOnly = true;
            this.btnDownDelete.Width = 38;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cboStethoscope);
            this.groupBox1.Controls.Add(this.btnAuscultate);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(6, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(946, 79);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择听诊器";
            // 
            // cboStethoscope
            // 
            this.cboStethoscope.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboStethoscope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStethoscope.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboStethoscope.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboStethoscope.FormattingEnabled = true;
            this.cboStethoscope.ItemHeight = 20;
            this.cboStethoscope.Location = new System.Drawing.Point(24, 27);
            this.cboStethoscope.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboStethoscope.Name = "cboStethoscope";
            this.cboStethoscope.Size = new System.Drawing.Size(308, 26);
            this.cboStethoscope.TabIndex = 1;
            this.cboStethoscope.DropDown += new System.EventHandler(this.cboStethoscope_DropDown);
            this.cboStethoscope.SelectedIndexChanged += new System.EventHandler(this.cboStethoscope_SelectedIndexChanged);
            // 
            // btnAuscultate
            // 
            this.btnAuscultate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(185)))), ((int)(((byte)(213)))));
            this.btnAuscultate.FlatAppearance.BorderSize = 0;
            this.btnAuscultate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAuscultate.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAuscultate.Location = new System.Drawing.Point(376, 28);
            this.btnAuscultate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAuscultate.Name = "btnAuscultate";
            this.btnAuscultate.Size = new System.Drawing.Size(89, 29);
            this.btnAuscultate.TabIndex = 12;
            this.btnAuscultate.Text = "新增录音";
            this.btnAuscultate.UseVisualStyleBackColor = false;
            this.btnAuscultate.Click += new System.EventHandler(this.btnAuscultate_Click);
            // 
            // FormEducation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(959, 592);
            this.Controls.Add(this.tabLocal);
            this.Controls.Add(this.groupBox1);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormEducation";
            this.ShowIcon = false;
            this.Text = "听诊录音";
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabLocal.ResumeLayout(false);
            this.tpLocal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocal)).EndInit();
            this.tpDown.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DownLoad)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private  ComboBoxEx cboStethoscope;
        private ButtonEx btnAuscultate;
        private DataGridViewEx dgvLocal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabLocal;
        private System.Windows.Forms.TabPage tpLocal;
        private System.Windows.Forms.TabPage tpDown;
        private DataGridViewEx dgv_DownLoad;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDownGUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDownPatientID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDownUserID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDownUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDownSharer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDownShareTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDownTime;
        private System.Windows.Forms.DataGridViewImageColumn btnDownDetail;
        private System.Windows.Forms.DataGridViewImageColumn btnDownDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn tzlyPatientGUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn tzlyPatientType;
        private System.Windows.Forms.DataGridViewTextBoxColumn tzlyPatientID;
        private System.Windows.Forms.DataGridViewTextBoxColumn tzlyPatientName;
        private System.Windows.Forms.DataGridViewTextBoxColumn tzlyDocName;
        private System.Windows.Forms.DataGridViewTextBoxColumn tzlyDocDiagnose;
        private System.Windows.Forms.DataGridViewTextBoxColumn tzlyRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn tzlyFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn tzlyCreateTime;
        private System.Windows.Forms.DataGridViewImageColumn btnDetail;
        private System.Windows.Forms.DataGridViewImageColumn btnDelete;
    }
}