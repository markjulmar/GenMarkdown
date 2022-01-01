namespace Julmar.GenMarkdown
{
    public class InlineCode : DelimitedText
    {
        public InlineCode(string text) : base("`", text) { }
    }
}