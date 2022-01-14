using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Julmar.GenMarkdown
{
    public class TaskList : MarkdownBlock, ICollection<TaskItem>
    {
        protected readonly List<TaskItem> Children = new();

        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            foreach (var child in Children)
            {
                child.IndentLevel = this.IndentLevel;
                child.Write(writer, formatting);
            }

            writer.WriteLine();
        }

        public IEnumerator<TaskItem> GetEnumerator() => Children.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this).GetEnumerator();
        public void Add(TaskItem item) => Children.Add(item);
        public void Clear() => Children.Clear();
        public bool Contains(TaskItem item) => Children.Contains(item);
        public void CopyTo(TaskItem[] array, int arrayIndex) => Children.CopyTo(array, arrayIndex);
        public bool Remove(TaskItem item) => Children.Remove(item);
        public int Count => Children.Count;
        public bool IsReadOnly => false;
    }
}
