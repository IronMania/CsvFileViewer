using System;
using System.Collections.Generic;
using System.Linq;

namespace CsvFileViewer
{
    public class CsvFile
    {
        private const string Separator = ";";
        private const int PageSize= 3;

        private readonly IList<IList<string>> _body;
        private readonly int _currentPage;

        private CsvFile(IList<string> header, IList<IList<string>> completeBody, int page = 0)
        {
            Header = header;
            _body = completeBody;
            ShownBody = _body.Skip(page).Take(PageSize);
            ColumnLength = CreateColumnLength(header, ShownBody);
            _currentPage = page;
        }

        public IList<string> Header { get; private set; }
        public IEnumerable<IList<string>> ShownBody { get; private set; }
        public IList<int> ColumnLength { get; private set; }

        public CsvFile Next()
        {
            return new CsvFile(Header, _body, _currentPage+1);
        }

        public CsvFile Previous()
        {
            return new CsvFile(Header, _body, _currentPage-1);
        }

        public CsvFile First()
        {
            return new CsvFile(Header, _body);
        }

        public CsvFile Last()
        {
            return new CsvFile(Header, _body,_body.Count / PageSize);
        }

        public static CsvFile Create(IList<string> lines)
        {
            var header = lines.First().Split(Separator);
            var body = lines.Skip(1).Select(s => (IList<string>) s.Split(Separator).ToList()).ToList();

            return new CsvFile(header, body);
        }

        private static IList<int> CreateColumnLength(IEnumerable<string> header, IEnumerable<IList<string>> body)
        {
            var tmp = header.Select(s => s.Length).ToList();

            return body.Aggregate(tmp,
                (current, bodyline) => current.Zip(bodyline.Select(s => s.Length), Math.Max).ToList());
        }
    }
}