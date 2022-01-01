namespace Julmar.GenMarkdown
{
    public class ItalicText : DelimitedText
    {
        public ItalicText(string text) : base("_", text) { }
    }
}