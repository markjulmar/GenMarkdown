using System.IO;

namespace Julmar.GenMarkdown
{
    public class InlineLink : MarkdownInline
    {
        public new string Text { get; set; }
        public string Url { get; set; }

        public InlineLink(string text, string url)
        {
            Text = text;
            Url = url;
        }

        public override void Write(TextWriter writer, MarkdownFormatting formatting) 
            => writer.Write($"[{Text}]({Url})");
    }
}