using BluetoothAuscultation.ExtendControl;
namespace BluetoothAuscultation.Forms
{
    partial class FormCloud
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboStethoscope = new BluetoothAuscultation.ExtendControl.ComboBoxEx();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tpUpload = new System.Windows.Forms.TabPage();
            this.dgv_tpUpload = new BluetoothAuscultation.ExtendControl.DataGridViewEx();
            this.picUploadReload = new System.Windows.Forms.PictureBox();
            this.tpServer = new System.Windows.Forms.TabPage();
            this.dgv_ShareMe = new BluetoothAuscultation.ExtendControl.DataGridViewEx();
            this.picReload = new System.Windows.Forms.PictureBox();
            this.GUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvuploadPatientType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_upLoad_patientID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_upLoad_patientName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_upLoad_Flag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_upload_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_upload_download = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgv_upload_share = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgv_upload_detail = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgv_upload_delete = new System.Windows.Forms.DataGridViewImageColumn();
            this.dvgShareGuid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dvgSharePatientType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dvgSharePatientID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dvgSharePatientName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dvgShareFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dvgShareSharer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dvgShareShareTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvShareDown = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgvShareDetail = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgvShareDelete = new System.Windows.Forms.DataGridViewImageColumn();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tpUpload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tpUpload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUploadReload)).BeginInit();
            this.tpServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ShareMe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReload)).BeginInit();
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
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cboStethoscope);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(6, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(933, 79);
            this.groupBox1.TabIndex = 27;
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
            this.cboStethoscope.Location = new System.Drawing.Point(33, 32);
            this.cboStethoscope.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboStethoscope.Name = "cboStethoscope";
            this.cboStethoscope.Size = new System.Drawing.Size(308, 26);
            this.cboStethoscope.TabIndex = 1;
            this.cboStethoscope.SelectedIndexChanged += new System.EventHandler(this.cboStethoscope_SelectedIndexChanged);
            // 
            // tabMain
            // 
            this.tabMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabMain.Controls.Add(this.tpUpload);
            this.tabMain.Controls.Add(this.tpServer);
            this.tabMain.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabMain.Location = new System.Drawing.Point(6, 86);
            this.tabMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(937, 433);
            this.tabMain.TabIndex = 26;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // tpUpload
            // 
            this.tpUpload.Controls.Add(this.dgv_tpUpload);
            this.tpUpload.Controls.Add(this.picUploadReload);
            this.tpUpload.Location = new System.Drawing.Point(4, 29);
            this.tpUpload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpUpload.Name = "tpUpload";
            this.tpUpload.Size = new System.Drawing.Size(929, 400);
            this.tpUpload.TabIndex = 3;
            this.tpUpload.Text = "我上传的听诊";
            this.tpUpload.UseVisualStyleBackColor = true;
            // 
            // dgv_tpUpload
            // 
            this.dgv_tpUpload.AllowUserToAddRows = false;
            this.dgv_tpUpload.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgv_tpUpload.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_tpUpload.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_tpUpload.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgv_tpUpload.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_tpUpload.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_tpUpload.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_tpUpload.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_tpUpload.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GUID,
            this.dgvuploadPatientType,
            this.dgv_upLoad_patientID,
            this.dgv_upLoad_patientName,
            this.dgv_upLoad_Flag,
            this.dgv_upload_time,
            this.dgv_upload_download,
            this.dgv_upload_share,
            this.dgv_upload_detail,
            this.dgv_upload_delete});
            this.dgv_tpUpload.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_tpUpload.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_tpUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_tpUpload.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.dgv_tpUpload.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgv_tpUpload.Location = new System.Drawing.Point(0, 0);
            this.dgv_tpUpload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgv_tpUpload.Name = "dgv_tpUpload";
            this.dgv_tpUpload.ReadOnly = true;
            this.dgv_tpUpload.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_tpUpload.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgv_tpUpload.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_tpUpload.RowTemplate.Height = 32;
            this.dgv_tpUpload.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_tpUpload.Size = new System.Drawing.Size(929, 400);
            this.dgv_tpUpload.TabIndex = 26;
            this.dgv_tpUpload.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_tpUpload_CellContentClick);
            // 
            // picUploadReload
            // 
            this.picUploadReload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picUploadReload.Location = new System.Drawing.Point(1081, 1);
            this.picUploadReload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picUploadReload.Name = "picUploadReload";
            this.picUploadReload.Size = new System.Drawing.Size(53, 41);
            this.picUploadReload.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picUploadReload.TabIndex = 24;
            this.picUploadReload.TabStop = false;
            // 
            // tpServer
            // 
            this.tpServer.Controls.Add(this.dgv_ShareMe);
            this.tpServer.Controls.Add(this.picReload);
            this.tpServer.Location = new System.Drawing.Point(4, 29);
            this.tpServer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tpServer.Name = "tpServer";
            this.tpServer.Size = new System.Drawing.Size(929, 400);
            this.tpServer.TabIndex = 2;
            this.tpServer.Text = " 分享给我的听诊 ";
            this.tpServer.UseVisualStyleBackColor = true;
            // 
            // dgv_ShareMe
            // 
            this.dgv_ShareMe.AllowUserToAddRows = false;
            this.dgv_ShareMe.AllowUserToDeleteRows = false;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgv_ShareMe.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_ShareMe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_ShareMe.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgv_ShareMe.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_ShareMe.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ShareMe.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_ShareMe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ShareMe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dvgShareGuid,
            this.dvgSharePatientType,
            this.dvgSharePatientID,
            this.dvgSharePatientName,
            this.dvgShareFlag,
            this.dvgShareSharer,
            this.dvgShareShareTime,
            this.dgvShareDown,
            this.dgvShareDetail,
            this.dgvShareDelete});
            this.dgv_ShareMe.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ShareMe.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgv_ShareMe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ShareMe.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.dgv_ShareMe.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgv_ShareMe.Location = new System.Drawing.Point(0, 0);
            this.dgv_ShareMe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgv_ShareMe.Name = "dgv_ShareMe";
            this.dgv_ShareMe.ReadOnly = true;
            this.dgv_ShareMe.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ShareMe.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgv_ShareMe.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgv_ShareMe.RowTemplate.Height = 32;
            this.dgv_ShareMe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ShareMe.Size = new System.Drawing.Size(929, 400);
            this.dgv_ShareMe.TabIndex = 27;
            this.dgv_ShareMe.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ShareMe_CellContentClick);
            // 
            // picReload
            // 
            this.picReload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picReload.Location = new System.Drawing.Point(1081, 1);
            this.picReload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picReload.Name = "picReload";
            this.picReload.Size = new System.Drawing.Size(53, 41);
            this.picReload.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picReload.TabIndex = 1;
            this.picReload.TabStop = false;
            // 
            // GUID
            // 
            this.GUID.HeaderText = "GUID";
            this.GUID.Name = "GUID";
            this.GUID.ReadOnly = true;
            this.GUID.Visible = false;
            this.GUID.Width = 64;
            // 
            // dgvuploadPatientType
            // 
            this.dgvuploadPatientType.HeaderText = "患者类型";
            this.dgvuploadPatientType.Name = "dgvuploadPatientType";
            this.dgvuploadPatientType.ReadOnly = true;
            this.dgvuploadPatientType.Width = 81;
            // 
            // dgv_upLoad_patientID
            // 
            this.dgv_upLoad_patientID.HeaderText = "患者ID";
            this.dgv_upLoad_patientID.Name = "dgv_upLoad_patientID";
            this.dgv_upLoad_patientID.ReadOnly = true;
            this.dgv_upLoad_patientID.Visible = false;
            this.dgv_upLoad_patientID.Width = 70;
            // 
            // dgv_upLoad_patientName
            // 
            this.dgv_upLoad_patientName.HeaderText = "患者姓名";
            this.dgv_upLoad_patientName.Name = "dgv_upLoad_patientName";
            this.dgv_upLoad_patientName.ReadOnly = true;
            this.dgv_upLoad_patientName.Width = 81;
            // 
            // dgv_upLoad_Flag
            // 
            this.dgv_upLoad_Flag.HeaderText = "标识";
            this.dgv_upLoad_Flag.Name = "dgv_upLoad_Flag";
            this.dgv_upLoad_Flag.ReadOnly = true;
            this.dgv_upLoad_Flag.Visible = false;
            this.dgv_upLoad_Flag.Width = 57;
            // 
            // dgv_upload_time
            // 
            this.dgv_upload_time.HeaderText = "上传时间";
            this.dgv_upload_time.Name = "dgv_upload_time";
            this.dgv_upload_time.ReadOnly = true;
            this.dgv_upload_time.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_upload_time.Width = 81;
            // 
            // dgv_upload_download
            // 
            this.dgv_upload_download.HeaderText = "下载";
            this.dgv_upload_download.Name = "dgv_upload_download";
            this.dgv_upload_download.ReadOnly = true;
            this.dgv_upload_download.Width = 38;
            // 
            // dgv_upload_share
            // 
            this.dgv_upload_share.HeaderText = "分享";
            this.dgv_upload_share.Name = "dgv_upload_share";
            this.dgv_upload_share.ReadOnly = true;
            this.dgv_upload_share.Width = 38;
            // 
            // dgv_upload_detail
            // 
            this.dgv_upload_detail.HeaderText = "详情";
            this.dgv_upload_detail.Name = "dgv_upload_detail";
            this.dgv_upload_detail.ReadOnly = true;
            this.dgv_upload_detail.Width = 38;
            // 
            // dgv_upload_delete
            // 
            this.dgv_upload_delete.HeaderText = "删除";
            this.dgv_upload_delete.Name = "dgv_upload_delete";
            this.dgv_upload_delete.ReadOnly = true;
            this.dgv_upload_delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_upload_delete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgv_upload_delete.Width = 57;
            // 
            // dvgShareGuid
            // 
            this.dvgShareGuid.HeaderText = "tzlyGUID";
            this.dvgShareGuid.Name = "dvgShareGuid";
            this.dvgShareGuid.ReadOnly = true;
            this.dvgShareGuid.Visible = false;
            this.dvgShareGuid.Width = 92;
            // 
            // dvgSharePatientType
            // 
            this.dvgSharePatientType.HeaderText = "患者类型";
            this.dvgSharePatientType.Name = "dvgSharePatientType";
            this.dvgSharePatientType.ReadOnly = true;
            this.dvgSharePatientType.Width = 90;
            // 
            // dvgSharePatientID
            // 
            this.dvgSharePatientID.HeaderText = "患者ID";
            this.dvgSharePatientID.Name = "dvgSharePatientID";
            this.dvgSharePatientID.ReadOnly = true;
            this.dvgSharePatientID.Visible = false;
            this.dvgSharePatientID.Width = 77;
            // 
            // dvgSharePatientName
            // 
            this.dvgSharePatientName.HeaderText = "患者姓名";
            this.dvgSharePatientName.Name = "dvgSharePatientName";
            this.dvgSharePatientName.ReadOnly = true;
            this.dvgSharePatientName.Width = 90;
            // 
            // dvgShareFlag
            // 
            this.dvgShareFlag.HeaderText = "标识";
            this.dvgShareFlag.Name = "dvgShareFlag";
            this.dvgShareFlag.ReadOnly = true;
            this.dvgShareFlag.Visible = false;
            this.dvgShareFlag.Width = 62;
            // 
            // dvgShareSharer
            // 
            this.dvgShareSharer.HeaderText = "分享者";
            this.dvgShareSharer.Name = "dvgShareSharer";
            this.dvgShareSharer.ReadOnly = true;
            this.dvgShareSharer.Width = 76;
            // 
            // dvgShareShareTime
            // 
            this.dvgShareShareTime.HeaderText = "分享时间";
            this.dvgShareShareTime.Name = "dvgShareShareTime";
            this.dvgShareShareTime.ReadOnly = true;
            this.dvgShareShareTime.Width = 90;
            // 
            // dgvShareDown
            // 
            this.dgvShareDown.HeaderText = "下载";
            this.dgvShareDown.Name = "dgvShareDown";
            this.dgvShareDown.ReadOnly = true;
            this.dgvShareDown.Width = 43;
            // 
            // dgvShareDetail
            // 
            this.dgvShareDetail.HeaderText = "详情";
            this.dgvShareDetail.Name = "dgvShareDetail";
            this.dgvShareDetail.ReadOnly = true;
            this.dgvShareDetail.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShareDetail.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvShareDetail.Width = 62;
            // 
            // dgvShareDelete
            // 
            this.dgvShareDelete.HeaderText = "删除";
            this.dgvShareDelete.Name = "dgvShareDelete";
            this.dgvShareDelete.ReadOnly = true;
            this.dgvShareDelete.Width = 43;
            // 
            // FormCloud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(951, 521);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabMain);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormCloud";
            this.ShowIcon = false;
            this.Text = "云端听诊";
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tpUpload.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tpUpload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUploadReload)).EndInit();
            this.tpServer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ShareMe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReload)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private  ComboBoxEx cboStethoscope;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tpUpload;
        private System.Windows.Forms.PictureBox picUploadReload;
        private System.Windows.Forms.TabPage tpServer;
        private System.Windows.Forms.PictureBox picReload;
        private ExtendControl.DataGridViewEx dgv_tpUpload;
        private System.Windows.Forms.GroupBox groupBox1;
        private ExtendControl.DataGridViewEx dgv_ShareMe;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn GUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvuploadPatientType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_upLoad_patientID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_upLoad_patientName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_upLoad_Flag;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_upload_time;
        private System.Windows.Forms.DataGridViewImageColumn dgv_upload_download;
        private System.Windows.Forms.DataGridViewImageColumn dgv_upload_share;
        private System.Windows.Forms.DataGridViewImageColumn dgv_upload_detail;
        private System.Windows.Forms.DataGridViewImageColumn dgv_upload_delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn dvgShareGuid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dvgSharePatientType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dvgSharePatientID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dvgSharePatientName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dvgShareFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn dvgShareSharer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dvgShareShareTime;
        private System.Windows.Forms.DataGridViewImageColumn dgvShareDown;
        private System.Windows.Forms.DataGridViewImageColumn dgvShareDetail;
        private System.Windows.Forms.DataGridViewImageColumn dgvShareDelete;
    }
}