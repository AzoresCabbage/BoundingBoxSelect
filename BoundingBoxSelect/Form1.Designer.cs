namespace BoundingBoxSelect
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_pre_picture = new System.Windows.Forms.Button();
            this.btn_next_picture = new System.Windows.Forms.Button();
            this.btn_next_box = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.picture_path = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(12, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(830, 610);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btn_pre_picture
            // 
            this.btn_pre_picture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_pre_picture.Location = new System.Drawing.Point(876, 565);
            this.btn_pre_picture.Name = "btn_pre_picture";
            this.btn_pre_picture.Size = new System.Drawing.Size(120, 66);
            this.btn_pre_picture.TabIndex = 1;
            this.btn_pre_picture.Text = "上一张图片";
            this.btn_pre_picture.UseVisualStyleBackColor = true;
            this.btn_pre_picture.Click += new System.EventHandler(this.btn_pre_picture_Click);
            // 
            // btn_next_picture
            // 
            this.btn_next_picture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_next_picture.Location = new System.Drawing.Point(876, 651);
            this.btn_next_picture.Name = "btn_next_picture";
            this.btn_next_picture.Size = new System.Drawing.Size(120, 66);
            this.btn_next_picture.TabIndex = 2;
            this.btn_next_picture.Text = "下一张图片";
            this.btn_next_picture.UseVisualStyleBackColor = true;
            this.btn_next_picture.Click += new System.EventHandler(this.btn_next_picture_Click);
            // 
            // btn_next_box
            // 
            this.btn_next_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_next_box.Location = new System.Drawing.Point(876, 473);
            this.btn_next_box.Name = "btn_next_box";
            this.btn_next_box.Size = new System.Drawing.Size(120, 66);
            this.btn_next_box.TabIndex = 3;
            this.btn_next_box.Text = "标下一个框";
            this.btn_next_box.UseVisualStyleBackColor = true;
            this.btn_next_box.Click += new System.EventHandler(this.btn_next_box_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(876, 176);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(120, 21);
            this.textBox1.TabIndex = 4;
            // 
            // picture_path
            // 
            this.picture_path.Location = new System.Drawing.Point(77, 651);
            this.picture_path.Name = "picture_path";
            this.picture_path.Size = new System.Drawing.Size(649, 45);
            this.picture_path.TabIndex = 5;
            this.picture_path.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.picture_path);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_next_box);
            this.Controls.Add(this.btn_next_picture);
            this.Controls.Add(this.btn_pre_picture);
            this.Controls.Add(this.pictureBox1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "框标注程序";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_pre_picture;
        private System.Windows.Forms.Button btn_next_picture;
        private System.Windows.Forms.Button btn_next_box;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label picture_path;
    }
}

