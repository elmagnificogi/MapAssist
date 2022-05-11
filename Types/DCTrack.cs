using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using System.Diagnostics;
using MapAssist.Settings;
using System.Threading;
using MapAssist.Helpers;

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
                            DCProgress_Refresh();
                            show.Clear();
                            var warningLevel = int.Parse("" + (MapAssistConfiguration.Loaded.DCTrack.WarningLevel.Trim())[0]);

                            //WindowsExternal.Beep(800, 300);
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
                                if(show.Count > 0)
                                {
                                    var beep = false;
                                    foreach(var dc in show)
                                    {
                                        if(dc.progress >= warningLevel)
                                            beep = true;
                                    }
                                    if(beep)
                                        WindowsExternal.Beep(800, 300);
                                }
                                    
                            }
                                

                        }
                    }
                    catch (Exception ex)
                    {
                        //Debug.WriteLine(ex);
                    }
                    
                    Thread.Sleep(5000);
                }
            });
        }

        private string GetWebClient(string url)
        {
            var strHTML = "";
            var myWebClient = new WebClient();
            myWebClient.Headers.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 6.0; " +
                                  "Windows NT 6.1; .NET CLR 1.0.3705;)");
            myWebClient.Proxy = null;
            Stream myStream = myWebClient.OpenRead(url);
            var sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8"));
            strHTML = sr.ReadToEnd();
            myStream.Close();
            return strHTML;
        }

        private void DCProgress_Refresh()
        {

            var doc = new HtmlAgilityPack.HtmlDocument();
            var html = GetWebClient("https://diablo2.io/dclonetracker.php");

            var dcp = new List<DC>();
            doc.LoadHtml(html);

            for (var i = 0; i < 12; i++)
            {
                var index = i + 1;

                int pro;
                string server;
                string ladder;
                string sc;
                string time;
                HtmlNode node = doc.DocumentNode.SelectSingleNode("//*[@id=\"memberlist\"]/tbody/tr[" + index.ToString() + "]/td[1]/span[1]/code");
                Debug.WriteLine(node.InnerText);  //输出节点内容     
                pro = int.Parse(""+node.InnerText.Trim()[0]);

                HtmlNode node2 = doc.DocumentNode.SelectSingleNode("//*[@id=\"memberlist\"]/tbody/tr[" + index.ToString() + "]/td[2]/span[1]/span");
                Debug.WriteLine(node2.InnerText);  //输出节点内容    
                if (node2.InnerText.Contains("Americas"))
                    server = "美服";
                else if (node2.InnerText.Contains("Europe"))
                    server = "欧服";
                else if (node2.InnerText.Contains("Asia"))
                    server = "亚服";
                else
                    server = "错误";

                HtmlNode node3 = doc.DocumentNode.SelectSingleNode("//*[@id=\"memberlist\"]/tbody/tr[" + index.ToString() + "]/td[3]/span[1]/span");
                Debug.WriteLine(node3.InnerText);  //输出节点内容    
                if (node3.InnerText.Contains("Non-Ladder"))
                    ladder = "经典";
                else if (node3.InnerText.Contains("Ladder"))
                    ladder = "天梯";
                else
                    ladder = "错误";

                HtmlNode node4 = doc.DocumentNode.SelectSingleNode("//*[@id=\"memberlist\"]/tbody/tr[" + index.ToString() + "]/td[4]/span[1]/span");
                Debug.WriteLine(node4.InnerText);  //输出节点内容    
                if (node4.InnerText.Contains("Softcore"))
                    sc = "普通";
                else if (node4.InnerText.Contains("Hardcore"))
                    sc = "专家";
                else
                    sc = "错误";

                HtmlNode node5 = doc.DocumentNode.SelectSingleNode("//*[@id=\"memberlist\"]/tbody/tr[" + index.ToString() + "]/td[5]/span[1]/span");
                Debug.WriteLine(node5.InnerText);  //输出节点内容    
                time = node5.InnerText;
                time = time.Replace("days", "天");
                time = time.Replace("day", "天");
                time = time.Replace("week", "周");
                time = time.Replace("weeks", "周");
                time = time.Replace("minutes", "分钟");
                time = time.Replace("minute", "分钟");
                time = time.Replace("hours", "小时");
                time = time.Replace("hour", "小时");
                time = time.Replace("ago", "前");
                time = time.Replace(" ", "");
                time = time.Trim();

                dcp.Add(new DC(server, sc, ladder, pro));
            }
            diabloCloneProgress = dcp;
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
            return region+ladder+sc+"模式进度:"+progress.ToString()+"/6";
        }
    }
}
