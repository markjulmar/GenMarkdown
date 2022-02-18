using System.IO;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// Basic markdown text
    /// </summary>
    public class Text : MarkdownInline
    {
        /// <summary>
        /// Internal flag to turn off escaped character support.
        /// This needs to be done for code blocks
        /// </summary>
        protected internal bool checkForEscapedCharacters;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Text</param>
        public Text(string text) : base(text)
        {
            checkForEscapedCharacters = true;
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

        /// <summary>
        /// Writes the text as markdown.
        /// </summary>
        /// <param name="writer">Text writer to write object to</param>
        /// <param name="formatting">Optional formatting information</param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting) =>
            writer.Write(checkForEscapedCharacters ? EscapeReservedCharacters(Text) : Text);

        /// <summary>
        /// Cleans the text of possible misinterpretations of Markdown.
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>Cleaned text</returns>
        protected static string EscapeReservedCharacters(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                int npos = text.IndexOf('<');
                while (npos >= 0)
                {
                    int epos = text.IndexOf('>', npos + 1);
                    if (epos > npos + 1 && text.Substring(npos + 1, epos - npos - 1).Trim().Length > 0)
                    {
                        text = text.Remove(npos, 1)
                            .Insert(npos, @"\<");
                    }
                    npos = text.IndexOf('<', npos + 2);
                }
            }
            return text;
        }
    }
}