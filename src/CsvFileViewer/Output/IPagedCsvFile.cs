using System.Collections.Generic;

namespace CsvFileViewer
{
    public interface IPagedCsvFile : ICsvFile
    {
        IList<int> ColumnLength { get; }
        IPagedCsvFile Last();
        IPagedCsvFile Previous();
        IPagedCsvFile Next();
        IPagedCsvFile First();
    }
}