using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Julmar.GenMarkdown
{
    public class Definition : MarkdownBlock, ICollection<string>
    {
        protected readonly List<string> Children = new();

        public string Term { get; }

        public Definition(string term)
        {
            Term = term ?? throw new ArgumentNullException(nameof(term));
        }

        public Definition(string term, string definition)
        {
            if (definition == null) throw new ArgumentNullException(nameof(definition));
            Term = term ?? throw new ArgumentNullException(nameof(term));
            Children.Add(definition);
        }

        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            writer.Write(Indent);
            writer.WriteLine(Term.Trim());
            foreach (var child in Children)
            {
                writer.Write(Indent);
                writer.WriteLine($": {child.TrimEnd('\r','\n')}");
            }
        }

        #region Collection
        public IEnumerator<string> GetEnumerator() => Children.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) Children).GetEnumerator();
        public void Add(string item) => Children.Add(item);
        public void Clear() => Children.Clear();
        public bool Contains(string item) => Children.Contains(item);
        public void CopyTo(string[] array, int arrayIndex) => Children.CopyTo(array, arrayIndex);
        public bool Remove(string item) => Children.Remove(item);
        public int Count => Children.Count;
        public bool IsReadOnly => false;
        #endregion
    }
}