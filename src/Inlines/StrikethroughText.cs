namespace Julmar.GenMarkdown
{
    /// <summary>
    /// Renders strike-through text - ~~text~~
    /// </summary>
    public class StrikethroughText : DelimitedText
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Text to render</param>
        public StrikethroughText(string text) : base("~~", text) { }
    }
}