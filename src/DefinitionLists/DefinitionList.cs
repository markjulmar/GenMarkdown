using System.IO;
using System.Linq;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This creates a markdown definition list
    /// Text
    /// : Definition 1
    /// : Definition 2
    /// </summary>
    public class DefinitionList : MarkdownBlockCollection<Definition>
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
                writer.WriteLine();
            }
        }
    }
}
