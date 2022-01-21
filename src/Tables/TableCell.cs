namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This represents a single cell in a Markdown table
    /// </summary>
    public class TableCell
    {
        /// <summary>
        /// Column span - defaults to 1.
        /// </summary>
        public int ColumnSpan { get; set; } = 1;

        /// <summary>
        /// The content for the cell
        /// </summary>
        public MarkdownBlock Content { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public TableCell()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Text to display</param>
        public TableCell(string text) : this(new Text(text))
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">Content to display in the cell</param>
        public TableCell(MarkdownInline content) : this(new Paragraph(content))
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">Content to display in the cell</param>
        public TableCell(MarkdownBlock content)
        {
            Content = content;
        }

        /// <summary>
        /// Converter to take a MarkdownBlock and create a TableCell
        /// </summary>
        /// <param name="content">Content to create cell from</param>
        public static implicit operator TableCell(MarkdownBlock content) => new(content);
    }
}