using System.IO;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// Renders emphasized (italic) text - *text* or _text_
    /// </summary>
    public class ItalicText : Text
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Text to render</param>
        public ItalicText(string text) : base(text) { }

        /// <summary>
        /// Writes the text as markdown.
        /// </summary>
        /// <param name="writer">Text writer to write object to</param>
        /// <param name="formatting">Optional formatting information</param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            char emphasis = formatting?.UseAsterisksForEmphasis == true ? '*' : '_';
            if (Text.Contains(emphasis))
                emphasis = emphasis == '*' ? '_' : '*';

            var sw = new StringWriter();
            base.Write(sw, formatting);

            writer.Write(emphasis);
            writer.Write(sw.ToString());
            writer.Write(emphasis);
        }
    }
}