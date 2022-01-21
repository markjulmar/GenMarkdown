using System.IO;

namespace Julmar.GenMarkdown
{
    public class Paragraph : MarkdownBlockCollection<MarkdownInline>
    {
        /// <summary>
        /// Default constructor to allow collection initialization
        /// </summary>
        public Paragraph()
        {
        }

        /// <summary>
        /// Default constructor to allow collection initialization
        /// </summary>
        public Paragraph(MarkdownInline inline)
        {
            Children.Add(inline);
        }

        /// <summary>
        /// Constructor to create a paragraph from a string.
        /// </summary>
        /// <param name="text">String text</param>
        public Paragraph(string text)
        {
            Children.Add(new Text(text));
        }

        /// <summary>
        /// Writes the block to the given TextWriter.
        /// </summary>
        /// <param name="writer">writer</param>
        /// <param name="formatting">optional formatting</param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            var sw = new StringWriter();
            foreach (var child in Children)
                child.Write(sw, formatting);

            writer.Write(Indent);
            writer.WriteLine(sw.ToString().TrimEnd('\r','\n').Replace("\n", "\n"+Indent));
            writer.WriteLine();
        }
    }
}