namespace Lesson1
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
            this.dgvSinhVien = new System.Windows.Forms.DataGridView();
            this.btnLoadConnected = new System.Windows.Forms.Button();
            this.btnLoadDisconnected = new System.Windows.Forms.Button();
            this.btnLoadDisconnected1 = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCreate1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinhVien)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSinhVien
            // 
            this.dgvSinhVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSinhVien.Location = new System.Drawing.Point(13, 25);
            this.dgvSinhVien.Name = "dgvSinhVien";
            this.dgvSinhVien.RowHeadersWidth = 72;
            this.dgvSinhVien.RowTemplate.Height = 31;
            this.dgvSinhVien.Size = new System.Drawing.Size(1728, 648);
            this.dgvSinhVien.TabIndex = 0;
            // 
            // btnLoadConnected
            // 
            this.btnLoadConnected.Location = new System.Drawing.Point(13, 738);
            this.btnLoadConnected.Name = "btnLoadConnected";
            this.btnLoadConnected.Size = new System.Drawing.Size(183, 76);
            this.btnLoadConnected.TabIndex = 1;
            this.btnLoadConnected.Text = "Load Connected";
            this.btnLoadConnected.UseVisualStyleBackColor = true;
            this.btnLoadConnected.Click += new System.EventHandler(this.btnLoadConnected_Click);
            // 
            // btnLoadDisconnected
            // 
            this.btnLoadDisconnected.Location = new System.Drawing.Point(278, 738);
            this.btnLoadDisconnected.Name = "btnLoadDisconnected";
            this.btnLoadDisconnected.Size = new System.Drawing.Size(228, 76);
            this.btnLoadDisconnected.TabIndex = 2;
            this.btnLoadDisconnected.Text = "Load Disconnected";
            this.btnLoadDisconnected.UseVisualStyleBackColor = true;
            this.btnLoadDisconnected.Click += new System.EventHandler(this.btnLoadDisconnected_Click);
            // 
            // btnLoadDisconnected1
            // 
            this.btnLoadDisconnected1.Location = new System.Drawing.Point(552, 738);
            this.btnLoadDisconnected1.Name = "btnLoadDisconnected1";
            this.btnLoadDisconnected1.Size = new System.Drawing.Size(228, 76);
            this.btnLoadDisconnected1.TabIndex = 3;
            this.btnLoadDisconnected1.Text = "Load Disconnected 1";
            this.btnLoadDisconnected1.UseVisualStyleBackColor = true;
            this.btnLoadDisconnected1.Click += new System.EventHandler(this.btnLoadDisconnected1_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(12, 849);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(213, 76);
            this.btnCreate.TabIndex = 1;
            this.btnCreate.Text = "Create Connected";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnCreate1
            // 
            this.btnCreate1.Location = new System.Drawing.Point(274, 849);
            this.btnCreate1.Name = "btnCreate1";
            this.btnCreate1.Size = new System.Drawing.Size(228, 76);
            this.btnCreate1.TabIndex = 4;
            this.btnCreate1.Text = "Create Disconnected";
            this.btnCreate1.UseVisualStyleBackColor = true;
            this.btnCreate1.Click += new System.EventHandler(this.btnCreate1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1753, 1134);
            this.Controls.Add(this.btnCreate1);
            this.Controls.Add(this.btnLoadDisconnected1);
            this.Controls.Add(this.btnLoadDisconnected);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnLoadConnected);
            this.Controls.Add(this.dgvSinhVien);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinhVien)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSinhVien;
        private System.Windows.Forms.Button btnLoadConnected;
        private System.Windows.Forms.Button btnLoadDisconnected;
        private System.Windows.Forms.Button btnLoadDisconnected1;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCreate1;
    }
}

