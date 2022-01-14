namespace Julmar.GenMarkdown
{
    public class HighlightText : DelimitedText
    {
        public HighlightText(string text) : base("==", text)
        {
        }
    }
}