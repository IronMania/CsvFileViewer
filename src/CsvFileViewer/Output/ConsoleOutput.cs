using System;
using System.Collections.Generic;
using System.Text;

namespace CsvFileViewer.Output
{
    public class ConsoleOutput : IOutput
    {
        private readonly int _pageSize;

        public ConsoleOutput(int pageSize)
        {
            _pageSize = pageSize;
        }
        public void Show(CsvFile file)
        {
            IPagedCsvFile csv = PagedCsvFile.Create(file,_pageSize);
            
            while (true)
            {
                WriteToConsole(csv);
                var answer = Console.ReadKey();
                switch (answer.Key)
                {
                    case ConsoleKey.N:
                        csv = csv.Next();
                        break;
                    case ConsoleKey.P:
                        csv = csv.Previous();
                        break;
                    case ConsoleKey.F:
                        csv = csv.First();
                        break;
                    case ConsoleKey.L:
                        csv = csv.Last();
                        break;
                    case ConsoleKey.X:
                        return;
                    default:
                        break;
                }
            }
            
        }

        private void WriteToConsole(IPagedCsvFile csv)
        {
            Console.Clear();
            CreateHeader(csv);
            CreateBody(csv);
            CreateCommands(csv);
        }

        private void CreateCommands(IPagedCsvFile file)
        {
            Console.WriteLine("N(ext page, P(revious page, F(irst page, L(ast page, eX(it");

        }

        private void CreateBody(IPagedCsvFile file)
        {
            foreach (var line in file.ShownBody)
            {
                Console.WriteLine(CreateLine(line, file.ColumnLength));
            }
        }

        private void CreateHeader(IPagedCsvFile file)
        {
            var belowHeaderBuilder = new StringBuilder();
            for (var index = 0; index < file.Header.Count; index++)
            {
                belowHeaderBuilder.Append(new string('-', file.ColumnLength[index]));
                belowHeaderBuilder.Append('+');
            }

            Console.WriteLine(CreateLine(file.Header, file.ColumnLength));
            Console.WriteLine(belowHeaderBuilder);
        }

        private string CreateLine(IList<string> lineText, IList<int> columnLength)
        {
            var builder = new StringBuilder();
            for (var index = 0; index < lineText.Count; index++)
            {
                var columnText = lineText[index];
                var textLength = columnLength[index];
                builder.Append($"{columnText}");
                builder.Append(new string(' ', textLength - columnText.Length));
                builder.Append("|");
            }

            return builder.ToString();
        }
    }
}