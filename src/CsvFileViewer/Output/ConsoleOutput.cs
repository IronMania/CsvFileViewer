using System;
using System.Collections.Generic;
using System.Text;

namespace CsvFileViewer.Output
{
    public class ConsoleOutput : IOutput
    {
        private readonly Dictionary<ConsoleKey, Func<IPagedCsvFile, IPagedCsvFile>> _commands;
        private readonly int _pageSize;

        public ConsoleOutput(int pageSize)
        {
            _pageSize = pageSize;
            _commands = new Dictionary<ConsoleKey, Func<IPagedCsvFile, IPagedCsvFile>>
            {
                {ConsoleKey.N, file => file.Next()},
                {ConsoleKey.P, file => file.Previous()},
                {ConsoleKey.F, file => file.First()},
                {ConsoleKey.L, file => file.Last()},
                {ConsoleKey.J, JumpToPage},
                {ConsoleKey.X, file => null}
            };
        }

        public void Show(CsvFile file)
        {
            var csv = PagedCsvFile.Create(file, _pageSize);

            while (csv != null)
            {
                WriteToConsole(csv);
                var answer = Console.ReadKey();
                if (_commands.TryGetValue(answer.Key, out var func))
                {
                    csv = func(csv);
                }
            }
        }

        private IPagedCsvFile JumpToPage(IPagedCsvFile csv)
        {
            Console.WriteLine();
            Console.WriteLine("Which Page?");
            var input = Console.ReadLine();
            var newPage = int.Parse(input);
            return csv.JumpToPage(newPage);
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
            Console.WriteLine($"Page {file.CurrentPage} of {file.MaxPage}");
            Console.WriteLine("N(ext page, P(revious page, F(irst page, L(ast page, , J(ump to page, eX(it");
        }

        private void CreateBody(IPagedCsvFile file)
        {
            foreach (var line in file.ShownBody)
            {
                var text = CreateLine(line, file.ColumnLength);
                Console.WriteLine(text);
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