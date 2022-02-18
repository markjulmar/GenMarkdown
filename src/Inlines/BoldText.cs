namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This emits Markdown bold text - **bold**
    /// </summary>
    public class BoldText : DelimitedText
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Text</param>
        public BoldText(string text) : base("**", text, true) { }
    }
}