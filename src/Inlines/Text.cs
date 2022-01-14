namespace Julmar.GenMarkdown
{
    public class Text : MarkdownInline
    {
        public Text(string text)
        {
            Text = text;
        }

        public static BoldText Bold(string text) => new(text);
        public static ItalicText Italic(string text) => new(text);
        public static InlineCode Code(string text) => new(text);
        public static InlineLink Link(string text, string url) => new(text, url);
        public static LineBreak LineBreak => new();
    }
}