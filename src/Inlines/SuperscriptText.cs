namespace Julmar.GenMarkdown
{
    public class SuperscriptText : DelimitedText
    {
        public SuperscriptText(string text) : base("^", text)
        {
        }
    }
}