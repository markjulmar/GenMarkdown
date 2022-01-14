using System.IO;

namespace Julmar.GenMarkdown
{
    public class InlineCode : DelimitedText
    {
        public InlineCode(string text) : base("`", text) { }

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