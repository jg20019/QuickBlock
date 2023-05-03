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
        private string hostsFile = @"C:\Windows\System32\drivers\etc\hosts";
        private string sectionHeader = "# Added by QuickBlock";
        private string sectionFooter = "# End Section";
        public HostFile()
        {
            blockedUrls = new List<string>();
            using (var sr = new StreamReader(hostsFile))
            {
                var line = sr.ReadLine();
                while (line != null) {
                    if (line == sectionHeader)
                    {
                        this.GetBlockedUrls(sr);
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
            List<string> contentsWithoutQuickBlockSection = new List<string>();
            using (var sr = new StreamReader(hostsFile))
            {
                var line = sr.ReadLine();
                while (line != null)
                {
                    if (line == sectionHeader)
                    {
                        line = this.SkipQuickBlockSection(sr);
                    } else
                    {
                       contentsWithoutQuickBlockSection.Add(line);
                        line = sr.ReadLine();
                    }
                }
            }
            using (var sw = new StreamWriter(hostsFile))
            {
                contentsWithoutQuickBlockSection.ForEach(x => { sw.WriteLine(x); });
                sw.WriteLine(sectionHeader);
                foreach(var url in blockedUrls)
                {
                    sw.WriteLine($"127.0.0.1 {url}");
                }
                sw.WriteLine(sectionFooter);
            }
        }
        private List<string> GetBlockedUrls(StreamReader sr)
        {
            var line = sr.ReadLine();
            while (line != null && line != sectionFooter)
            {
                blockedUrls.Add(line.Split()[1]);
                line = sr.ReadLine();
            }
            return blockedUrls;
        }

        private string? SkipQuickBlockSection(StreamReader sr)
        {
            var line = sr.ReadLine();
            while (line != null && line != sectionFooter)
            {
                line = sr.ReadLine();
            }
            return sr.ReadLine();
        }
    }
}
