// See https://aka.ms/new-console-template for more information
using System.Xml.Xsl;

namespace QuickBlock
{
    class Program
    {
        public static void Main(string[] args)
        {
            var hostFile = new HostFile();
            if (args.Length == 0)
            {
                Console.WriteLine("Blocked Urls: ");
                hostFile.BlockedUrls().ForEach(url => Console.WriteLine(url));
            } else if (args.Length != 2) 
            {
                Program.PrintUsage();
            } else
            {
                var command = args[0];
                var url = args[1];

                if (command == "block")
                {
                    hostFile.Block(url);
                    hostFile.Save();
                } else if (command == "unblock")
                {
                    hostFile.UnBlock(url);
                    hostFile.Save();
                } else
                {
                    Program.PrintUsage();
                }
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Quick Block v.0.0.1");
            Console.WriteLine("Easily update hosts file.");
            Console.WriteLine("Usage:");
            Console.WriteLine("quickblock [(block url) | (unblock url)]?");
        }
    }
}
