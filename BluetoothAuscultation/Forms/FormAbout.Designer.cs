namespace BluetoothAuscultation.Forms
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSure = new BluetoothAuscultation.ExtendControl.ButtonEx();
            this.waterTextBox5 = new BluetoothAuscultation.ExtendControl.WaterTextBox();
            this.waterTextBox4 = new BluetoothAuscultation.ExtendControl.WaterTextBox();
            this.waterTextBox3 = new BluetoothAuscultation.ExtendControl.WaterTextBox();
            this.waterTextBox2 = new BluetoothAuscultation.ExtendControl.WaterTextBox();
            this.waterTextBox1 = new BluetoothAuscultation.ExtendControl.WaterTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "激活码:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "MAC:";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "激活时间:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "有效天数:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "版本:";
            // 
            // btnSure
            // 
            this.btnSure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(185)))), ((int)(((byte)(213)))));
            this.btnSure.FlatAppearance.BorderSize = 0;
            this.btnSure.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSure.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSure.Location = new System.Drawing.Point(263, 177);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(75, 24);
            this.btnSure.TabIndex = 10;
            this.btnSure.Text = "确定";
            this.btnSure.UseVisualStyleBackColor = false;
            this.btnSure.Click += new System.EventHandler(this.btnSure_Click);
            // 
            // waterTextBox5
            // 
            this.waterTextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.waterTextBox5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.waterTextBox5.Location = new System.Drawing.Point(79, 22);
            this.waterTextBox5.Multiline = true;
            this.waterTextBox5.Name = "waterTextBox5";
            this.waterTextBox5.ReadOnly = true;
            this.waterTextBox5.Size = new System.Drawing.Size(312, 21);
            this.waterTextBox5.TabIndex = 8;
            this.waterTextBox5.WaterText = "";
            // 
            // waterTextBox4
            // 
            this.waterTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.waterTextBox4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.waterTextBox4.Location = new System.Drawing.Point(79, 133);
            this.waterTextBox4.Multiline = true;
            this.waterTextBox4.Name = "waterTextBox4";
            this.waterTextBox4.ReadOnly = true;
            this.waterTextBox4.Size = new System.Drawing.Size(312, 21);
            this.waterTextBox4.TabIndex = 3;
            this.waterTextBox4.WaterText = "";
            this.waterTextBox4.TextChanged += new System.EventHandler(this.waterTextBox4_TextChanged);
            // 
            // waterTextBox3
            // 
            this.waterTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.waterTextBox3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.waterTextBox3.Location = new System.Drawing.Point(79, 91);
            this.waterTextBox3.Multiline = true;
            this.waterTextBox3.Name = "waterTextBox3";
            this.waterTextBox3.ReadOnly = true;
            this.waterTextBox3.Size = new System.Drawing.Size(312, 21);
            this.waterTextBox3.TabIndex = 2;
            this.waterTextBox3.WaterText = "";
            this.waterTextBox3.TextChanged += new System.EventHandler(this.waterTextBox3_TextChanged);
            // 
            // waterTextBox2
            // 
            this.waterTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.waterTextBox2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.waterTextBox2.Location = new System.Drawing.Point(79, 181);
            this.waterTextBox2.Multiline = true;
            this.waterTextBox2.Name = "waterTextBox2";
            this.waterTextBox2.ReadOnly = true;
            this.waterTextBox2.Size = new System.Drawing.Size(96, 21);
            this.waterTextBox2.TabIndex = 1;
            this.waterTextBox2.Visible = false;
            this.waterTextBox2.WaterText = "";
            this.waterTextBox2.TextChanged += new System.EventHandler(this.waterTextBox2_TextChanged);
            // 
            // waterTextBox1
            // 
            this.waterTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.waterTextBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.waterTextBox1.Location = new System.Drawing.Point(79, 52);
            this.waterTextBox1.Multiline = true;
            this.waterTextBox1.Name = "waterTextBox1";
            this.waterTextBox1.ReadOnly = true;
            this.waterTextBox1.Size = new System.Drawing.Size(312, 21);
            this.waterTextBox1.TabIndex = 0;
            this.waterTextBox1.WaterText = "";
            this.waterTextBox1.TextChanged += new System.EventHandler(this.waterTextBox1_TextChanged);
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(414, 214);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.waterTextBox5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.waterTextBox4);
            this.Controls.Add(this.waterTextBox3);
            this.Controls.Add(this.waterTextBox2);
            this.Controls.Add(this.waterTextBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.Text = "关于软件";
            this.Load += new System.EventHandler(this.FormAbout_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExtendControl.WaterTextBox waterTextBox1;
        private ExtendControl.WaterTextBox waterTextBox2;
        private ExtendControl.WaterTextBox waterTextBox3;
        private ExtendControl.WaterTextBox waterTextBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private ExtendControl.WaterTextBox waterTextBox5;
        private ExtendControl.ButtonEx btnSure;
    }
}