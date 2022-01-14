using System.IO;

namespace Julmar.GenMarkdown
{
    public class HorizontalRule : MarkdownBlock
    {
        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            writer.WriteLine("---");
            writer.WriteLine();
        }
    }
}
