using System.Collections.Generic;

namespace CsvFileViewer.Output
{
    public interface IPagedCsvFile : ICsvFile
    {
        IList<int> ColumnLength { get; }
        int CurrentPage { get; }
        int MaxPage { get; }
        IPagedCsvFile Last();
        IPagedCsvFile Previous();
        IPagedCsvFile Next();
        IPagedCsvFile First();
        IPagedCsvFile JumpToPage(int newPage);
    }
}