using BluetoothAuscultation.ExtendControl;
namespace BluetoothAuscultation.Forms
{
    partial class FormPatientInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPatientInfo));
            this.gpAuscultationInfo = new System.Windows.Forms.GroupBox();
            this.pictureBoxEx1 = new BluetoothAuscultation.ExtendControl.PictureBoxEx();
            this.txtMemo = new BluetoothAuscultation.ExtendControl.WaterTextBox();
            this.lblMemo = new System.Windows.Forms.Label();
            this.cboPatientSites = new System.Windows.Forms.ComboBox();
            this.lblPatientSites = new System.Windows.Forms.Label();
            this.cboPatientPosture = new System.Windows.Forms.ComboBox();
            this.lblPatientPosture = new System.Windows.Forms.Label();
            this.gpPatientInfo = new System.Windows.Forms.GroupBox();
            this.lblPatientId = new System.Windows.Forms.Label();
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.txtPatientId = new System.Windows.Forms.TextBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDocName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDocDiagnosis = new System.Windows.Forms.TextBox();
            this.btnSavePatient = new BluetoothAuscultation.ExtendControl.ButtonEx();
            this.gpAuscultationInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx1)).BeginInit();
            this.gpPatientInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpAuscultationInfo
            // 
            this.gpAuscultationInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gpAuscultationInfo.Controls.Add(this.pictureBoxEx1);
            this.gpAuscultationInfo.Controls.Add(this.txtMemo);
            this.gpAuscultationInfo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpAuscultationInfo.Location = new System.Drawing.Point(12, 99);
            this.gpAuscultationInfo.Name = "gpAuscultationInfo";
            this.gpAuscultationInfo.Size = new System.Drawing.Size(497, 274);
            this.gpAuscultationInfo.TabIndex = 8;
            this.gpAuscultationInfo.TabStop = false;
            this.gpAuscultationInfo.Text = "听诊信息";
            // 
            // pictureBoxEx1
            // 
            this.pictureBoxEx1.CurImage = null;
            this.pictureBoxEx1.Location = new System.Drawing.Point(201, 25);
            this.pictureBoxEx1.Name = "pictureBoxEx1";
            this.pictureBoxEx1.PageSize = new System.Drawing.Size(20, 40);
            this.pictureBoxEx1.Size = new System.Drawing.Size(101, 125);
            this.pictureBoxEx1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxEx1.TabIndex = 7;
            this.pictureBoxEx1.TabStop = false;
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(18, 166);
            this.txtMemo.MaxLength = 512;
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMemo.Size = new System.Drawing.Size(461, 102);
            this.txtMemo.TabIndex = 5;
            this.txtMemo.WaterText = "备注";
            // 
            // lblMemo
            // 
            this.lblMemo.AutoSize = true;
            this.lblMemo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMemo.Location = new System.Drawing.Point(322, 543);
            this.lblMemo.Name = "lblMemo";
            this.lblMemo.Size = new System.Drawing.Size(44, 17);
            this.lblMemo.TabIndex = 6;
            this.lblMemo.Text = "备注：";
            this.lblMemo.Visible = false;
            // 
            // cboPatientSites
            // 
            this.cboPatientSites.DisplayMember = "Name";
            this.cboPatientSites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPatientSites.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPatientSites.FormattingEnabled = true;
            this.cboPatientSites.Location = new System.Drawing.Point(153, 539);
            this.cboPatientSites.Name = "cboPatientSites";
            this.cboPatientSites.Size = new System.Drawing.Size(40, 25);
            this.cboPatientSites.TabIndex = 4;
            this.cboPatientSites.ValueMember = "Value";
            this.cboPatientSites.Visible = false;
            // 
            // lblPatientSites
            // 
            this.lblPatientSites.AutoSize = true;
            this.lblPatientSites.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientSites.Location = new System.Drawing.Point(119, 545);
            this.lblPatientSites.Name = "lblPatientSites";
            this.lblPatientSites.Size = new System.Drawing.Size(44, 17);
            this.lblPatientSites.TabIndex = 3;
            this.lblPatientSites.Text = "部位：";
            this.lblPatientSites.Visible = false;
            // 
            // cboPatientPosture
            // 
            this.cboPatientPosture.DisplayMember = "Name";
            this.cboPatientPosture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPatientPosture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPatientPosture.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboPatientPosture.FormattingEnabled = true;
            this.cboPatientPosture.Location = new System.Drawing.Point(60, 539);
            this.cboPatientPosture.Name = "cboPatientPosture";
            this.cboPatientPosture.Size = new System.Drawing.Size(53, 25);
            this.cboPatientPosture.TabIndex = 3;
            this.cboPatientPosture.ValueMember = "Value";
            this.cboPatientPosture.Visible = false;
            // 
            // lblPatientPosture
            // 
            this.lblPatientPosture.AutoSize = true;
            this.lblPatientPosture.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientPosture.Location = new System.Drawing.Point(10, 544);
            this.lblPatientPosture.Name = "lblPatientPosture";
            this.lblPatientPosture.Size = new System.Drawing.Size(44, 17);
            this.lblPatientPosture.TabIndex = 0;
            this.lblPatientPosture.Text = "姿势：";
            this.lblPatientPosture.Visible = false;
            // 
            // gpPatientInfo
            // 
            this.gpPatientInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gpPatientInfo.Controls.Add(this.lblPatientId);
            this.gpPatientInfo.Controls.Add(this.txtPatientName);
            this.gpPatientInfo.Controls.Add(this.lblPatientName);
            this.gpPatientInfo.Controls.Add(this.txtPatientId);
            this.gpPatientInfo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gpPatientInfo.Location = new System.Drawing.Point(12, 12);
            this.gpPatientInfo.Name = "gpPatientInfo";
            this.gpPatientInfo.Size = new System.Drawing.Size(497, 72);
            this.gpPatientInfo.TabIndex = 7;
            this.gpPatientInfo.TabStop = false;
            this.gpPatientInfo.Text = "患者信息";
            // 
            // lblPatientId
            // 
            this.lblPatientId.AutoSize = true;
            this.lblPatientId.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientId.Location = new System.Drawing.Point(29, 33);
            this.lblPatientId.Name = "lblPatientId";
            this.lblPatientId.Size = new System.Drawing.Size(33, 17);
            this.lblPatientId.TabIndex = 4;
            this.lblPatientId.Text = "ID：";
            // 
            // txtPatientName
            // 
            this.txtPatientName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPatientName.Location = new System.Drawing.Point(297, 30);
            this.txtPatientName.MaxLength = 64;
            this.txtPatientName.Multiline = true;
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(172, 23);
            this.txtPatientName.TabIndex = 2;
            // 
            // lblPatientName
            // 
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientName.Location = new System.Drawing.Point(245, 32);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(44, 17);
            this.lblPatientName.TabIndex = 0;
            this.lblPatientName.Text = "姓名：";
            // 
            // txtPatientId
            // 
            this.txtPatientId.AcceptsReturn = true;
            this.txtPatientId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPatientId.Location = new System.Drawing.Point(70, 30);
            this.txtPatientId.MaxLength = 64;
            this.txtPatientId.Multiline = true;
            this.txtPatientId.Name = "txtPatientId";
            this.txtPatientId.Size = new System.Drawing.Size(167, 23);
            this.txtPatientId.TabIndex = 1;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(441, 544);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(50, 21);
            this.radioButton2.TabIndex = 6;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "成人";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Visible = false;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(375, 544);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(50, 21);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.Text = "儿科";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDocName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDocDiagnosis);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(17, 395);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(497, 138);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "医生信息";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "初步诊断：";
            // 
            // txtDocName
            // 
            this.txtDocName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDocName.Location = new System.Drawing.Point(84, 25);
            this.txtDocName.MaxLength = 64;
            this.txtDocName.Multiline = true;
            this.txtDocName.Name = "txtDocName";
            this.txtDocName.Size = new System.Drawing.Size(172, 23);
            this.txtDocName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(30, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "姓名：";
            // 
            // txtDocDiagnosis
            // 
            this.txtDocDiagnosis.AcceptsReturn = true;
            this.txtDocDiagnosis.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDocDiagnosis.Location = new System.Drawing.Point(84, 61);
            this.txtDocDiagnosis.MaxLength = 64;
            this.txtDocDiagnosis.Multiline = true;
            this.txtDocDiagnosis.Name = "txtDocDiagnosis";
            this.txtDocDiagnosis.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDocDiagnosis.Size = new System.Drawing.Size(395, 71);
            this.txtDocDiagnosis.TabIndex = 1;
            // 
            // btnSavePatient
            // 
            this.btnSavePatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(185)))), ((int)(((byte)(213)))));
            this.btnSavePatient.FlatAppearance.BorderSize = 0;
            this.btnSavePatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePatient.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSavePatient.Location = new System.Drawing.Point(226, 545);
            this.btnSavePatient.Name = "btnSavePatient";
            this.btnSavePatient.Size = new System.Drawing.Size(75, 24);
            this.btnSavePatient.TabIndex = 10;
            this.btnSavePatient.Text = "确定";
            this.btnSavePatient.UseVisualStyleBackColor = false;
            this.btnSavePatient.Click += new System.EventHandler(this.btnSavePatient_Click);
            // 
            // FormPatientInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(526, 587);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblMemo);
            this.Controls.Add(this.btnSavePatient);
            this.Controls.Add(this.gpAuscultationInfo);
            this.Controls.Add(this.cboPatientSites);
            this.Controls.Add(this.lblPatientSites);
            this.Controls.Add(this.gpPatientInfo);
            this.Controls.Add(this.cboPatientPosture);
            this.Controls.Add(this.lblPatientPosture);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPatientInfo";
            this.Text = "患者听诊基本信息";
            this.gpAuscultationInfo.ResumeLayout(false);
            this.gpAuscultationInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx1)).EndInit();
            this.gpPatientInfo.ResumeLayout(false);
            this.gpPatientInfo.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpAuscultationInfo;
        private System.Windows.Forms.Label lblMemo;
        internal WaterTextBox txtMemo;
        internal System.Windows.Forms.ComboBox cboPatientSites;
        private System.Windows.Forms.Label lblPatientSites;
        internal System.Windows.Forms.ComboBox cboPatientPosture;
        private System.Windows.Forms.Label lblPatientPosture;
        private System.Windows.Forms.GroupBox gpPatientInfo;
        private System.Windows.Forms.Label lblPatientId;
        internal System.Windows.Forms.TextBox txtPatientName;
        private System.Windows.Forms.Label lblPatientName;
        internal System.Windows.Forms.TextBox txtPatientId;
        private ExtendControl.ButtonEx btnSavePatient;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox txtDocName;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtDocDiagnosis;
        private ExtendControl.PictureBoxEx pictureBoxEx1;
    }
}