using System;
using System.Collections.Generic;
using System.Text;

namespace CsvFileViewer
{
    public class ConsoleOutput : IOutput
    {
        public void Write(CsvFile file)
        {
            Console.Clear();
            CreateHeader(file);
            CreateBody(file);
        }

        private void CreateBody(CsvFile file)
        {
            foreach (var line in file.ShownBody)
            {
                Console.WriteLine(CreateLine(line, file.ColumnLength));
            }
        }

        private void CreateHeader(CsvFile file)
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