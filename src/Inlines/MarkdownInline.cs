namespace Julmar.GenMarkdown
{
    public abstract class MarkdownInline
    {
        public string Text { get; set; }
        public override string ToString() => Text;

        public static implicit operator MarkdownInline(string s) => new Text(s);
    }
}