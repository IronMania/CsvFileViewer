using CsvFileViewer.Output;

namespace CsvFileViewer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length<2 || !int.TryParse(args[1], out var lineLength))
            {
                lineLength = 3;
            }

            var viewer = new FileViewer(new FileProvider(args[0]), new ConsoleOutput(lineLength));
            viewer.Run();
        }
    }
}