using System.IO;

namespace Julmar.GenMarkdown
{
    public class Link : MarkdownBlock
    {
        public string Text { get; set; }
        public string Url { get; set; }

        public Link(string text, string url)
        {
            Text = text;
            Url = url;
        }

        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            writer.WriteLine($"{Indent}[{Text}]({Url})");
            writer.WriteLine();
        }
    }
}