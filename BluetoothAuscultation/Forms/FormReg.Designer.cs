namespace BluetoothAuscultation.Forms
{
    partial class FormReg
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
            this.groupBoxRegisteredCode = new System.Windows.Forms.GroupBox();
            this.btnRegistered = new BluetoothAuscultation.ExtendControl.ButtonEx();
            this.txtRegisteredCode = new BluetoothAuscultation.ExtendControl.WaterTextBox();
            this.groupBoxRegisteredCode.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxRegisteredCode
            // 
            this.groupBoxRegisteredCode.Controls.Add(this.btnRegistered);
            this.groupBoxRegisteredCode.Controls.Add(this.txtRegisteredCode);
            this.groupBoxRegisteredCode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxRegisteredCode.Location = new System.Drawing.Point(12, 8);
            this.groupBoxRegisteredCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxRegisteredCode.Name = "groupBoxRegisteredCode";
            this.groupBoxRegisteredCode.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxRegisteredCode.Size = new System.Drawing.Size(407, 89);
            this.groupBoxRegisteredCode.TabIndex = 26;
            this.groupBoxRegisteredCode.TabStop = false;
            this.groupBoxRegisteredCode.Text = "软件激活";
            // 
            // btnRegistered
            // 
            this.btnRegistered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(185)))), ((int)(((byte)(213)))));
            this.btnRegistered.FlatAppearance.BorderSize = 0;
            this.btnRegistered.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistered.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRegistered.Location = new System.Drawing.Point(326, 39);
            this.btnRegistered.Name = "btnRegistered";
            this.btnRegistered.Size = new System.Drawing.Size(75, 26);
            this.btnRegistered.TabIndex = 27;
            this.btnRegistered.Text = "激活";
            this.btnRegistered.UseVisualStyleBackColor = false;
            this.btnRegistered.Click += new System.EventHandler(this.btnRegistered_Click);
            // 
            // txtRegisteredCode
            // 
            this.txtRegisteredCode.Location = new System.Drawing.Point(12, 39);
            this.txtRegisteredCode.Name = "txtRegisteredCode";
            this.txtRegisteredCode.Size = new System.Drawing.Size(314, 26);
            this.txtRegisteredCode.TabIndex = 3;
            this.txtRegisteredCode.WaterText = "请输入注册码";
            // 
            // FormReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(430, 158);
            this.Controls.Add(this.groupBoxRegisteredCode);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormReg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "软件授权";
            this.groupBoxRegisteredCode.ResumeLayout(false);
            this.groupBoxRegisteredCode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxRegisteredCode;
        private ExtendControl.ButtonEx btnRegistered;
        private ExtendControl.WaterTextBox txtRegisteredCode;
    }
}