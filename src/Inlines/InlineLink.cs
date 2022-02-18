using System;
using System.IO;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// Renders an inline link - [alt-text](url "description")
    /// </summary>
    public class InlineLink : MarkdownInline
    {
        /// <summary>
        /// True to add '**' around link to make it render bold.
        /// </summary>
        public bool Bold { get; set; }

        /// <summary>
        /// True to add '`' around text to render as code.
        /// </summary>
        public bool Code { get; set; }

        /// <summary>
        /// True to add '*' around link to render italic.
        /// </summary>
        public bool Italic { get; set; }

        /// <summary>
        /// Text for the link
        /// </summary>
        public new string Text { get; set; }

        /// <summary>
        /// Url for the link
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Title for the link
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text"></param>
        /// <param name="url"></param>
        /// <param name="title"></param>
        public InlineLink(string text, string url, string title = null) : base("")
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(url));

            Text = text;
            Url = url;
            Title = title?.Trim();
        }

        /// <summary>
        /// Write the Markdown for this inline link
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="formatting"></param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            // Output simplified URL format if URL is the same as the text if this is an inline link.
            if (!Bold && !Italic && !Code && string.IsNullOrEmpty(Title)
                && Text == Url && GetType() == typeof(InlineLink))
            {
                writer.Write($"<{Url}>");
                return;
            }

            if (Bold) writer.Write("**");
            if (Italic) writer.Write("*");

            writer.Write("[");

            if (!string.IsNullOrEmpty(Text))
            {
                if (Code) writer.Write("`");
                writer.Write(Text);
                if (Code) writer.Write("`");
            }

            writer.Write($"]({Url}");

            if (!string.IsNullOrWhiteSpace(Title))
                writer.Write(" \"" + Title + "\"");
            
            writer.Write(")");

            if (Bold) writer.Write("**");
            if (Italic) writer.Write("*");
        }
    }
}