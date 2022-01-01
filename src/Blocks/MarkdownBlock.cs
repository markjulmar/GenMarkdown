namespace Julmar.GenMarkdown
{
    public abstract class MarkdownBlock
    {
        public static implicit operator MarkdownBlock(string s) => new Paragraph(s);
    }
}