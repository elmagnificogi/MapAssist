using MapAssist.Helpers;
using MapAssist.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MapAssist.Types
{
    public class DCTrack
    {
        private List<DC> diabloCloneProgress = new List<DC>();
        private List<DC> show = new List<DC>();

        public DCTrack()
        {
            var task = Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        if (MapAssistConfiguration.Loaded.DCTrack.Enabled)
                        {
                            var dclist = HttpHelper.GetDC();
                            dclist = dclist.Replace('\'', '\"');
                            var dcRaws = Serializer.JsonToObject<List<DCRaw>>(dclist);
                            diabloCloneProgress.Clear();
                            if (dcRaws == null || dcRaws?.Count == 0)
                            {
                                dcRaws = new List<DCRaw>();
                                dcRaws.Add(new DCRaw("3", "1", "1", "0"));
                                dcRaws.Add(new DCRaw("3", "1", "2", "0"));
                                dcRaws.Add(new DCRaw("3", "2", "1", "0"));
                                dcRaws.Add(new DCRaw("3", "2", "2", "0"));
                                dcRaws.Add(new DCRaw("2", "1", "1", "0"));
                                dcRaws.Add(new DCRaw("2", "1", "2", "0"));
                                dcRaws.Add(new DCRaw("2", "2", "1", "0"));
                                dcRaws.Add(new DCRaw("2", "2", "2", "0"));
                                dcRaws.Add(new DCRaw("1", "1", "1", "0"));
                                dcRaws.Add(new DCRaw("1", "1", "2", "0"));
                                dcRaws.Add(new DCRaw("1", "2", "1", "0"));
                                dcRaws.Add(new DCRaw("1", "2", "2", "0"));
                            }

                            foreach (DCRaw dcr in dcRaws)
                            {
                                string server;
                                string ladder;
                                string sc;
                                if (dcr.region.Equals("1"))
                                    server = "美服";
                                else if (dcr.region.Equals("2"))
                                    server = "欧服";
                                else if (dcr.region.Equals("3"))
                                    server = "亚服";
                                else
                                    server = "错误";

                                if (dcr.ladder.Equals("2"))
                                    ladder = "经典";
                                else if (dcr.ladder.Equals("1"))
                                    ladder = "天梯";
                                else
                                    ladder = "错误";

                                if (dcr.hc.Equals("2"))
                                    sc = "普通";
                                else if (dcr.hc.Equals("1"))
                                    sc = "专家";
                                else
                                    sc = "错误";
                                var pro = int.Parse(dcr.progress);
                                diabloCloneProgress.Add(new DC(server, sc, ladder, pro));
                            }

                            show.Clear();
                            var warningLevel = int.Parse("" + (MapAssistConfiguration.Loaded.DCTrack.WarningLevel.Trim())[0]);

                            if (MapAssistConfiguration.Loaded.DCTrack.Asia)
                            {
                                if (MapAssistConfiguration.Loaded.DCTrack.Ladder)
                                {
                                    if (MapAssistConfiguration.Loaded.DCTrack.Normal)
                                    {
                                        var dc = diabloCloneProgress.FirstOrDefault(x =>
                                        x.region.Equals("亚服") &&
                                        x.ladder.Equals("天梯") &&
                                        x.sc.Equals("普通"));
                                        if (dc != null)
                                            show.Add(dc);
                                    }
                                    if (MapAssistConfiguration.Loaded.DCTrack.Hard)
                                    {
                                        var dc = diabloCloneProgress.FirstOrDefault(x =>
                                            x.region.Equals("亚服") &&
                                            x.ladder.Equals("天梯") &&
                                            x.sc.Equals("专家"));
                                        if (dc != null)
                                            show.Add(dc);
                                    }
                                }
                                if (MapAssistConfiguration.Loaded.DCTrack.Classic)
                                {
                                    if (MapAssistConfiguration.Loaded.DCTrack.Normal)
                                    {
                                        var dc = diabloCloneProgress.FirstOrDefault(x =>
                                        x.region.Equals("亚服") &&
                                        x.ladder.Equals("经典") &&
                                        x.sc.Equals("普通"));
                                        if (dc != null)
                                            show.Add(dc);
                                    }
                                    if (MapAssistConfiguration.Loaded.DCTrack.Hard)
                                    {
                                        var dc = diabloCloneProgress.FirstOrDefault(x =>
                                            x.region.Equals("亚服") &&
                                            x.ladder.Equals("经典") &&
                                            x.sc.Equals("专家"));
                                        if (dc != null)
                                            show.Add(dc);
                                    }
                                }
                            }

                            if (MapAssistConfiguration.Loaded.DCTrack.America)
                            {
                                if (MapAssistConfiguration.Loaded.DCTrack.Ladder)
                                {
                                    if (MapAssistConfiguration.Loaded.DCTrack.Normal)
                                    {
                                        var dc = diabloCloneProgress.FirstOrDefault(x =>
                                        x.region.Equals("美服") &&
                                        x.ladder.Equals("天梯") &&
                                        x.sc.Equals("普通"));
                                        if (dc != null)
                                            show.Add(dc);
                                    }
                                    if (MapAssistConfiguration.Loaded.DCTrack.Hard)
                                    {
                                        var dc = diabloCloneProgress.FirstOrDefault(x =>
                                            x.region.Equals("美服") &&
                                            x.ladder.Equals("天梯") &&
                                            x.sc.Equals("专家"));
                                        if (dc != null)
                                            show.Add(dc);
                                    }
                                }
                                if (MapAssistConfiguration.Loaded.DCTrack.Classic)
                                {
                                    if (MapAssistConfiguration.Loaded.DCTrack.Normal)
                                    {
                                        var dc = diabloCloneProgress.FirstOrDefault(x =>
                                        x.region.Equals("美服") &&
                                        x.ladder.Equals("经典") &&
                                        x.sc.Equals("普通"));
                                        if (dc != null)
                                            show.Add(dc);
                                    }
                                    if (MapAssistConfiguration.Loaded.DCTrack.Hard)
                                    {
                                        var dc = diabloCloneProgress.FirstOrDefault(x =>
                                            x.region.Equals("美服") &&
                                            x.ladder.Equals("经典") &&
                                            x.sc.Equals("专家"));
                                        if (dc != null)
                                            show.Add(dc);
                                    }
                                }
                            }

                            if (MapAssistConfiguration.Loaded.DCTrack.Europe)
                            {
                                if (MapAssistConfiguration.Loaded.DCTrack.Ladder)
                                {
                                    if (MapAssistConfiguration.Loaded.DCTrack.Normal)
                                    {
                                        var dc = diabloCloneProgress.FirstOrDefault(x =>
                                        x.region.Equals("欧服") &&
                                        x.ladder.Equals("天梯") &&
                                        x.sc.Equals("普通"));
                                        if (dc != null)
                                            show.Add(dc);
                                    }
                                    if (MapAssistConfiguration.Loaded.DCTrack.Hard)
                                    {
                                        var dc = diabloCloneProgress.FirstOrDefault(x =>
                                            x.region.Equals("欧服") &&
                                            x.ladder.Equals("天梯") &&
                                            x.sc.Equals("专家"));
                                        if (dc != null)
                                            show.Add(dc);
                                    }
                                }
                                if (MapAssistConfiguration.Loaded.DCTrack.Classic)
                                {
                                    if (MapAssistConfiguration.Loaded.DCTrack.Normal)
                                    {
                                        var dc = diabloCloneProgress.FirstOrDefault(x =>
                                        x.region.Equals("欧服") &&
                                        x.ladder.Equals("经典") &&
                                        x.sc.Equals("普通"));
                                        if (dc != null)
                                            show.Add(dc);
                                    }
                                    if (MapAssistConfiguration.Loaded.DCTrack.Hard)
                                    {
                                        var dc = diabloCloneProgress.FirstOrDefault(x =>
                                            x.region.Equals("欧服") &&
                                            x.ladder.Equals("经典") &&
                                            x.sc.Equals("专家"));
                                        if (dc != null)
                                            show.Add(dc);
                                    }
                                }
                            }

                            if (MapAssistConfiguration.Loaded.DCTrack.Sound)
                            {
                                if (show.Count > 0)
                                {
                                    var beep = false;
                                    foreach (var dc in show)
                                    {
                                        if (dc.progress >= warningLevel)
                                            beep = true;
                                    }
                                    if (beep)
                                        WindowsExternal.Beep(800, 300);
                                }

                            }
                        }
                    }
                    catch
                    {
                        show.Clear();
                        //Debug.WriteLine(ex);
                        show.Add(new DC("错误", "错误", "错误", 0));
                    }

                    Thread.Sleep(5000);
                }
            });
        }

        public List<DC> DC => show;
    }

    public class DC
    {
        public string region;
        public string sc;
        public string exp;
        public string ladder;
        public int progress;
        public string time;

        public DC(string r, string s, string l, int pro)
        {
            region = r;
            sc = s;
            ladder = l;
            progress = pro;
        }

        public override string ToString()
        {
            var ret = region + ladder + sc + "模式进度:" + progress.ToString() + "/6";
            if (progress == 0)
            {
                ret = "DC追踪网站又挂了，请关注群或者频道内消息";
            }
            return ret;
        }
    }

    [DataContract]
    public class DCRaw
    {
        [DataMember]
        public string progress;
        [DataMember]
        public string region;
        [DataMember]
        public string ladder;
        [DataMember]
        public string hc;
        [DataMember]
        public string timestamped;

        public DCRaw(string r, string s, string l, string pro)
        {
            region = r;
            hc = s;
            ladder = l;
            progress = pro;
        }
    }
}
