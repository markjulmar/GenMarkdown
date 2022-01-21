using System.IO;
using System.Linq;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This generates a Markdown Task list
    /// - [ ] Item 1
    /// - [x] Item 2
    /// - [ ] Item 3
    /// </summary>
    public class TaskList : MarkdownBlockCollection<TaskItem>
    {
        /// <summary>
        /// Writes the block to the given TextWriter.
        /// </summary>
        /// <param name="writer">writer</param>
        /// <param name="formatting">optional formatting</param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            if (!Children.Any()) return;

            foreach (var child in Children)
            {
                child.IndentLevel = this.IndentLevel;
                child.Write(writer, formatting);
            }

            writer.WriteLine();
        }
    }
}
