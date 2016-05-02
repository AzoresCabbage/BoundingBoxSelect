namespace BoundingBoxSelect
{
    partial class FrameArea
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Btn_pre_frame_ = new System.Windows.Forms.Button();
            this.Pb_frame_ = new System.Windows.Forms.PictureBox();
            this.L_id_hint_ = new System.Windows.Forms.Label();
            this.Tb_id_ = new System.Windows.Forms.TextBox();
            this.Btn_submit_Bbox_ = new System.Windows.Forms.Button();
            this.Btn_next_frame_ = new System.Windows.Forms.Button();
            this.Btn_no_people_ = new System.Windows.Forms.Button();
            this.Btn_redo_ = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Pb_frame_)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_pre_frame_
            // 
            this.Btn_pre_frame_.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_pre_frame_.Location = new System.Drawing.Point(253, 345);
            this.Btn_pre_frame_.Name = "Btn_pre_frame_";
            this.Btn_pre_frame_.Size = new System.Drawing.Size(75, 23);
            this.Btn_pre_frame_.TabIndex = 0;
            this.Btn_pre_frame_.Text = "上一帧";
            this.Btn_pre_frame_.UseVisualStyleBackColor = true;
            this.Btn_pre_frame_.Click += new System.EventHandler(this.Btn_pre_frame_Click);
            // 
            // Pb_frame_
            // 
            this.Pb_frame_.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pb_frame_.Location = new System.Drawing.Point(0, 0);
            this.Pb_frame_.Margin = new System.Windows.Forms.Padding(0);
            this.Pb_frame_.Name = "Pb_frame_";
            this.Pb_frame_.Size = new System.Drawing.Size(618, 319);
            this.Pb_frame_.TabIndex = 1;
            this.Pb_frame_.TabStop = false;
            // 
            // L_id_hint_
            // 
            this.L_id_hint_.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.L_id_hint_.Location = new System.Drawing.Point(3, 347);
            this.L_id_hint_.Name = "L_id_hint_";
            this.L_id_hint_.Size = new System.Drawing.Size(33, 23);
            this.L_id_hint_.TabIndex = 2;
            this.L_id_hint_.Text = "编号";
            this.L_id_hint_.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Tb_id_
            // 
            this.Tb_id_.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Tb_id_.Location = new System.Drawing.Point(42, 347);
            this.Tb_id_.Name = "Tb_id_";
            this.Tb_id_.Size = new System.Drawing.Size(100, 21);
            this.Tb_id_.TabIndex = 3;
            // 
            // Btn_submit_Bbox_
            // 
            this.Btn_submit_Bbox_.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_submit_Bbox_.Location = new System.Drawing.Point(159, 345);
            this.Btn_submit_Bbox_.Name = "Btn_submit_Bbox_";
            this.Btn_submit_Bbox_.Size = new System.Drawing.Size(75, 23);
            this.Btn_submit_Bbox_.TabIndex = 4;
            this.Btn_submit_Bbox_.Text = "标下一个框";
            this.Btn_submit_Bbox_.UseVisualStyleBackColor = true;
            this.Btn_submit_Bbox_.Click += new System.EventHandler(this.Btn_submit_Bbox_Click);
            // 
            // Btn_next_frame_
            // 
            this.Btn_next_frame_.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_next_frame_.Location = new System.Drawing.Point(347, 345);
            this.Btn_next_frame_.Name = "Btn_next_frame_";
            this.Btn_next_frame_.Size = new System.Drawing.Size(75, 23);
            this.Btn_next_frame_.TabIndex = 5;
            this.Btn_next_frame_.Text = "下一帧";
            this.Btn_next_frame_.UseVisualStyleBackColor = true;
            this.Btn_next_frame_.Click += new System.EventHandler(this.Btn_next_frame_Click);
            // 
            // Btn_no_people_
            // 
            this.Btn_no_people_.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_no_people_.Location = new System.Drawing.Point(441, 345);
            this.Btn_no_people_.Name = "Btn_no_people_";
            this.Btn_no_people_.Size = new System.Drawing.Size(75, 23);
            this.Btn_no_people_.TabIndex = 6;
            this.Btn_no_people_.Text = "跳过此图";
            this.Btn_no_people_.UseVisualStyleBackColor = true;
            this.Btn_no_people_.Click += new System.EventHandler(this.Btn_no_people_Click);
            // 
            // Btn_redo_
            // 
            this.Btn_redo_.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_redo_.Location = new System.Drawing.Point(532, 345);
            this.Btn_redo_.Name = "Btn_redo_";
            this.Btn_redo_.Size = new System.Drawing.Size(75, 23);
            this.Btn_redo_.TabIndex = 7;
            this.Btn_redo_.Text = "撤销上一个";
            this.Btn_redo_.UseVisualStyleBackColor = true;
            this.Btn_redo_.Click += new System.EventHandler(this.Btn_redo_Click);
            // 
            // FrameArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.Btn_redo_);
            this.Controls.Add(this.Btn_no_people_);
            this.Controls.Add(this.Pb_frame_);
            this.Controls.Add(this.Btn_next_frame_);
            this.Controls.Add(this.Btn_submit_Bbox_);
            this.Controls.Add(this.Tb_id_);
            this.Controls.Add(this.L_id_hint_);
            this.Controls.Add(this.Btn_pre_frame_);
            this.Name = "FrameArea";
            this.Size = new System.Drawing.Size(629, 376);
            this.Load += new System.EventHandler(this.FrameArea_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Pb_frame_)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_pre_frame_;
        private System.Windows.Forms.PictureBox Pb_frame_;
        private System.Windows.Forms.Label L_id_hint_;
        private System.Windows.Forms.TextBox Tb_id_;
        private System.Windows.Forms.Button Btn_submit_Bbox_;
        private System.Windows.Forms.Button Btn_next_frame_;
        private System.Windows.Forms.Button Btn_no_people_;
        private System.Windows.Forms.Button Btn_redo_;
    }
}
