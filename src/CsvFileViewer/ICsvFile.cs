using System.Collections.Generic;

namespace CsvFileViewer
{
    public interface ICsvFile
    {
        IList<string> Header { get; }
        IEnumerable<IList<string>> ShownBody { get; }
    }
}