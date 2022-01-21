using System.IO;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This generates a markdown horizontal rule
    /// ---
    /// </summary>
    public class HorizontalRule : MarkdownBlock
    {
        /// <summary>
        /// Writes the block to the given TextWriter.
        /// </summary>
        /// <param name="writer">writer</param>
        /// <param name="formatting">optional formatting</param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            writer.WriteLine("---");
            writer.WriteLine();
        }
    }
}
