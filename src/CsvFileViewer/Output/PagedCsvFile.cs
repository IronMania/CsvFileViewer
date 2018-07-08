using System;
using System.Collections.Generic;
using System.Linq;

namespace CsvFileViewer.Output
{
    public class PagedCsvFile : IPagedCsvFile
    {
        private readonly CsvFile _csvFile;
        private readonly int _page;
        private readonly int _pageSize;

        private PagedCsvFile(CsvFile csvFile, int pageSize, int page = 0)
        {
            _csvFile = csvFile;
            _pageSize = pageSize;
            if (page <= 0)
            {
                _page = 0;
            }
            else if (page > LastPageIndex())
            {
                _page = LastPageIndex();
            }
            else
            {
                _page = page;
            }
        }

        public int CurrentPage => _page + 1;
        public int MaxPage => LastPageIndex() + 1;

        public IList<int> ColumnLength => CreateColumnLength(Header, ShownBody);
        public IList<string> Header => _csvFile.Header;
        public IEnumerable<IList<string>> ShownBody => _csvFile.ShownBody.Skip(_page * _pageSize).Take(_pageSize);

        public IPagedCsvFile Next()
        {
            return Create(_csvFile, _pageSize, _page + 1);
        }

        public IPagedCsvFile Previous()
        {
            return Create(_csvFile, _pageSize, _page - 1);
        }

        public IPagedCsvFile First()
        {
            return Create(_csvFile, _pageSize);
        }


        public IPagedCsvFile Last()
        {
            return Create(_csvFile, _pageSize, LastPageIndex());
        }

        private int LastPageIndex()
        {
            return _csvFile.ShownBody.Count() / _pageSize;
        }

        public static IPagedCsvFile Create(CsvFile file, int pageSize, int page = 0)
        {
            return new PagedCsvFile(file, pageSize, page);
        }

        private static IList<int> CreateColumnLength(IEnumerable<string> header, IEnumerable<IList<string>> body)
        {
            var tmp = header.Select(s => s.Length).ToList();

            return body.Aggregate(tmp,
                (current, bodyline) => current.Zip(bodyline.Select(s => s.Length), Math.Max).ToList());
        }
    }
}