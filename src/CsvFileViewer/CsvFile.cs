using System.Collections.Generic;
using System.Linq;

namespace CsvFileViewer
{
    public class CsvFile : ICsvFile
    {
        private const string Separator = ";";

        private readonly IList<IList<string>> _body;
        private readonly int _currentPage;

        private CsvFile(IList<string> header, IList<IList<string>> completeBody, int page = 0)
        {
            Header = header;
            _body = completeBody;
            _currentPage = page;
        }

        public IList<string> Header { get; private set; }
        public IEnumerable<IList<string>> ShownBody => _body;


        public static CsvFile Create(IList<string> lines)
        {
            var header = lines.First().Split(Separator).ToList();
            header.Insert(0,"No.");
            int lineIndex = 1;
            var body = lines.Skip(1).Select(s =>
            {
                IList<string> line = s.Split(Separator).ToList();
                line.Insert(0,lineIndex.ToString());
                lineIndex++;
                return line ;
            }).ToList();

            return new CsvFile(header, body);
        }
    }
}