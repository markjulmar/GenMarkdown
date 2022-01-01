using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Julmar.GenMarkdown
{
    public enum ColumnAlignment
    {
        Default = 0,
        Left = 0,
        Center,
        Right
    }

    public class Table : MarkdownBlock, IList<TableRow>
    {
        private readonly List<TableRow> rows = new();

        public ColumnAlignment[] ColumnAlignments { get; private set; }

        public TableTypes Type { get; set; }

        public Table(TableTypes type, ColumnAlignment[] columns)
        {
            Type = type;
            ColumnAlignments = columns;
        }

        public Table(TableTypes type, int columnCount)
        {
            Type = type;
            ColumnAlignments = new ColumnAlignment[columnCount];
        }

        public Table(int columns) : this(TableTypes.Standard, columns)
        {
        }

        public Table(ColumnAlignment[] columns) : this(TableTypes.Standard, columns)
        {
        }

        public override string ToString()
        {
            return Type switch
            {
                TableTypes.RowExtension => RenderTableExtension(),
                TableTypes.Standard => RenderPipeTable(),
                _ => RenderPipeTable()
            };
        }

        private string RenderTableExtension()
        {
            int columnCount = ColumnAlignments.Length;

            var sb = new StringBuilder();
            foreach (var row in rows)
            {
                sb.AppendLine(":::row:::");

                for (int colIndex = 0; colIndex < columnCount; colIndex++)
                {
                    var cell = row.Cells[colIndex];

                    if (cell.ColumnSpan == 1)
                    {
                        sb.AppendLine("    :::column:::");
                        sb.Append(cell.Content ?? "");
                        sb.AppendLine("    :::column-end:::");
                    }
                    else
                    {
                        sb.AppendLine($"    :::column span=\"{cell.ColumnSpan}\":::");
                        sb.Append(cell.Content ?? "");
                        sb.AppendLine("    :::column-end:::");
                        colIndex += cell.ColumnSpan - 1;
                    }
                }

                sb.AppendLine(":::row-end:::");
            }

            return sb.ToString();
        }

        private string RenderPipeTable()
        {
            bool requiresExtension = rows.Any(r => r.Cells.Any(tc => tc.ColumnSpan>1));
            if (requiresExtension)
                throw new ArgumentException("Cannot render column-spanned content with pipe tables.");

            int columnCount = ColumnAlignments.Length;

            var sb = new StringBuilder();
            for (var rowIndex = 0; rowIndex < rows.Count; rowIndex++)
            {
                var row = rows[rowIndex];
                sb.Append("|");
                for (int i = 0; i < columnCount; i++)
                {
                    if (row.Count > i)
                    {
                        var block = row[i]?.ToString()??"";
                        sb.Append(block.TrimEnd('\r', '\n'));
                    }

                    sb.Append(" |");
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
                                sb.Append(":-:");
                                break;
                            case ColumnAlignment.Right:
                                sb.Append("-:");
                                break;
                            default: // left
                                sb.Append("-");
                                break;
                        }

                        sb.Append("|");
                    }
                    sb.AppendLine();
                }

            }

            return sb.ToString();
        }

        #region Rows
        public IEnumerator<TableRow> GetEnumerator() => rows.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) rows).GetEnumerator();
        public void Add(TableRow item) => rows.Add(item);
        public void Clear() => rows.Clear();
        public bool Contains(TableRow item) => rows.Contains(item);
        public void CopyTo(TableRow[] array, int arrayIndex) => rows.CopyTo(array, arrayIndex);
        public bool Remove(TableRow item) => rows.Remove(item);
        public int Count => rows.Count;
        public bool IsReadOnly => false;
        public int IndexOf(TableRow item) => rows.IndexOf(item);
        public void Insert(int index, TableRow item) => rows.Insert(index, item);
        public void RemoveAt(int index) => rows.RemoveAt(index);
        public TableRow this[int index]
        {
            get => rows[index];
            set => rows[index] = value;
        }
        #endregion
    }
}
