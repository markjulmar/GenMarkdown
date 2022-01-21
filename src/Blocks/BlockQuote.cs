using System.IO;
using System.Text;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This generates a Markdown quote
    /// > Text
    /// </summary>
    public class BlockQuote : MarkdownBlockCollection<MarkdownBlock>
    {
        /// <summary>
        /// Constructor usable with array syntax creation
        /// </summary>
        public BlockQuote()
        {
        }

        /// <summary>
        /// Constructor which takes an initial block of text.
        /// </summary>
        /// <param name="text">Text to create a quote from.</param>
        public BlockQuote(string text)
        {
            Children.Add(text);
        }

        /// <summary>
        /// Writes the block to the given TextWriter.
        /// </summary>
        /// <param name="writer">writer</param>
        /// <param name="formatting">optional formatting</param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            string indent = Indent;
            var sb = new StringBuilder();

            foreach (var item in Children)
            {
                var sw = new StringWriter();
                item.Write(sw, formatting);

                bool isNestedQuote = item is BlockQuote;

                sb.Append(indent + (isNestedQuote ? ">" : "> "))
                    .AppendLine(sw.ToString()
                        .TrimEnd('\r', '\n')
                        .Replace("\n", "\n" + indent + "> "));
            }
            writer.Write(sb.AppendLine().ToString());
        }
    }
}
