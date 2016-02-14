using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UPNPLib;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace SimpleMediaController
{
    public partial class MainForm : Form
    {
        DMRControlForm _dmr_form;
        DLNAMediaServers _dlna_ms;

        public MainForm()
        {
            InitializeComponent();

            // タイトルにバージョン情報を付加
            Text += String.Format(" v{0}", Application.ProductVersion);

            // DMR制御フォームを表示
            _dmr_form = new DMRControlForm();
            _dmr_form.Show(this);

            // インスタンス化
            _dlna_ms = new DLNAMediaServers();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ScanDMCDevices();
        }

        private void ctl_btn_rescan_Click(object sender, EventArgs e)
        {
            // 再検索を実施
            ScanDMCDevices();
        }

        private void ScanDMCDevices()
        {
            // ツリービューとリストビューを初期化
            ctl_treeview_devices.Nodes.Clear();
            ctl_listview_item.Items.Clear();

            // 再検索ボタンを無効化
            ctl_btn_rescan.Enabled = false;

            // 検索スレッド作成
            _dlna_ms.CreateDeviceScanThread(() =>
            {
                try
                {
                    // ツリービューに反映
                    _dlna_ms.Items.ToList().ForEach((ms) =>
                        {
                            // ContentDirectoryサービスを所持しているか？
                            if (ms.HasContentDirectory)
                            {
                                // ルートノード作成
                                TreeNode root_node = (TreeNode)Invoke((Func<string, TreeNode>)ctl_treeview_devices.Nodes.Add, new object[] { ms.FriendlyName });
                                root_node.Tag = ms.UID;

                                // コンテナ一覧を作成
                                Action<TreeNode, string> BuildTreeNode = null;
                                BuildTreeNode = (node, object_id) =>
                                {
                                    // メタデータを取得
                                    XElement didl = ms.ContentDirectory.BrowseDirectChildren(object_id, "", 0, 0, "");

                                    // containerタグを抽出
                                    didl.Elements(DLNAMetaData.NS + "container")
                                        .ToList().ForEach((e) =>
                                        {
                                            // タイトルとオブジェクトIDを抽出
                                            string t = e.Element(DLNAMetaData.NS_DC + "title").Value;
                                            string id = e.Attribute("id").Value;

                                            // ノードに追加
                                            TreeNode n = (TreeNode)Invoke((Func<string, TreeNode>)node.Nodes.Add, new object[] { t });
                                            n.Tag = id;

                                            // 再探索
                                            BuildTreeNode(n, id);
                                        });
                                };

                                // ルートオブジェクト(ID:0)を指定してツリーを構築
                                BuildTreeNode(root_node, "0");
                            }
                        });
                }
                catch
                {
                }
                // 再検索ボタンを有効化
                ctl_btn_rescan.Invoke((Action)(() => ctl_btn_rescan.Enabled = true));
            });

            // スレッド実行
            _dlna_ms.StartDevceScanThread();
        }

        private void ctl_treeview_devices_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // アイテムが選択されたか
            if (e.Action == TreeViewAction.ByMouse)
            {
                // ルートノードだったら何もしない
                if (e.Node.Parent == null) return;

                // リストビューをクリア
                ctl_listview_item.Items.Clear();

                // ルートノードを抽出
                TreeNode root = e.Node.Parent;
                while (root.Parent != null) root = root.Parent;

                // アイテム一覧を取得する
                try
                {
                    XElement didl = _dlna_ms[(string)root.Tag].ContentDirectory.BrowseDirectChildren((string)e.Node.Tag, "", 0, 0, "");
                    didl.Elements(DLNAMetaData.NS + "item").ToList()
                        .ForEach((elem) =>
                        {
                            // タイトル、ID、再生時間、メディアタイプを取得
                            string title = elem.Element(DLNAMetaData.NS_DC + "title").Value;
                            string id = elem.Attribute("id").Value;

                            XElement res = elem.Elements(DLNAMetaData.NS + "res").Where((r) =>
                            {
                                return r.Attribute("protocolInfo").Value.Contains(":video/");
                            }).First();
                            string duration = res.Attribute("duration") != null ? res.Attribute("duration").Value : "";
                            string type = res.Attribute("protocolInfo").Value.Split(':')[2];

                            // リストビューアイテムを追加
                            ListViewItem item = ctl_listview_item.Items.Add(title);
                            item.Tag = id;
                            item.SubItems.Add(duration);
                            item.SubItems.Add(type);
                        });
                }
                catch
                {
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _dlna_ms.Dispose();
        }

        private void ctl_listview_item_DoubleClick(object sender, EventArgs e)
        {
            // メタデータ取得
            try
            {
                TreeNode root_node = ctl_treeview_devices.SelectedNode;
                while (root_node.Parent != null) root_node = root_node.Parent;
                XElement metadata = _dlna_ms[(string)root_node.Tag]
                    .ContentDirectory
                    .BrowseDirectChildren((string)ctl_listview_item.SelectedItems[0].Tag, "", 0, 0, "");

                // DMRへ送る
                _dmr_form.MediaTransport(metadata);
            }
            catch
            {
            }
        }
    }
}
