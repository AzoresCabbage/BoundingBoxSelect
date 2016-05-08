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
            this.picture_path = new System.Windows.Forms.Label();
            this.Btn_SelectRootPath = new System.Windows.Forms.Button();
            this.TB_basepath = new System.Windows.Forms.TextBox();
            this.Btn_SetConfig = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(12, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(984, 610);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btn_pre_picture
            // 
            this.btn_pre_picture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_pre_picture.Location = new System.Drawing.Point(669, 652);
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
            this.btn_next_picture.Location = new System.Drawing.Point(848, 653);
            this.btn_next_picture.Name = "btn_next_picture";
            this.btn_next_picture.Size = new System.Drawing.Size(120, 66);
            this.btn_next_picture.TabIndex = 2;
            this.btn_next_picture.Text = "下一张图片";
            this.btn_next_picture.UseVisualStyleBackColor = true;
            this.btn_next_picture.Click += new System.EventHandler(this.btn_next_picture_Click);
            // 
            // picture_path
            // 
            this.picture_path.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.picture_path.Location = new System.Drawing.Point(123, 651);
            this.picture_path.Name = "picture_path";
            this.picture_path.Size = new System.Drawing.Size(531, 15);
            this.picture_path.TabIndex = 5;
            this.picture_path.Text = "label1";
            // 
            // Btn_SelectRootPath
            // 
            this.Btn_SelectRootPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_SelectRootPath.Location = new System.Drawing.Point(12, 694);
            this.Btn_SelectRootPath.Name = "Btn_SelectRootPath";
            this.Btn_SelectRootPath.Size = new System.Drawing.Size(98, 23);
            this.Btn_SelectRootPath.TabIndex = 6;
            this.Btn_SelectRootPath.Text = "选择图片根目录";
            this.Btn_SelectRootPath.UseVisualStyleBackColor = true;
            this.Btn_SelectRootPath.Click += new System.EventHandler(this.button1_Click);
            // 
            // TB_basepath
            // 
            this.TB_basepath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TB_basepath.Location = new System.Drawing.Point(125, 696);
            this.TB_basepath.Name = "TB_basepath";
            this.TB_basepath.Size = new System.Drawing.Size(311, 21);
            this.TB_basepath.TabIndex = 7;
            this.TB_basepath.Text = "E:\\Databases\\PIPA\\images";
            // 
            // Btn_SetConfig
            // 
            this.Btn_SetConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_SetConfig.Location = new System.Drawing.Point(492, 696);
            this.Btn_SetConfig.Name = "Btn_SetConfig";
            this.Btn_SetConfig.Size = new System.Drawing.Size(75, 23);
            this.Btn_SetConfig.TabIndex = 12;
            this.Btn_SetConfig.Text = "设定";
            this.Btn_SetConfig.UseVisualStyleBackColor = true;
            this.Btn_SetConfig.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Location = new System.Drawing.Point(12, 651);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "当前图片地址";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Btn_SetConfig);
            this.Controls.Add(this.TB_basepath);
            this.Controls.Add(this.Btn_SelectRootPath);
            this.Controls.Add(this.picture_path);
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
        private System.Windows.Forms.Label picture_path;
        private System.Windows.Forms.Button Btn_SelectRootPath;
        private System.Windows.Forms.TextBox TB_basepath;
        private System.Windows.Forms.Button Btn_SetConfig;
        private System.Windows.Forms.Label label3;
    }
}

