using System;

namespace CsvFileViewer
{
    public class FileViewer
    {
        private readonly FileProvider _fileProvider;
        private readonly IOutput _output;

        public FileViewer(FileProvider fileProvider, IOutput output)
        {
            _fileProvider = fileProvider;
            _output = output;
        }


        public void Run()
        {
            var csv = _fileProvider.Read();
            _output.Write(csv);
            //csv.SelectPage(0);
            //write available Commands
            //WaitForCommands
            //TODO: Remove
            Console.ReadKey();
        }
    }
}