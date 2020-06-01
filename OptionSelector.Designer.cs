namespace RegistryReader
{
    partial class OptionSelector
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
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lblSysHive = new System.Windows.Forms.Label();
            this.btnSys = new System.Windows.Forms.Button();
            this.chkLive = new System.Windows.Forms.CheckBox();
            this.btnDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Location = new System.Drawing.Point(101, 37);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(253, 25);
            this.txtPath.TabIndex = 0;
            // 
            // lblSysHive
            // 
            this.lblSysHive.AutoSize = true;
            this.lblSysHive.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSysHive.Location = new System.Drawing.Point(12, 40);
            this.lblSysHive.Name = "lblSysHive";
            this.lblSysHive.Size = new System.Drawing.Size(78, 17);
            this.lblSysHive.TabIndex = 1;
            this.lblSysHive.Text = "System Hive";
            // 
            // btnSys
            // 
            this.btnSys.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSys.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSys.Location = new System.Drawing.Point(385, 34);
            this.btnSys.Name = "btnSys";
            this.btnSys.Size = new System.Drawing.Size(75, 28);
            this.btnSys.TabIndex = 2;
            this.btnSys.Text = "Browse";
            this.btnSys.UseVisualStyleBackColor = true;
            // 
            // chkLive
            // 
            this.chkLive.AutoSize = true;
            this.chkLive.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLive.Location = new System.Drawing.Point(15, 123);
            this.chkLive.Name = "chkLive";
            this.chkLive.Size = new System.Drawing.Size(306, 21);
            this.chkLive.TabIndex = 6;
            this.chkLive.Text = "Process Live System (Requires admin privileges)";
            this.chkLive.UseVisualStyleBackColor = true;
            // 
            // btnDone
            // 
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDone.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDone.Location = new System.Drawing.Point(385, 156);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 31);
            this.btnDone.TabIndex = 7;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            // 
            // OptionSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 199);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.chkLive);
            this.Controls.Add(this.btnSys);
            this.Controls.Add(this.lblSysHive);
            this.Controls.Add(this.txtPath);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionSelector";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lblSysHive;
        private System.Windows.Forms.Button btnSys;
        private System.Windows.Forms.CheckBox chkLive;
        private System.Windows.Forms.Button btnDone;
    }
}