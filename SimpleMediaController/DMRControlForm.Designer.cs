namespace SimpleMediaController
{
    partial class DMRControlForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ctl_combo_devices = new System.Windows.Forms.ComboBox();
            this.ctl_btn_rescan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ctl_label_status = new System.Windows.Forms.Label();
            this.ctl_trackbar_time = new System.Windows.Forms.TrackBar();
            this.ctl_btn_start = new System.Windows.Forms.Button();
            this.ctl_btn_pause = new System.Windows.Forms.Button();
            this.ctl_btn_stop = new System.Windows.Forms.Button();
            this.timer_state_check = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.ctl_label_time = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ctl_trackbar_time)).BeginInit();
            this.SuspendLayout();
            // 
            // ctl_combo_devices
            // 
            this.ctl_combo_devices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctl_combo_devices.FormattingEnabled = true;
            this.ctl_combo_devices.Location = new System.Drawing.Point(12, 12);
            this.ctl_combo_devices.Name = "ctl_combo_devices";
            this.ctl_combo_devices.Size = new System.Drawing.Size(300, 21);
            this.ctl_combo_devices.TabIndex = 0;
            this.ctl_combo_devices.SelectedIndexChanged += new System.EventHandler(this.ctl_combo_devices_SelectedIndexChanged);
            // 
            // ctl_btn_rescan
            // 
            this.ctl_btn_rescan.Location = new System.Drawing.Point(318, 10);
            this.ctl_btn_rescan.Name = "ctl_btn_rescan";
            this.ctl_btn_rescan.Size = new System.Drawing.Size(75, 23);
            this.ctl_btn_rescan.TabIndex = 1;
            this.ctl_btn_rescan.Text = "再検索";
            this.ctl_btn_rescan.UseVisualStyleBackColor = true;
            this.ctl_btn_rescan.Click += new System.EventHandler(this.ctl_btn_rescan_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "ステータス：";
            // 
            // ctl_label_status
            // 
            this.ctl_label_status.Location = new System.Drawing.Point(86, 36);
            this.ctl_label_status.Name = "ctl_label_status";
            this.ctl_label_status.Size = new System.Drawing.Size(101, 14);
            this.ctl_label_status.TabIndex = 3;
            // 
            // ctl_trackbar_time
            // 
            this.ctl_trackbar_time.AutoSize = false;
            this.ctl_trackbar_time.Enabled = false;
            this.ctl_trackbar_time.LargeChange = 0;
            this.ctl_trackbar_time.Location = new System.Drawing.Point(15, 53);
            this.ctl_trackbar_time.Maximum = 100;
            this.ctl_trackbar_time.Name = "ctl_trackbar_time";
            this.ctl_trackbar_time.Size = new System.Drawing.Size(378, 20);
            this.ctl_trackbar_time.SmallChange = 0;
            this.ctl_trackbar_time.TabIndex = 4;
            this.ctl_trackbar_time.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // ctl_btn_start
            // 
            this.ctl_btn_start.Location = new System.Drawing.Point(85, 79);
            this.ctl_btn_start.Name = "ctl_btn_start";
            this.ctl_btn_start.Size = new System.Drawing.Size(75, 23);
            this.ctl_btn_start.TabIndex = 5;
            this.ctl_btn_start.Text = "再生";
            this.ctl_btn_start.UseVisualStyleBackColor = true;
            this.ctl_btn_start.Click += new System.EventHandler(this.ctl_btn_start_Click);
            // 
            // ctl_btn_pause
            // 
            this.ctl_btn_pause.Location = new System.Drawing.Point(166, 79);
            this.ctl_btn_pause.Name = "ctl_btn_pause";
            this.ctl_btn_pause.Size = new System.Drawing.Size(75, 23);
            this.ctl_btn_pause.TabIndex = 6;
            this.ctl_btn_pause.Text = "一時停止";
            this.ctl_btn_pause.UseVisualStyleBackColor = true;
            this.ctl_btn_pause.Click += new System.EventHandler(this.ctl_btn_pause_Click);
            // 
            // ctl_btn_stop
            // 
            this.ctl_btn_stop.Location = new System.Drawing.Point(247, 79);
            this.ctl_btn_stop.Name = "ctl_btn_stop";
            this.ctl_btn_stop.Size = new System.Drawing.Size(75, 23);
            this.ctl_btn_stop.TabIndex = 7;
            this.ctl_btn_stop.Text = "停止";
            this.ctl_btn_stop.UseVisualStyleBackColor = true;
            this.ctl_btn_stop.Click += new System.EventHandler(this.ctl_btn_stop_Click);
            // 
            // timer_state_check
            // 
            this.timer_state_check.Interval = 500;
            this.timer_state_check.Tick += new System.EventHandler(this.timer_state_check_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 8;
            this.label2.Text = "再生時間：";
            // 
            // ctl_label_time
            // 
            this.ctl_label_time.Location = new System.Drawing.Point(269, 36);
            this.ctl_label_time.Name = "ctl_label_time";
            this.ctl_label_time.Size = new System.Drawing.Size(124, 14);
            this.ctl_label_time.TabIndex = 9;
            // 
            // DMRControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 113);
            this.Controls.Add(this.ctl_label_time);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ctl_btn_stop);
            this.Controls.Add(this.ctl_btn_pause);
            this.Controls.Add(this.ctl_btn_start);
            this.Controls.Add(this.ctl_trackbar_time);
            this.Controls.Add(this.ctl_label_status);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctl_btn_rescan);
            this.Controls.Add(this.ctl_combo_devices);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DMRControlForm";
            this.Text = "DMRの操作";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DMRControlForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DMRControlForm_FormClosed);
            this.Load += new System.EventHandler(this.DMRControlForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ctl_trackbar_time)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ctl_combo_devices;
        private System.Windows.Forms.Button ctl_btn_rescan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ctl_label_status;
        private System.Windows.Forms.TrackBar ctl_trackbar_time;
        private System.Windows.Forms.Button ctl_btn_start;
        private System.Windows.Forms.Button ctl_btn_pause;
        private System.Windows.Forms.Button ctl_btn_stop;
        private System.Windows.Forms.Timer timer_state_check;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ctl_label_time;
    }
}