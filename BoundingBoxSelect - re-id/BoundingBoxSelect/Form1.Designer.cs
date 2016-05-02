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
            this.picture_path = new System.Windows.Forms.Label();
            this.Btn_SelectRootPath = new System.Windows.Forms.Button();
            this.TB_basepath = new System.Windows.Forms.TextBox();
            this.Btn_SetConfig = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.frameArea4 = new BoundingBoxSelect.FrameArea();
            this.frameArea3 = new BoundingBoxSelect.FrameArea();
            this.frameArea2 = new BoundingBoxSelect.FrameArea();
            this.frameArea1 = new BoundingBoxSelect.FrameArea();
            this.Btn_all_forward = new System.Windows.Forms.Button();
            this.Btn_all_backward = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // picture_path
            // 
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
            this.TB_basepath.Text = "D:\\Workplace\\WangYujie\\VS2013-program\\BoundingBoxSelect - re-id\\data";
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
            this.label3.Location = new System.Drawing.Point(12, 651);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "当前图片地址";
            // 
            // frameArea4
            // 
            this.frameArea4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.frameArea4.Location = new System.Drawing.Point(498, 292);
            this.frameArea4.Name = "frameArea4";
            this.frameArea4.Size = new System.Drawing.Size(504, 298);
            this.frameArea4.TabIndex = 17;
            // 
            // frameArea3
            // 
            this.frameArea3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.frameArea3.Location = new System.Drawing.Point(-6, 292);
            this.frameArea3.Name = "frameArea3";
            this.frameArea3.Size = new System.Drawing.Size(504, 298);
            this.frameArea3.TabIndex = 16;
            // 
            // frameArea2
            // 
            this.frameArea2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.frameArea2.Location = new System.Drawing.Point(498, -12);
            this.frameArea2.Name = "frameArea2";
            this.frameArea2.Size = new System.Drawing.Size(504, 298);
            this.frameArea2.TabIndex = 15;
            // 
            // frameArea1
            // 
            this.frameArea1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.frameArea1.Location = new System.Drawing.Point(-6, -12);
            this.frameArea1.Name = "frameArea1";
            this.frameArea1.Size = new System.Drawing.Size(504, 298);
            this.frameArea1.TabIndex = 14;
            // 
            // Btn_all_forward
            // 
            this.Btn_all_forward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_all_forward.Location = new System.Drawing.Point(603, 696);
            this.Btn_all_forward.Name = "Btn_all_forward";
            this.Btn_all_forward.Size = new System.Drawing.Size(75, 23);
            this.Btn_all_forward.TabIndex = 18;
            this.Btn_all_forward.Text = "集体向前";
            this.Btn_all_forward.UseVisualStyleBackColor = true;
            this.Btn_all_forward.Click += new System.EventHandler(this.Btn_all_forward_Click);
            // 
            // Btn_all_backward
            // 
            this.Btn_all_backward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_all_backward.Location = new System.Drawing.Point(724, 696);
            this.Btn_all_backward.Name = "Btn_all_backward";
            this.Btn_all_backward.Size = new System.Drawing.Size(75, 23);
            this.Btn_all_backward.TabIndex = 19;
            this.Btn_all_backward.Text = "集体向后";
            this.Btn_all_backward.UseVisualStyleBackColor = true;
            this.Btn_all_backward.Click += new System.EventHandler(this.Btn_all_backward_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.Btn_all_backward);
            this.Controls.Add(this.Btn_all_forward);
            this.Controls.Add(this.frameArea4);
            this.Controls.Add(this.frameArea3);
            this.Controls.Add(this.frameArea2);
            this.Controls.Add(this.frameArea1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Btn_SetConfig);
            this.Controls.Add(this.TB_basepath);
            this.Controls.Add(this.Btn_SelectRootPath);
            this.Controls.Add(this.picture_path);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "框标注程序";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label picture_path;
        private System.Windows.Forms.Button Btn_SelectRootPath;
        private System.Windows.Forms.TextBox TB_basepath;
        private System.Windows.Forms.Button Btn_SetConfig;
        private System.Windows.Forms.Label label3;
        private FrameArea frameArea1;
        private FrameArea frameArea2;
        private FrameArea frameArea3;
        private FrameArea frameArea4;
        private System.Windows.Forms.Button Btn_all_forward;
        private System.Windows.Forms.Button Btn_all_backward;
    }
}

