using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuickBlock
{
    public class HostFile
    {
        private List<string> blockedUrls;
        public HostFile()
        {
            blockedUrls = new List<string>();
            string[] lines = File.ReadAllLines(@"C:\Windows\System32\drivers\etc\hosts");
            using (var sr = new StreamReader(@"C:\Windows\System32\drivers\etc\hosts"))
            {
                var line = sr.ReadLine();
                while (line != null) {
                    if (line == "# Added by QuickBlock")
                    {
                        this.getBlockedUrls(sr);
                    }
                    line = sr.ReadLine();
                }
            }
        }

        public List<String> BlockedUrls()
        {
            return blockedUrls;
        }
        public void Block(string url)
        {
            blockedUrls.Add(url);
        }

        public void UnBlock(string url)
        {
            blockedUrls.Remove(url);
        }

        public void Save()
        {

        }
        private List<string> getBlockedUrls(StreamReader sr)
        {
            var line = sr.ReadLine();
            while (line != null && line != "# End Section")
            {
                blockedUrls.Add(line.Split()[1]);
                line = sr.ReadLine();
            }
            return blockedUrls;
        }
    }
}
