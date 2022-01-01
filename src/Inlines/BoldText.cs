namespace Julmar.GenMarkdown
{
    public class BoldText : DelimitedText
    {
        public BoldText(string text) : base("**", text) { }
    }
}