namespace Julmar.GenMarkdown
{
    public class StrikethroughText : DelimitedText
    {
        public StrikethroughText(string text) : base("~~", text)
        {
        }
    }
}