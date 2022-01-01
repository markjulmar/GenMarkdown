using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Julmar.GenMarkdown
{
    public class TableRow : List<MarkdownBlock>
    {
        public TableRow()
        {
        }

        public TableRow(int columns)
        {
            for (int i = 0; i < columns; i++)
                Add(string.Empty);            
        }
    }

    public class Table : MarkdownBlock, IList<TableRow>
    {
        private readonly List<TableRow> rows = new();

        private int Columns => rows.Max(r => r.Count);

        public override string ToString()
        {
            return rows.Any(r => r.Any(b => b is not Paragraph)) 
                ? RenderTableExtension() 
                : RenderPipeTable();
        }

        private string RenderTableExtension()
        {
            var sb = new StringBuilder();
            foreach (var row in rows)
            {
                sb.AppendLine(":::row:::");

                for (int i = 0; i < Columns; i++)
                {
                    if (row.Count > i)
                    {
                        sb.AppendLine("    :::column:::");
                        sb.Append(row[i].ToString() ?? "");
                        sb.AppendLine("    :::column-end:::");
                    }
                    else if (row.Count == i)
                    {
                        int span = Columns - row.Count;
                        sb.AppendLine($"    :::column span=\"{span}\":::");
                        sb.Append(row[i].ToString() ?? "");
                        sb.AppendLine("    :::column-end:::");
                    }
                }

                sb.AppendLine(":::row-end:::");
            }

            return sb.ToString();
        }

        private string RenderPipeTable()
        {
            var sb = new StringBuilder();
            for (var rowIndex = 0; rowIndex < rows.Count; rowIndex++)
            {
                var row = rows[rowIndex];
                sb.Append("|");
                for (int i = 0; i < Columns; i++)
                {
                    if (row.Count > i)
                    {
                        var block = row[i];
                        sb.Append((block.ToString() ?? "")
                            .TrimEnd('\r', '\n'));
                    }

                    sb.Append(" |");
                }

                sb.AppendLine();

                if (rowIndex == 0)
                {
                    sb.Append("|");
                    for (int i = 0; i < Columns; i++)
                    {
                        sb.Append("-|");
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
