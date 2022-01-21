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

            var sb = new StringBuilder();
            for (var rowIndex = 0; rowIndex < Children.Count; rowIndex++)
            {
                var row = Children[rowIndex];
                sb.Append("|");
                for (int i = 0; i < columnCount; i++)
                {
                    if (row.Count > i)
                    {
                        var sw = new StringWriter();

                        var content = row[i]?.Content;
                        content?.Write(sw, formatting);

                        var block = sw.ToString();
                        if (string.IsNullOrEmpty(block))
                            block = " ";

                        block = block.Replace("|", @"&#124;"); // escape pipe chars in content.
                        sb.Append(block.TrimEnd('\r', '\n'));
                    }

                    sb.Append("|");
                }

                sb.AppendLine();

                if (rowIndex == 0)
                {
                    sb.Append("|");
                    for (int i = 0; i < columnCount; i++)
                    {
                        switch (ColumnAlignments[i])
                        {
                            case ColumnAlignment.Center:
                                sb.Append(":---:");
                                break;
                            case ColumnAlignment.Right:
                                sb.Append("---:");
                                break;
                            case ColumnAlignment.Left:
                            default: // left
                                sb.Append("---");
                                break;
                        }
                        sb.Append("|");
                    }
                    sb.AppendLine();
                }
            }
            writer.WriteLine(sb);
        }
    }
}
