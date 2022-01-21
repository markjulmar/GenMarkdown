namespace Julmar.GenMarkdown
{
    /// <summary>
    /// Renders subscript text - ~text~
    /// </summary>
    public class SubscriptText : DelimitedText
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Text to render</param>
        public SubscriptText(string text) : base("~", text) { }
    }
}