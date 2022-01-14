using System;
using System.IO;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// Base class for all Markdown blocks.
    /// </summary>
    public abstract class MarkdownBlock
    {
        private int indentLevel;

        /// <summary>
        /// If this block is intended to be quoted, set this to > 0.
        /// </summary>
        public int IndentLevel
        {
            get => indentLevel;
            set
            {
                if (value < 0 || value > 3)
                    throw new ArgumentOutOfRangeException(nameof(IndentLevel), "Indent level must be between 0 and 3.");

                indentLevel = value;
            }
        }

        /// <summary>
        /// Conversion from a string to Paragraph
        /// </summary>
        /// <param name="s"></param>
        public static implicit operator MarkdownBlock(string s) => new Paragraph(s);

        /// <summary>
        /// Internal method to generate indent block text
        /// </summary>
        protected string Indent => IndentLevel > 0 ? new string(' ', IndentLevel * 3) : "";

        /// <summary>
        /// Writes the block to the given TextWriter.
        /// </summary>
        /// <param name="writer">writer</param>
        /// <param name="formatting">optional formatting</param>
        public abstract void Write(TextWriter writer, MarkdownFormatting formatting);

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var writer = new StringWriter();
            Write(writer, null);
            return writer.ToString();
        }
    }
}