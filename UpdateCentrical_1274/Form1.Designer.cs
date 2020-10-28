namespace UpdateCentrical_1274
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.stopbtn = new System.Windows.Forms.Button();
            this.OpenLogBtn = new System.Windows.Forms.Button();
            this.OpenLogFolderBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.AllowDrop = true;
            this.textBox1.Location = new System.Drawing.Point(24, 13);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(741, 335);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // startBtn
            // 
            this.startBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.startBtn.Location = new System.Drawing.Point(675, 369);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(89, 33);
            this.startBtn.TabIndex = 1;
            this.startBtn.Text = "start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // stopbtn
            // 
            this.stopbtn.Enabled = false;
            this.stopbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.stopbtn.Location = new System.Drawing.Point(558, 369);
            this.stopbtn.Name = "stopbtn";
            this.stopbtn.Size = new System.Drawing.Size(91, 33);
            this.stopbtn.TabIndex = 2;
            this.stopbtn.Text = "Stop";
            this.stopbtn.UseVisualStyleBackColor = true;
            this.stopbtn.Click += new System.EventHandler(this.stopbtn_Click);
            // 
            // OpenLogBtn
            // 
            this.OpenLogBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.OpenLogBtn.Location = new System.Drawing.Point(446, 369);
            this.OpenLogBtn.Name = "OpenLogBtn";
            this.OpenLogBtn.Size = new System.Drawing.Size(90, 33);
            this.OpenLogBtn.TabIndex = 3;
            this.OpenLogBtn.Text = "Open Log";
            this.OpenLogBtn.UseVisualStyleBackColor = true;
            this.OpenLogBtn.Click += new System.EventHandler(this.OpenLogBtn_Click);
            // 
            // OpenLogFolderBtn
            // 
            this.OpenLogFolderBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.OpenLogFolderBtn.Location = new System.Drawing.Point(272, 369);
            this.OpenLogFolderBtn.Name = "OpenLogFolderBtn";
            this.OpenLogFolderBtn.Size = new System.Drawing.Size(151, 33);
            this.OpenLogFolderBtn.TabIndex = 4;
            this.OpenLogFolderBtn.Text = "Open Log Folder";
            this.OpenLogFolderBtn.UseVisualStyleBackColor = true;
            this.OpenLogFolderBtn.Click += new System.EventHandler(this.OpenLogFolderBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OpenLogFolderBtn);
            this.Controls.Add(this.OpenLogBtn);
            this.Controls.Add(this.stopbtn);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Update select in Centrical";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button stopbtn;
        private System.Windows.Forms.Button OpenLogBtn;
        private System.Windows.Forms.Button OpenLogFolderBtn;
    }
}

