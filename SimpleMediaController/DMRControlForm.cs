using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UPNPLib;
using System.Xml;
using System.Xml.Linq;
using System.Threading;

namespace SimpleMediaController
{
    public partial class DMRControlForm : Form
    {
        struct MediaRenderInfo
        {
            public string Name;
            public string UID;

            public override string ToString()
            {
                return Name;
            }
        }

        DLNAMediaRenders _dlna_mr;

        public DMRControlForm()
        {
            InitializeComponent();

            // インスタンス化
            _dlna_mr = new DLNAMediaRenders();
        }

        private void DMRControlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 自分が閉じられた場合のみキャンセル
            if (e.CloseReason == CloseReason.UserClosing) e.Cancel = true;
        }

        private void DMRControlForm_Load(object sender, EventArgs e)
        {
            ScanDMRDevices();
        }

        private void DMRControlForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _dlna_mr.Dispose();
        }

        private void ctl_combo_devices_SelectedIndexChanged(object sender, EventArgs e)
        {
            StateChange();
        }

        private void ctl_btn_rescan_Click(object sender, EventArgs e)
        {
            ScanDMRDevices();
        }

        private void ctl_btn_start_Click(object sender, EventArgs e)
        {
            if (ctl_combo_devices.Items.Count > 0)
            {
                try
                {
                    _dlna_mr[((MediaRenderInfo)ctl_combo_devices.SelectedItem).UID].AVTransport.Play();
                }
                catch
                {
                }
            }
        }

        private void ctl_btn_pause_Click(object sender, EventArgs e)
        {
            if (ctl_combo_devices.Items.Count > 0)
            {
                try
                {
                    _dlna_mr[((MediaRenderInfo)ctl_combo_devices.SelectedItem).UID].AVTransport.Pause();
                }
                catch
                {
                }
            }
        }

        private void ctl_btn_stop_Click(object sender, EventArgs e)
        {
            if (ctl_combo_devices.Items.Count > 0)
            {
                try
                {
                    _dlna_mr[((MediaRenderInfo)ctl_combo_devices.SelectedItem).UID].AVTransport.Stop();
                }
                catch
                {
                }
            }
        }

        private void ScanDMRDevices()
        {
            // コンボボックスを初期化
            ctl_combo_devices.Items.Clear();

            // 再検索ボタンを無効化
            ctl_btn_rescan.Enabled = false;

            // 検索スレッド作成
            _dlna_mr.CreateDeviceScanThread(() =>
            {
                try
                {
                    _dlna_mr.Items.ToList().ForEach((m) =>
                    {
                        // AVTrabsoirtとRendoringControlサービスを所持しているか？
                        if (m.HasAVTransport && m.HasRenderingControl)
                        {
                            // コンボボックスに追加
                            Invoke((Func<object, int>)ctl_combo_devices.Items.Add, new object[] { new MediaRenderInfo { Name = m.FriendlyName, UID = m.UID } });
                        }
                    });

                    // コンボボックスを選択
                    if (ctl_combo_devices.Items.Count > 0)
                    {
                        Invoke((Action)(() => ctl_combo_devices.SelectedIndex = 0));
                    }
                }
                catch
                {
                }

                // タイマーを有効化
                timer_state_check.Enabled = true;

                // 検索ボタンを元に戻す
                Invoke((Action)(() => ctl_btn_rescan.Enabled = true));
            });

            // スレッド開始
            _dlna_mr.StartDevceScanThread();
        }

        public void MediaTransport(XElement metadata)
        {
            if (ctl_combo_devices.Items.Count > 0)
            {
                try
                {
                    _dlna_mr[((MediaRenderInfo)ctl_combo_devices.SelectedItem).UID].AVTransport.SetAVTransportURI("", metadata.ToString());
                    _dlna_mr[((MediaRenderInfo)ctl_combo_devices.SelectedItem).UID].AVTransport.Play();
                }
                catch
                {
                }
            }
        }

        private void timer_state_check_Tick(object sender, EventArgs e)
        {
            StateChange();
        }

        private void StateChange()
        {
            if (ctl_combo_devices.Items.Count > 0)
            {
                //ステータス変更
                try
                {
                    MediaRenderInfo info = (MediaRenderInfo)ctl_combo_devices.SelectedItem;
                    ctl_label_status.Text = _dlna_mr[info.UID].AVTransport.GetTransportInfo().CurrentTransportState;

                    DLNAMediaRender.AVTransportService.PositionInfo posInfo = _dlna_mr[info.UID].AVTransport.GetPositionInfo();
                    DateTime duration = DateTime.Parse(posInfo.TrackDuration);
                    DateTime reltime = DateTime.Parse(posInfo.RelTime);

                    ctl_trackbar_time.Maximum = (int)duration.TimeOfDay.TotalSeconds;
                    ctl_trackbar_time.Value = (int)reltime.TimeOfDay.TotalSeconds;
                    ctl_label_time.Text = reltime.ToString("HH:mm:ss");
                }
                catch
                {
                    // 例外が出たらタイマーを停止して、ステータス表示を変える
                    timer_state_check.Enabled = false;
                    ctl_label_status.Text = "---";
                    ctl_label_time.Text = "--:--:--";
                    ctl_trackbar_time.Maximum = 100;
                    ctl_trackbar_time.Value = 0;
                }

            }
        }
    }
}
