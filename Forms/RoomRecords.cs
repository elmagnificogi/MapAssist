using MapAssist.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace MapAssist.Forms
{
    public partial class RoomRecords : Form
    {
        public RoomRecords()
        {
            InitializeComponent();
            RoomListViewReset();
        }

        private void RoomRecords_Load(object sender, EventArgs e)
        {
            listViewRefresh();
            timer1.Enabled = true;
        }

        void listViewRefresh()
        {
            try
            {
                //listView1.BeginUpdate();
                var num = Program.roomRecords.Count;

                if (listView1.Items.Count < num)
                {
                    for (var i = 0; i < num - listView1.Items.Count; i++)
                    {
                        var lvi = new ListViewItem();
                        var cname = new ListViewSubItem();
                        var rname = new ListViewSubItem();
                        var pass = new ListViewSubItem();
                        var time = new ListViewSubItem();
                        lvi.SubItems.Add(cname);
                        lvi.SubItems.Add(rname);
                        lvi.SubItems.Add(pass);
                        lvi.SubItems.Add(time);
                        listView1.Items.Add(lvi);
                    }
                }
                else
                {
                    for (var i = 0; i < listView1.Items.Count - num; i++)
                        listView1.Items.RemoveAt(listView1.Items.Count - 1);
                }

                // clean it
                foreach (ListViewItem listViewItem in listView1.Items)
                {
                    foreach (ListViewSubItem subItem in listViewItem.SubItems)
                    {
                        subItem.Text = "";
                    }
                }

                for (var i = num-1; i >= 0; i--)
                {
                    RoomRecord room = Program.roomRecords[i];
                    listView1.Items[num - 1 - i].SubItems[0].Text = room.CharacterName;
                    listView1.Items[num - 1 - i].SubItems[1].Text = room.RoomName;
                    listView1.Items[num - 1 - i].SubItems[2].Text = room.RoomPassword;
                    listView1.Items[num - 1 - i].SubItems[3].Text = room.time.ToString("HH:mm:ss");
                }

                //listView1.EndUpdate();
                listView1.Refresh();
            }
            catch
            {

            }
        }

        private void RoomListViewReset()
        {
            //添加列名
            var c1 = new ColumnHeader();
            c1.Width = 80;
            c1.Text = "角色名";
            var c2 = new ColumnHeader();
            c2.Width = 100;
            c2.Text = "房间名";
            var c3 = new ColumnHeader();
            c3.Width = 100;
            c3.Text = "密码";
            var c4 = new ColumnHeader();
            c4.Width = 75;
            c4.Text = "时间";

            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;
            listView1.View = View.Details;  
            listView1.HoverSelection = false;  
            listView1.Columns.Add(c1);
            listView1.Columns.Add(c2);
            listView1.Columns.Add(c3);
            listView1.Columns.Add(c4);
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // copy content
                var selectCount = listView1.SelectedItems.Count;
                if (selectCount > 0)
                {
                    ListViewItem lv = listView1.GetItemAt(e.X, e.Y);
                    if (lv != null)
                    {
                        ListViewSubItem slv = lv.GetSubItemAt(e.X, e.Y);
                        if (slv != null)
                            Clipboard.SetDataObject(slv.Text);
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            listViewRefresh();

            // test
            //Program.roomRecords.Add(new RoomRecord("elmagnifico", "Game324934234", "Game324934234"));
        }
    }
}
