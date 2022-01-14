using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Julmar.GenMarkdown
{
    public class Paragraph : MarkdownBlock, IList<MarkdownInline>
    {
        protected readonly List<MarkdownInline> Children = new();

        public Paragraph()
        {
        }

        public Paragraph(string text)
        {
            Children.Add(new Text(text));
        }

        #region List
        public IEnumerator<MarkdownInline> GetEnumerator() => Children.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) Children).GetEnumerator();
        public void Add(MarkdownInline item) => Children.Add(item);
        public void Clear() => Children.Clear();
        public bool Contains(MarkdownInline item) => Children.Contains(item);
        public void CopyTo(MarkdownInline[] array, int arrayIndex) => Children.CopyTo(array, arrayIndex);
        public bool Remove(MarkdownInline item) => Children.Remove(item);
        public int Count => Children.Count;
        public bool IsReadOnly => false;
        public int IndexOf(MarkdownInline item) => Children.IndexOf(item);
        public void Insert(int index, MarkdownInline item) => Children.Insert(index, item);
        public void RemoveAt(int index) => Children.RemoveAt(index);
        public MarkdownInline this[int index]
        {
            get => Children[index];
            set => Children[index] = value;
        }
        #endregion

        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            var sw = new StringWriter();
            foreach (var child in Children)
                child.Write(sw, formatting);

            writer.Write(Indent);
            writer.WriteLine(sw.ToString().TrimEnd('\r','\n').Replace("\n", "\n"+Indent));
            writer.WriteLine();
        }
    }
}