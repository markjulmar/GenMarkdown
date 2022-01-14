using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Julmar.GenMarkdown
{
    public class BlockQuote : MarkdownBlock, IList<MarkdownBlock>
    {
        protected readonly List<MarkdownBlock> Children = new();

        public BlockQuote()
        {
        }

        public BlockQuote(string text)
        {
            Children.Add(text);
        }

        #region List
        public IEnumerator<MarkdownBlock> GetEnumerator() => Children.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) Children).GetEnumerator();
        public void Add(MarkdownBlock item) => Children.Add(item);
        public void Clear() => Children.Clear();
        public bool Contains(MarkdownBlock item) => Children.Contains(item);
        public void CopyTo(MarkdownBlock[] array, int arrayIndex) => Children.CopyTo(array, arrayIndex);
        public bool Remove(MarkdownBlock item) => Children.Remove(item);
        public int Count => Children.Count;
        public bool IsReadOnly => false;
        public int IndexOf(MarkdownBlock item) => Children.IndexOf(item);
        public void Insert(int index, MarkdownBlock item) => Children.Insert(index, item);
        public void RemoveAt(int index) => Children.RemoveAt(index);
        public MarkdownBlock this[int index]
        {
            get => Children[index];
            set => Children[index] = value;
        }
        #endregion

        /// <summary>
        /// Writes the block to the given TextWriter.
        /// </summary>
        /// <param name="writer">writer</param>
        /// <param name="formatting">optional formatting</param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            string indent = Indent;
            var sb = new StringBuilder();

            foreach (var item in Children)
            {
                var sw = new StringWriter();
                item.Write(sw, formatting);

                sb.Append(indent + "> ")
                    .AppendLine(sw.ToString()
                        .TrimEnd('\r', '\n')
                        .Replace("\n", "\n" + indent + "> "));
            }
            writer.Write(sb.AppendLine().ToString());
        }
    }
}
