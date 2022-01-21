using System.IO;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// Base class for inline text with surrounding delimiters
    /// </summary>
    public abstract class DelimitedText : Text
    {
        /// <summary>
        /// Delimiter to surround text with
        /// </summary>
        protected readonly string Delimiter;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="delimiter">Surround delimiter</param>
        /// <param name="text">Text</param>
        protected DelimitedText(string delimiter, string text) : base(text)
        {
            this.Delimiter = delimiter;
        }

        /// <summary>
        /// Writes the text as markdown.
        /// </summary>
        /// <param name="writer">Text writer to write object to</param>
        /// <param name="formatting">Optional formatting information</param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting) 
            => writer.Write($"{Delimiter}{Text}{Delimiter}");
    }
}