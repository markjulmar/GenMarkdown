namespace Julmar.GenMarkdown
{
    /// <summary>
    /// Basic markdown text
    /// </summary>
    public class Text : MarkdownInline
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Text</param>
        public Text(string text)
        {
            Text = text;
        }

        /// <summary>
        /// Creates an inline Bold text element
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>BoldText object</returns>
        public static BoldText Bold(string text) => new(text);

        /// <summary>
        /// Creates an inline BoldItalic text element
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>BoldItalicText object</returns>
        public static BoldItalicText BoldItalic(string text) => new(text);

        /// <summary>
        /// Creates an inline Italic text element
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>ItalicText object</returns>
        public static ItalicText Italic(string text) => new(text);

        /// <summary>
        /// Creates an inline code text element
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>InlineCode object</returns>
        public static InlineCode Code(string text) => new(text);

        /// <summary>
        /// Creates an inline link text element
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="url">Link url</param>
        /// <param name="title">Optional title</param>
        /// <returns>InlineLink object</returns>
        public static InlineLink Link(string text, string url, string title = null) => new(text, url, title);

        /// <summary>
        /// Creates an inline line break element
        /// </summary>
        /// <returns>LineBreak object</returns>
        public static LineBreak LineBreak => new();
    }
}