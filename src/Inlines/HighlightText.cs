namespace Julmar.GenMarkdown
{
    /// <summary>
    /// Renders a highlighted text element: ==Text==
    /// </summary>
    public class HighlightText : DelimitedText
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Text to render</param>
        public HighlightText(string text) : base("==", text, true) { }
    }
}