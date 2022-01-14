namespace Julmar.GenMarkdown
{
    public class SubscriptText : DelimitedText
    {
        public SubscriptText(string text) : base("~", text)
        {
        }
    }
}