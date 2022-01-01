using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Julmar.GenMarkdown
{
    public class BlockQuote : MarkdownBlock, IList<MarkdownBlock>
    {
        private readonly List<MarkdownBlock> children = new();

        public BlockQuote()
        {
        }

        public BlockQuote(string text)
        {
            children.Add(text);
        }

        #region List
        public IEnumerator<MarkdownBlock> GetEnumerator() => children.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) children).GetEnumerator();
        public void Add(MarkdownBlock item) => children.Add(item);
        public void Clear() => children.Clear();
        public bool Contains(MarkdownBlock item) => children.Contains(item);
        public void CopyTo(MarkdownBlock[] array, int arrayIndex) => children.CopyTo(array, arrayIndex);
        public bool Remove(MarkdownBlock item) => children.Remove(item);
        public int Count => children.Count;
        public bool IsReadOnly => false;
        public int IndexOf(MarkdownBlock item) => children.IndexOf(item);
        public void Insert(int index, MarkdownBlock item) => children.Insert(index, item);
        public void RemoveAt(int index) => children.RemoveAt(index);
        public MarkdownBlock this[int index]
        {
            get => children[index];
            set => children[index] = value;
        }
        #endregion

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in children)
            {
                sb.Append("> ");
                sb.Append(item);
            }

            sb.AppendLine();
            return sb.ToString();
        }
    }
}
