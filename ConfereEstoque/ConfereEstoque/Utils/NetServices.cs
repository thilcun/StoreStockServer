using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConfereEstoque.Utils
{
    public class NetServices
    {
        public List<string> RelacaoIPs()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            List<string> ips = new List<string>();

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ips.Add(ip.ToString());

                }
            }
            return ips;
        }
    }
}
