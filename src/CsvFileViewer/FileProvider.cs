using System.IO;

namespace CsvFileViewer
{
    public class FileProvider
    {
        private readonly string _fileName;

        public FileProvider(string fileName)
        {
            _fileName = fileName;
        }

        public CsvFile Read()
        {
            var lines = File.ReadAllLines(_fileName);
            return CsvFile.Create(lines);
        }
    }
}