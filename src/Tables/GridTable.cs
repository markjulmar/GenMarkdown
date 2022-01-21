using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using ColumnList = System.Collections.Generic.List<System.Collections.Generic.List<string[]>>;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This generates a table in Grid format compatible with MarkDig.
    /// See https://github.com/xoofx/markdig/blob/master/src/Markdig.Tests/Specs/GridTableSpecs.md
    /// </summary>
    public class GridTable : Table
    {
        /// <summary>
        /// Width for each column.
        /// </summary>
        public double[] ColumnWidths { get; private set; }

        /// <summary>
        /// Max width to generate.
        /// </summary>
        public int MaxWidth { get; set; } = 80;

        /// <summary>
        /// True if this table should render a header from the first row.
        /// </summary>
        public bool HasHeader { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hasHeader">True if this table has a header row</param>
        /// <param name="columnCount">Number of columns</param>
        public GridTable(bool hasHeader, int columnCount) : this(columnCount)
        {
            HasHeader = hasHeader;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="columnCount">Number of columns</param>
        public GridTable(int columnCount) : base(columnCount)
        {
            ColumnWidths = new double[columnCount];
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hasHeader">True if this table has a header row</param>
        /// <param name="columns">Column definitions</param>
        public GridTable(bool hasHeader, params GridColumnDefinition[] columns) : this(columns)
        {
            HasHeader = hasHeader;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="columns">Column definitions</param>
        public GridTable(params GridColumnDefinition[] columns) : base(columns.Select(c => c.Alignment).ToArray())
        {
            ColumnWidths = columns.Select(c => c.Width).ToArray();
        }

        /// <summary>
        /// Writes the block to the given TextWriter.
        /// </summary>
        /// <param name="writer">writer</param>
        /// <param name="formatting">optional formatting</param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            if (!Children.Any()) return;

            var widths = CalculateWidths();

            // Build each row -- this allows us to wrap on our max width across physical rows.
            var columns = new ColumnList();

            foreach (var row in Children)
            {
                var rowList = new List<string[]>();
                columns.Add(rowList);

                int colIndex = 0;
                for (var cellIndex = 0; cellIndex < widths.Length; cellIndex++)
                {
                    var cell = row.Count > cellIndex ? row[cellIndex] : null;

                    // Determine the width of the cell, taking into account any column spanning.
                    int width = widths.Length > colIndex ? widths[colIndex] : 0;
                    if (cell != null)
                    {
                        for (int x = 1; x < cell.ColumnSpan; x++)
                            width += widths[colIndex + x] + 1;
                        colIndex += cell.ColumnSpan;
                    }

                    var sw = new StringWriter();

                    var content = cell?.Content;
                    if (content != null)
                    {
                        content.Write(sw, formatting);
                        var block = sw.ToString().TrimEnd('\r', '\n');

                        // Now split the block.
                        var lines = SplitIntoWrappedText(block, width);
                        for (int i = 0; i < lines.Length; i++)
                            lines[i] = lines[i].Replace("|", @"&#124;");

                        rowList.Add(lines);
                    }
                    else
                    {
                        if (cell != null)
                            rowList.Add(new[] {new string(' ', width)});
                    }
                }
            }

            // Build the row separator header
            var sb = new StringBuilder();
            RenderRowSeparator(sb, widths, '-', true);

            // Now render each row.
            for (var rowIndex = 0; rowIndex < columns.Count; rowIndex++)
            {
                var row = columns[rowIndex];
                int maxLines = row.Max(c => c.Length);
                for (int index = 0; index < maxLines; index++)
                {
                    sb.Append('|');
                    for (var colIndex = 0; colIndex < row.Count; colIndex++)
                    {
                        var cells = row[colIndex];
                        int width = widths[colIndex];

                        string text = cells.Length > index ? cells[index] : new string(' ', width);
                        sb.Append(text).Append('|');
                    }
                    sb.AppendLine();
                }

                RenderRowSeparator(sb, widths, rowIndex == 0 && HasHeader ? '=' : '-', false);
            }

            sb.AppendLine();
            writer.WriteLine(sb.ToString());
        }

        /// <summary>
        /// Renders a row separator for the grid table.
        /// </summary>
        /// <param name="sb">StringBuilder to add to.</param>
        /// <param name="widths">Column widths</param>
        /// <param name="sep">Separator between columns</param>
        /// <param name="includeAlignment">True to include alignment characters.</param>
        private void RenderRowSeparator(StringBuilder sb, int[] widths, char sep, bool includeAlignment)
        {
            for (var index = 0; index < ColumnAlignments.Length; index++)
            {
                sb.Append('+');

                if (includeAlignment)
                {
                    var alignment = ColumnAlignments[index];
                    switch (alignment)
                    {
                        case ColumnAlignment.Center:
                            sb.Append(':').Append(new string(sep, widths[index]-2)).Append(':');
                            break;
                        case ColumnAlignment.Right:
                            sb.Append(new string(sep, widths[index]-1)).Append(':');
                            break;
                        default: // Left
                            sb.Append(new string(sep, widths[index]));
                            break;
                    }
                }
                else
                {
                    sb.Append(new string(sep, widths[index]));
                }
            }

            sb.AppendLine("+");
        }

        /// <summary>
        /// Splits a given content object as needed based on the max width of the cell.
        /// </summary>
        /// <param name="text">Text to split</param>
        /// <param name="width">Width to cap to</param>
        /// <returns></returns>
        private static string[] SplitIntoWrappedText(string text, int width)
        {
            if (text.Length < width)
            {
                text += new string(' ', width - text.Length);
                return new [] { text };
            }

            if (text.Length == width)
                return new [] { text };

            var lines = new List<string>();

            while (text.Length > width)
            {
                int lp = 0;
                int pos = text.IndexOf(' ');
                while (pos < width)
                {
                    lp = pos;
                    pos = text.IndexOf(' ', pos + 1);
                    if (pos == -1)
                    {
                        lp = width;
                        break;
                    }
                }

                if (lp == 0) lp = width;
                lines.Add(text[..lp]);
                text = text[lp..];
            }

            if (text.Length < width)
            {
                text += new string(' ', width - text.Length);
            }

            lines.Add(text);
            return lines.ToArray();
        }

        /// <summary>
        /// Calculate the width of each column
        /// </summary>
        /// <returns></returns>
        private int[] CalculateWidths()
        {
            int columnCount = ColumnWidths.Length;
            int[] widths = new int[columnCount];

            int childMaxWidth = Children.Max(c => c.Sum(cl => (cl?.Content?.ToString()??"").Length) + c.Count*3);
            int maxWidth = Math.Min(childMaxWidth, MaxWidth);

            for (int i = 0; i < columnCount; i++)
            {
                if (ColumnWidths[i] == 0)
                {
                    int width = Children.Max(c => c.Count > i ? (c[i]?.Content?.ToString() ?? "").Length : 0);
                    if (width > maxWidth)
                        width = maxWidth / columnCount;
                    widths[i] = width;
                }
            }

            int remainder = maxWidth - widths.Sum();
            for (int i = 0; i < columnCount; i++)
            {
                if (ColumnWidths[i] > 0)
                    widths[i] = (int) (remainder * ColumnWidths[i]);
                else if (widths[i] == 0)
                    widths[i] = remainder / columnCount + 3;
            }
            return widths;
        }
    }
}
