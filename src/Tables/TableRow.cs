using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Julmar.GenMarkdown
{
    public class TableRow : IList<MarkdownBlock>
    {
        private readonly List<TableCell> cells = new();

        public TableRow()
        {
        }

        public IList<TableCell> Cells => cells;

        public TableRow(int columns)
        {
            for (int i = 0; i < columns; i++)
                cells.Add(new TableCell());            
        }

        public void Add(TableCell cell) => cells.Add(cell);

        public IEnumerator<MarkdownBlock> GetEnumerator() => cells.Select(c => c.Content).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public void Add(MarkdownBlock item) => cells.Add(new TableCell(item));
        public void Clear() => cells.Clear();
        public bool Contains(MarkdownBlock item) => cells.Any(c => c.Content == item);

        public void CopyTo(MarkdownBlock[] array, int arrayIndex) => 
            cells.Select(c => c.Content).ToArray().CopyTo(array, arrayIndex);

        public bool Remove(MarkdownBlock item)
        {
            int pos = IndexOf(item);
            if (pos >= 0)
            {
                cells.RemoveAt(pos);
                return true;
            }

            return false;
        }

        public int Count => cells.Count;
        public bool IsReadOnly => false;
        public int IndexOf(MarkdownBlock item)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].Content == item)
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, MarkdownBlock item) => cells.Insert(index, new TableCell(item));
        public void RemoveAt(int index) => cells.RemoveAt(index);

        public MarkdownBlock this[int index]
        {
            get => cells[index].Content;
            set => cells[index].Content = value;
        }
    }
}