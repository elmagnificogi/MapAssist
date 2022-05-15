using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nautilus;

namespace MapAssist.Helpers
{
    public static class IPBlock
    {
        private static IFirewallRuleService service = null;
        private static List<string> block_ip_ranges = new List<string>();
        private static List<string> block_ip_address = new List<string>();

        private static void SetServer(string server)
        {
            block_ip_address.Clear();
            block_ip_ranges.Clear();
            if (server.Contains("香港"))
            {
                block_ip_ranges.Add("34.150.1.1 - 34.150.255.255");
                block_ip_ranges.Add("34.92.1.1 - 34.92.255.255");
                block_ip_ranges.Add("34.96.1.1 - 34.96.255.255");
                block_ip_ranges.Add("35.220.1.1 - 35.220.255.255");
                block_ip_ranges.Add("35.241.1.1 - 35.241.255.255");
            }

            if (server.Contains("台湾"))
            {
                block_ip_ranges.Add("34.80.1.1 - 34.80.255.255");
                block_ip_ranges.Add("34.81.1.1 - 34.81.255.255");
                block_ip_ranges.Add("34.185.1.1 - 34.185.255.255");
                block_ip_ranges.Add("34.187.1.1 - 34.187.255.255");
                block_ip_ranges.Add("35.189.1.1 - 35.189.255.255");
                block_ip_ranges.Add("35.194.1.1 - 35.194.255.255");
                block_ip_ranges.Add("35.201.1.1 - 35.201.255.255");
                block_ip_ranges.Add("35.221.1.1 - 35.221.255.255");
                block_ip_ranges.Add("35.229.1.1 - 35.229.255.255");
                block_ip_ranges.Add("35.234.1.1 - 35.234.255.255");
                block_ip_ranges.Add("35.236.1.1 - 35.236.255.255");
            }

            if (server.Contains("日本"))
            {
                block_ip_ranges.Add("34.97.1.1 - 34.97.255.255");
                block_ip_ranges.Add("34.146.1.1 - 34.146.255.255");
                block_ip_ranges.Add("34.84.1.1 - 34.84.255.255");
                block_ip_ranges.Add("34.85.1.1 - 34.85.255.255");
                block_ip_ranges.Add("35.187.1.1 - 35.187.255.255");
                block_ip_ranges.Add("35.194.1.1 - 35.194.255.255");
                block_ip_ranges.Add("35.200.1.1 - 35.200.255.255");
                block_ip_ranges.Add("35.221.1.1 - 35.221.255.255");
                block_ip_ranges.Add("35.243.1.1 - 35.243.255.255");
                block_ip_ranges.Add("35.189.1.1 - 35.189.255.255");
            }

            if (server.Contains("韩国"))
            {
                block_ip_ranges.Add("34.64.1.1 - 34.64.255.255");
            }

            if (server.Contains("印度"))
            {
                block_ip_ranges.Add("35.200.1.1 - 35.200.255.255");
                block_ip_ranges.Add("35.244.1.1 - 35.244.255.255");
                block_ip_ranges.Add("34.93.1.1 - 34.93.255.255");
            }

            if (server.Contains("印尼"))
            {
                block_ip_ranges.Add("34.101.1.1 - 34.101.255.255");
            }
        }

        public static bool StartIPBlock(string server)
        {
            try
            {
                service = Firewall.GetRuleService("elmagnifico_map_ip_block");

                // creates a new firewall rule.
                var newRule = service.CreateRule();
                newRule.Enabled = true;
                newRule.Direction = Directions.Outgoing;
                newRule.Protocol = ProtocolTypes.TCP;
                newRule.Action = Actions.Block;

                SetServer(server);

                foreach (var ip in block_ip_address)
                    newRule.RemoteAddresses.Add(IPAddress.Parse(ip));

                foreach (var ip_range in block_ip_ranges)
                    newRule.RemoteAddresses.Add(IPAddressRange.Parse(ip_range));

                // commits all changes.
                service.UpdateRule(newRule);


            }
            catch
            {
                MessageBox.Show("IP屏蔽出错，请使用管理员权限或者开启防火墙以后再尝试");
                return false;
            }
            return true;
        }

        public static bool StopIPBlock()
        {
            try
            {
                // removes all firewall rules.
                if (service == null)
                    service = Firewall.GetRuleService("elmagnifico_map_ip_block");
                service.DropRules();
            }
            catch
            {
                return false;
            }
            return true;
        }


    }
}
