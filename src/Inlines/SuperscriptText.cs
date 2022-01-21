namespace Julmar.GenMarkdown
{
    /// <summary>
    /// Renders superscript text - ^text^
    /// </summary>
    public class SuperscriptText : DelimitedText
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Text to render</param>
        public SuperscriptText(string text) : base("^", text) { }
    }
}