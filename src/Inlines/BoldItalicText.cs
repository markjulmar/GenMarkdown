namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This emits bold _and_ italic text
    /// </summary>
    public class BoldItalicText : DelimitedText
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Text</param>
        public BoldItalicText(string text) : base("***", text, true) { }
    }
}
