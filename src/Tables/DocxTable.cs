using System.IO;
using System.Text;

namespace Julmar.GenMarkdown
{
    public class DocxTable : Table
    {
        public DocxTable(int columns) : base(columns)
        {
        }

        public DocxTable(ColumnAlignment[] columns) : base(columns)
        {
        }

        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            int columnCount = ColumnAlignments.Length;

            var sb = new StringBuilder();
            foreach (var row in rows)
            {
                sb.AppendLine(":::row:::");

                for (int colIndex = 0; colIndex < columnCount; colIndex++)
                {
                    var cell = row.Cells[colIndex];
                    var sw = new StringWriter();
                    cell.Content.Write(sw, formatting);

                    if (cell.ColumnSpan == 1)
                    {
                        sb.AppendLine("    :::column:::");
                        sb.Append(sw);
                        sb.AppendLine("    :::column-end:::");
                    }
                    else
                    {
                        sb.AppendLine($"    :::column span=\"{cell.ColumnSpan}\":::");
                        sb.Append(sw);
                        sb.AppendLine("    :::column-end:::");
                        colIndex += cell.ColumnSpan - 1;
                    }
                }

                sb.AppendLine(":::row-end:::");
            }

            writer.WriteLine(sb);
        }
    }
}