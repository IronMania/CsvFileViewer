using System;

namespace CsvFileViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            var viewer = new FileViewer(new FileProvider(args[0]), new ConsoleOutput());
            viewer.Run();
        }
    }
}
