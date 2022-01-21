using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This is the basis for any list in Markdown capable of holding multiple
    /// blocks within each item.
    ///
    /// The MarkdownList is comprised of a List of markdown block lists. Each list
    /// generates a single entry in the Markdown list itself with indented children.
    /// </summary>
    public abstract class MarkdownList : MarkdownBlockCollection<List<MarkdownBlock>>
    {
        /// <summary>
        /// The indentation (4-space) required for sub-elements to keep the list consistent.
        /// </summary>
        private static readonly string SubElementIndent = new(' ', 4);
        
        /// <summary>
        /// Get the prefix for each list item based on the formatting options and current index.
        /// </summary>
        /// <param name="formatting"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected abstract string GetPrefix(MarkdownFormatting formatting, int index);

        /// <summary>
        /// Writes the block to the given TextWriter.
        /// </summary>
        /// <param name="writer">writer</param>
        /// <param name="formatting">optional formatting</param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            if (!AllBlocks().Any()) return;

            string indent = Indent;
            var sb = new StringBuilder();
            for (int i = 0; i < Children.Count; i++)
            {
                var block = Children[i];
                string prefix = GetPrefix(formatting, i);
                for (var index = 0; index < block.Count; index++)
                {
                    var item = block[index];

                    if (index > 0 && item is not MarkdownList)
                        sb.AppendLine();

                    sb.Append(indent + prefix);

                    if (index == 0)
                        prefix = SubElementIndent;

                    var sw = new StringWriter();
                    item.Write(sw, formatting);

                    sb.Append(sw.ToString()
                        .TrimEnd('\r', '\n')
                        .Replace("\n", "\n" + indent + prefix));

                    if (item is not MarkdownList)
                        sb.AppendLine();
                }

                if (block.Count > 1)
                    sb.AppendLine();
            }

            writer.Write(sb.ToString().TrimEnd('\r', '\n') + Environment.NewLine + Environment.NewLine);
        }

        /// <summary>
        /// Add a new MarkdownBlock to our list.
        /// </summary>
        /// <param name="item">Item to add</param>
        public void Add(MarkdownBlock item) => Children.Add(new List<MarkdownBlock> { item });

        /// <summary>
        /// Add a set of MarkdownBlock items as a single item in our list.
        /// </summary>
        /// <param name="items">Items to add</param>
        public void Add(params MarkdownBlock[] items) => Children.Add(new List<MarkdownBlock>(items));

        /// <summary>
        /// Return whether the given MarkdownBlock exists in this list.
        /// </summary>
        /// <param name="item">Item to look for</param>
        /// <returns>True/False if found</returns>
        public bool Contains(MarkdownBlock item) => Children.Any(list => list.Contains(item));
        
        /// <summary>
        /// Removes the given MarkdownBlock from the list.
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <returns>True if removed.</returns>
        public bool Remove(MarkdownBlock item)
        {
            for (var index = 0; index < Children.Count; index++)
            {
                var block = Children[index];
                if (block.Remove(item))
                {
                    // Block is now empty? Remove it.
                    if (block.Count == 0)
                        Children.Remove(block);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns the index of a given MarkdownBlock in our list.
        /// </summary>
        /// <param name="item">Item to look for</param>
        /// <returns>Zero-based index or -1 if not found.</returns>
        public int IndexOf(MarkdownBlock item)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                int pos = Children[i].IndexOf(item);
                if (pos >= 0)
                    return pos + i;
            }
            return -1;
        }

        /// <summary>
        /// Inserts a new top-level block into a given position in the list.
        /// This creates a new top-level entry in the list.
        /// </summary>
        /// <param name="index">Index to insert at</param>
        /// <param name="item">Item to insert</param>
        public void Insert(int index, MarkdownBlock item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (index <= 0) throw new ArgumentOutOfRangeException(nameof(index));

            Children.Insert(index, new List<MarkdownBlock> {item});
        }

        /// <summary>
        /// Returns a count of all Markdown blocks in this list.
        /// This includes any sub-items in the list.
        /// </summary>
        public int BlockCount => Children.Sum(mb => mb.Count);

        /// <summary>
        /// Returns an enumerator of all added Markdown blocks.
        /// This includes sub-items in the list.
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerable<MarkdownBlock> AllBlocks() => Children.SelectMany(item => item);
    }
}
