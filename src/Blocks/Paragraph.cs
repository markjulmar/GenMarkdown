using System;
using System.IO;
using System.Linq;

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
        /// Check to see if we can merge with the prior block
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns></returns>
        protected override bool ShouldAddChild(MarkdownInline item)
        {
            var itemType = item.GetType();

            var lastItem = Children.LastOrDefault();
            if (lastItem?.GetType() == itemType
                && itemType != typeof(InlineLink)
                && itemType != typeof(LineBreak))
            {
                lastItem.Text += item.Text;
                return false;
            }

            return true;
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