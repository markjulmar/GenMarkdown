using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Julmar.GenMarkdown
{
    public class Paragraph : MarkdownBlock, IList<MarkdownInline>
    {
        private readonly List<MarkdownInline> children = new();

        public Paragraph()
        {
        }

        public Paragraph(string text)
        {
            children.Add(new Text(text));
        }

        #region List
        public IEnumerator<MarkdownInline> GetEnumerator() => children.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) children).GetEnumerator();
        public void Add(MarkdownInline item) => children.Add(item);
        public void Clear() => children.Clear();
        public bool Contains(MarkdownInline item) => children.Contains(item);
        public void CopyTo(MarkdownInline[] array, int arrayIndex) => children.CopyTo(array, arrayIndex);
        public bool Remove(MarkdownInline item) => children.Remove(item);
        public int Count => children.Count;
        public bool IsReadOnly => false;
        public int IndexOf(MarkdownInline item) => children.IndexOf(item);
        public void Insert(int index, MarkdownInline item) => children.Insert(index, item);
        public void RemoveAt(int index) => children.RemoveAt(index);
        public MarkdownInline this[int index]
        {
            get => children[index];
            set => children[index] = value;
        }
        #endregion

        public override string ToString()
        {
            var sb = new StringBuilder();
            
            foreach (var child in children)
            {
                sb.Append(child);
            }
            
            sb.AppendLine();

            return sb.ToString();
        }
    }
}