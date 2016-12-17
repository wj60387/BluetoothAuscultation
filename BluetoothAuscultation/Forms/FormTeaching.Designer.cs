using BluetoothAuscultation.ExtendControl;
namespace BluetoothAuscultation.Forms
{
    partial class FormTeaching
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboStethoscope = new BluetoothAuscultation.ExtendControl.ComboBoxEx();
            this.btnTeach = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewEx1 = new BluetoothAuscultation.ExtendControl.DataGridViewEx();
            this.Checked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.StetChineseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StetOwner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StetStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cboStethoscope);
            this.groupBox1.Controls.Add(this.btnTeach);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(6, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(754, 75);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择听诊器";
            // 
            // cboStethoscope
            // 
            this.cboStethoscope.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboStethoscope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStethoscope.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboStethoscope.FormattingEnabled = true;
            this.cboStethoscope.ItemHeight = 20;
            this.cboStethoscope.Location = new System.Drawing.Point(25, 29);
            this.cboStethoscope.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboStethoscope.Name = "cboStethoscope";
            this.cboStethoscope.Size = new System.Drawing.Size(308, 26);
            this.cboStethoscope.TabIndex = 1;
            this.cboStethoscope.DropDown += new System.EventHandler(this.cboStethoscope_DropDown);
            this.cboStethoscope.SelectedIndexChanged += new System.EventHandler(this.cboStethoscope_SelectedIndexChanged);
            // 
            // btnTeach
            // 
            this.btnTeach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(185)))), ((int)(((byte)(213)))));
            this.btnTeach.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTeach.FlatAppearance.BorderSize = 0;
            this.btnTeach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTeach.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTeach.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTeach.Location = new System.Drawing.Point(371, 28);
            this.btnTeach.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTeach.Name = "btnTeach";
            this.btnTeach.Size = new System.Drawing.Size(86, 30);
            this.btnTeach.TabIndex = 10;
            this.btnTeach.Tag = "start";
            this.btnTeach.Text = "开始教学";
            this.btnTeach.UseVisualStyleBackColor = false;
            this.btnTeach.Click += new System.EventHandler(this.btnTeach_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.dataGridViewEx1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(6, 88);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 358);
            this.panel1.TabIndex = 26;
            // 
            // dataGridViewEx1
            // 
            this.dataGridViewEx1.AllowUserToAddRows = false;
            this.dataGridViewEx1.AllowUserToDeleteRows = false;
            this.dataGridViewEx1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewEx1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewEx1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewEx1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewEx1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEx1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Checked,
            this.StetChineseName,
            this.StetName,
            this.StetOwner,
            this.StetStatus});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewEx1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewEx1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.dataGridViewEx1.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridViewEx1.Location = new System.Drawing.Point(0, 52);
            this.dataGridViewEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridViewEx1.Name = "dataGridViewEx1";
            this.dataGridViewEx1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewEx1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewEx1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewEx1.RowTemplate.Height = 32;
            this.dataGridViewEx1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEx1.Size = new System.Drawing.Size(760, 306);
            this.dataGridViewEx1.TabIndex = 5;
            this.dataGridViewEx1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEx1_CellContentClick);
            // 
            // Checked
            // 
            this.Checked.HeaderText = "选择";
            this.Checked.Name = "Checked";
            this.Checked.ReadOnly = true;
            this.Checked.Width = 38;
            // 
            // StetChineseName
            // 
            this.StetChineseName.HeaderText = "听诊器名称";
            this.StetChineseName.Name = "StetChineseName";
            this.StetChineseName.ReadOnly = true;
            this.StetChineseName.Width = 93;
            // 
            // StetName
            // 
            this.StetName.HeaderText = "听诊器序号";
            this.StetName.Name = "StetName";
            this.StetName.ReadOnly = true;
            this.StetName.Width = 93;
            // 
            // StetOwner
            // 
            this.StetOwner.HeaderText = "听诊器所属人";
            this.StetOwner.Name = "StetOwner";
            this.StetOwner.ReadOnly = true;
            this.StetOwner.Width = 105;
            // 
            // StetStatus
            // 
            this.StetStatus.HeaderText = "连接状态";
            this.StetStatus.Name = "StetStatus";
            this.StetStatus.ReadOnly = true;
            this.StetStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.StetStatus.Width = 81;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(760, 52);
            this.panel2.TabIndex = 4;
            this.panel2.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(62, 18);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(51, 21);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "全选";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // FormTeaching
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(786, 458);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormTeaching";
            this.Text = "听诊教学";
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEx1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBoxEx cboStethoscope;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private ExtendControl.DataGridViewEx dataGridViewEx1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnTeach;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Checked;
        private System.Windows.Forms.DataGridViewTextBoxColumn StetChineseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StetOwner;
        private System.Windows.Forms.DataGridViewTextBoxColumn StetStatus;
    }
}