using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This renders a Markdown pipe table
    /// </summary>
    public class Table : MarkdownBlockCollection<TableRow>
    {
        private const char PipeSeparator = '|';

        /// <summary>
        /// Column alignments
        /// </summary>
        public ColumnAlignment[] ColumnAlignments { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="columns">Column alignments</param>
        public Table(params ColumnAlignment[] columns)
        {
            ColumnAlignments = columns;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="columnCount"># of columns, all left-aligned</param>
        public Table(int columnCount)
        {
            ColumnAlignments = new ColumnAlignment[columnCount];
        }

        /// <summary>
        /// Writes the block to the given TextWriter.
        /// </summary>
        /// <param name="writer">writer</param>
        /// <param name="formatting">optional formatting</param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            if (!Children.Any()) return;

            // Check for row or column spans.
            if (Children.Any(r => r.Any(c => c?.ColumnSpan > 1)))
                throw new InvalidOperationException("Cannot render spanned rows/columns with pipe table.");

            int columnCount = ColumnAlignments.Length;

            int[] widths = Children.Select(r => r.Max(c => Math.Min(30, c?.Content?.ToString().Length ?? 0) + 2)).ToArray();

            var sb = new StringBuilder();
            for (var rowIndex = 0; rowIndex < Children.Count; rowIndex++)
            {
                var row = Children[rowIndex];
                sb.Append(PipeSeparator);
                for (int i = 0; i < columnCount; i++)
                {
                    if (row.Count > i)
                    {
                        var sw = new StringWriter();

                        var content = row[i]?.Content;
                        content?.Write(sw, formatting);

                        var block = sw.ToString().Replace("|", @"&#124;").TrimEnd('\r', '\n');
                        if (string.IsNullOrEmpty(block))
                            block = " ";

                        if (block[0] != ' ') block = ' ' + block;
                        if (block.Length > 1 && block[^1] != ' ') block += ' ';

                        if (formatting?.PrettyPipeTables == true)
                        {
                            block = block.PadRight(widths[i]);
                        }

                        sb.Append(block);
                    }

                    sb.Append(PipeSeparator);
                }

                sb.AppendLine();

                if (rowIndex == 0)
                {
                    sb.Append(PipeSeparator);
                    for (int i = 0; i < columnCount; i++)
                    {
                        switch (ColumnAlignments[i])
                        {
                            case ColumnAlignment.Center:
                                sb.Append($":{new string('-', formatting?.PrettyPipeTables == true ? widths[i]-2 : 3)}:");
                                break;
                            case ColumnAlignment.Right:
                                sb.Append($"{new string('-', formatting?.PrettyPipeTables == true ? widths[i]-1 : 3)}:");
                                break;
                            case ColumnAlignment.Left:
                                sb.Append($":{new string('-', formatting?.PrettyPipeTables == true ? widths[i-1] : 3)}");
                                break;
                            default: // unspecified
                                sb.Append(new string('-', formatting?.PrettyPipeTables == true ? widths[i] : 3));
                                break;
                        }
                        sb.Append(PipeSeparator);
                    }
                    sb.AppendLine();
                }
            }
            writer.WriteLine(sb);
        }
    }
}
