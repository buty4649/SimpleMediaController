namespace SimpleMediaController
{
    partial class MainForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ctl_treeview_devices = new System.Windows.Forms.TreeView();
            this.ctl_btn_rescan = new System.Windows.Forms.Button();
            this.ctl_listview_item = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ctl_listview_item);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(664, 460);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.ctl_treeview_devices);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ctl_btn_rescan);
            this.splitContainer2.Size = new System.Drawing.Size(200, 460);
            this.splitContainer2.SplitterDistance = 425;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            // 
            // ctl_treeview_devices
            // 
            this.ctl_treeview_devices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctl_treeview_devices.Location = new System.Drawing.Point(0, 0);
            this.ctl_treeview_devices.Name = "ctl_treeview_devices";
            this.ctl_treeview_devices.Size = new System.Drawing.Size(200, 425);
            this.ctl_treeview_devices.TabIndex = 0;
            this.ctl_treeview_devices.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ctl_treeview_devices_AfterSelect);
            // 
            // ctl_btn_rescan
            // 
            this.ctl_btn_rescan.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ctl_btn_rescan.Location = new System.Drawing.Point(63, 5);
            this.ctl_btn_rescan.Name = "ctl_btn_rescan";
            this.ctl_btn_rescan.Size = new System.Drawing.Size(75, 30);
            this.ctl_btn_rescan.TabIndex = 0;
            this.ctl_btn_rescan.Text = "再検索";
            this.ctl_btn_rescan.UseVisualStyleBackColor = true;
            this.ctl_btn_rescan.Click += new System.EventHandler(this.ctl_btn_rescan_Click);
            // 
            // ctl_listview_item
            // 
            this.ctl_listview_item.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.ctl_listview_item.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctl_listview_item.FullRowSelect = true;
            this.ctl_listview_item.GridLines = true;
            this.ctl_listview_item.HideSelection = false;
            this.ctl_listview_item.Location = new System.Drawing.Point(0, 0);
            this.ctl_listview_item.MultiSelect = false;
            this.ctl_listview_item.Name = "ctl_listview_item";
            this.ctl_listview_item.Size = new System.Drawing.Size(463, 460);
            this.ctl_listview_item.TabIndex = 0;
            this.ctl_listview_item.UseCompatibleStateImageBehavior = false;
            this.ctl_listview_item.View = System.Windows.Forms.View.Details;
            this.ctl_listview_item.DoubleClick += new System.EventHandler(this.ctl_listview_item_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "タイトル";
            this.columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "再生時間";
            this.columnHeader2.Width = 85;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "タイプ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 460);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(680, 500);
            this.Name = "MainForm";
            this.Text = "SimpleMediaController";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button ctl_btn_rescan;
        private System.Windows.Forms.TreeView ctl_treeview_devices;
        private System.Windows.Forms.ListView ctl_listview_item;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}

