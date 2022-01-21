using System.IO;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This represents a single item in a Markdown task list.
    /// </summary>
    public class TaskItem : Paragraph
    {
        /// <summary>
        /// True if this item is checked
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public TaskItem()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Text for item</param>
        /// <param name="isChecked">True if checked</param>
        public TaskItem(string text, bool isChecked = false) : base(text)
        {
            IsChecked = isChecked;
        }

        /// <summary>
        /// Writes the block to the given TextWriter.
        /// </summary>
        /// <param name="writer">writer</param>
        /// <param name="formatting">optional formatting</param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            var sw = new StringWriter();
            base.Write(sw, formatting);

            writer.Write(Indent);
            writer.Write("- [");
            writer.Write(IsChecked ? "x" : " ");
            writer.Write("] ");
            writer.WriteLine(sw.ToString().TrimEnd('\r','\n'));
        }
    }
}