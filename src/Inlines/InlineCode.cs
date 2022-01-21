using System.IO;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// Renders an inline code element - `Code`
    /// </summary>
    public class InlineCode : DelimitedText
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Text to render</param>
        public InlineCode(string text) : base("`", text) { }

        /// <summary>
        /// Writes the text as markdown.
        /// </summary>
        /// <param name="writer">Text writer to write object to</param>
        /// <param name="formatting">Optional formatting information</param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            if (Text.Contains(Delimiter))
            {
                // Escape the delimiter
                writer.Write(Delimiter);
                base.Write(writer,formatting);
                writer.Write(Delimiter);
            }
            else base.Write(writer, formatting);
        }
    }
}