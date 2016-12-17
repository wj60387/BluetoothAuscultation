namespace BluetoothAuscultation.Forms
{
    partial class FormStetInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStetInfo));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSure = new BluetoothAuscultation.ExtendControl.ButtonEx();
            this.txtStetRemark = new BluetoothAuscultation.ExtendControl.WaterTextBox();
            this.txtStetFunc = new BluetoothAuscultation.ExtendControl.WaterTextBox();
            this.txtStetOwner = new BluetoothAuscultation.ExtendControl.WaterTextBox();
            this.txtStetChineseName = new BluetoothAuscultation.ExtendControl.WaterTextBox();
            this.txtStetName = new BluetoothAuscultation.ExtendControl.WaterTextBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "听诊器序列号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "听诊器名称:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "听诊器所属人:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "听诊器描述:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 227);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "听诊器备注:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSure);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 309);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(361, 62);
            this.panel1.TabIndex = 11;
            // 
            // btnSure
            // 
            this.btnSure.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(185)))), ((int)(((byte)(213)))));
            this.btnSure.FlatAppearance.BorderSize = 0;
            this.btnSure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSure.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSure.Location = new System.Drawing.Point(146, 22);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(75, 24);
            this.btnSure.TabIndex = 10;
            this.btnSure.Text = "确定";
            this.btnSure.UseVisualStyleBackColor = false;
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // txtStetRemark
            // 
            this.txtStetRemark.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStetRemark.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStetRemark.Location = new System.Drawing.Point(121, 224);
            this.txtStetRemark.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStetRemark.Multiline = true;
            this.txtStetRemark.Name = "txtStetRemark";
            this.txtStetRemark.Size = new System.Drawing.Size(213, 77);
            this.txtStetRemark.TabIndex = 8;
            this.txtStetRemark.WaterText = "听诊器备注";
            // 
            // txtStetFunc
            // 
            this.txtStetFunc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStetFunc.Location = new System.Drawing.Point(121, 181);
            this.txtStetFunc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStetFunc.Multiline = true;
            this.txtStetFunc.Name = "txtStetFunc";
            this.txtStetFunc.Size = new System.Drawing.Size(213, 21);
            this.txtStetFunc.TabIndex = 6;
            this.txtStetFunc.WaterText = "听诊器描述";
            // 
            // txtStetOwner
            // 
            this.txtStetOwner.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStetOwner.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStetOwner.Location = new System.Drawing.Point(121, 128);
            this.txtStetOwner.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStetOwner.Multiline = true;
            this.txtStetOwner.Name = "txtStetOwner";
            this.txtStetOwner.Size = new System.Drawing.Size(213, 21);
            this.txtStetOwner.TabIndex = 4;
            this.txtStetOwner.WaterText = "听诊器所属人";
            // 
            // txtStetChineseName
            // 
            this.txtStetChineseName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStetChineseName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStetChineseName.Location = new System.Drawing.Point(121, 86);
            this.txtStetChineseName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStetChineseName.Multiline = true;
            this.txtStetChineseName.Name = "txtStetChineseName";
            this.txtStetChineseName.Size = new System.Drawing.Size(213, 21);
            this.txtStetChineseName.TabIndex = 2;
            this.txtStetChineseName.WaterText = "听诊器名称";
            // 
            // txtStetName
            // 
            this.txtStetName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStetName.Enabled = false;
            this.txtStetName.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStetName.Location = new System.Drawing.Point(121, 21);
            this.txtStetName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStetName.Multiline = true;
            this.txtStetName.Name = "txtStetName";
            this.txtStetName.ReadOnly = true;
            this.txtStetName.Size = new System.Drawing.Size(213, 21);
            this.txtStetName.TabIndex = 0;
            this.txtStetName.WaterText = "听诊器序列号";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(200, 51);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(50, 21);
            this.radioButton2.TabIndex = 13;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "成人";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(121, 51);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(50, 21);
            this.radioButton1.TabIndex = 12;
            this.radioButton1.Text = "儿科";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "听诊器用途:";
            // 
            // FormStetInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 371);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtStetRemark);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtStetFunc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtStetOwner);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStetChineseName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtStetName);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormStetInfo";
            this.Text = "听诊器信息";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExtendControl.WaterTextBox txtStetName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ExtendControl.WaterTextBox txtStetChineseName;
        private System.Windows.Forms.Label label3;
        private ExtendControl.WaterTextBox txtStetOwner;
        private System.Windows.Forms.Label label4;
        private ExtendControl.WaterTextBox txtStetFunc;
        private System.Windows.Forms.Label label5;
        private ExtendControl.WaterTextBox txtStetRemark;
        private ExtendControl.ButtonEx btnSure;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label6;
    }
}