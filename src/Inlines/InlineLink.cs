namespace Julmar.GenMarkdown
{
    public class InlineLink : MarkdownInline
    {
        public string Text { get; set; }
        public string Url { get; set; }

        public InlineLink(string text, string url)
        {
            Text = text;
            Url = url;
        }

        public override string ToString() => $"[{Text}]({Url})";
    }
}