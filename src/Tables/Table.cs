using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        protected readonly List<TableRow> rows = new();

        public ColumnAlignment[] ColumnAlignments { get; private set; }

        public Table(ColumnAlignment[] columns)
        {
            ColumnAlignments = columns;
        }

        public Table(int columnCount)
        {
            ColumnAlignments = new ColumnAlignment[columnCount];
        }

        public override void Write(TextWriter writer, MarkdownFormatting formatting)
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
                        var sw = new StringWriter();
                        row[i]?.Write(sw, formatting);
                        var block = sw.ToString();
                        block = block.Replace("|", @"\|");
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
