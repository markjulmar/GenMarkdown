using System;
using System.IO;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This creates a Markdown code block, either as a fenced block, or indented block.
    /// </summary>
    public class CodeBlock : Paragraph
    {
        private const string FencedCodeBlockMarker = "```";

        /// <summary>
        /// The optional language tied to this code block.
        /// </summary>
        public string Language { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="language">Language</param>
        /// <param name="code">Code</param>
        /// <exception cref="ArgumentException"></exception>
        public CodeBlock(string language, string code = null) : base(code)
        {
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Value cannot be null or empty.", nameof(language));
            this.Language = language.Trim();
        }

        /// <summary>
        /// Default constructor to allow for array initializer.
        /// </summary>
        public CodeBlock()
        {
        }

        /// <summary>
        /// Check to see if we can merge with the prior block
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns></returns>
        protected override bool ShouldAddChild(MarkdownInline item)
        {
            if (item is Text t)
                t.checkForEscapedCharacters = false;
            return true;
        }

        /// <summary>
        /// Writes the block to the given TextWriter.
        /// </summary>
        /// <param name="writer">writer</param>
        /// <param name="formatting">optional formatting</param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            bool noFence = formatting?.UseIndentsForCodeBlocks == true && string.IsNullOrEmpty(Language);

            string indent = Indent;
            if (noFence)
                indent += new string(' ', 4);

            var sw = new StringWriter();
            base.Write(sw, formatting);

            string codeText = sw.ToString().TrimEnd('\r', '\n').Replace("\n", "\n" + indent);
            bool needsEscape = codeText.Contains(FencedCodeBlockMarker);
            writer.Write(indent);

            if (noFence)
            {
                writer.Write(codeText);
            }
            else
            {
                if (needsEscape) writer.Write(FencedCodeBlockMarker[0]); // add one more to escape the entire block.
                writer.Write(FencedCodeBlockMarker);
                writer.WriteLine(Language);
                writer.WriteLine(codeText);
                writer.Write(indent);
                if (needsEscape) writer.Write(FencedCodeBlockMarker[0]);
                writer.WriteLine(FencedCodeBlockMarker);
            }

            writer.WriteLine();
        }
    }
}
