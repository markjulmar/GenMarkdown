using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Julmar.GenMarkdown
{
    public class DefinitionList : MarkdownBlock, ICollection<Definition>
    {
        protected readonly List<Definition> Children = new();

        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            foreach (var child in Children)
            {
                child.IndentLevel = this.IndentLevel;
                child.Write(writer, formatting);
                writer.WriteLine();
            }
        }

        #region Collection
        public IEnumerator<Definition> GetEnumerator() => Children.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Children).GetEnumerator();
        public void Add(Definition item) => Children.Add(item);
        public void Clear() => Children.Clear();
        public bool Contains(Definition item) => Children.Contains(item);
        public void CopyTo(Definition[] array, int arrayIndex) => Children.CopyTo(array, arrayIndex);
        public bool Remove(Definition item) => Children.Remove(item);
        public int Count => Children.Count;
        public bool IsReadOnly => false;
        #endregion
    }
}
