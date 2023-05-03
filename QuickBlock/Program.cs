// See https://aka.ms/new-console-template for more information
namespace QuickBlock
{
    class Program
    {
        public static void Main(string[] args)
        {
            var hostFile = new HostFile();
            Console.WriteLine("Blocked Urls:");
            foreach(var url in hostFile.BlockedUrls())
            {
                Console.WriteLine(url);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
