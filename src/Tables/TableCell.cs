namespace Julmar.GenMarkdown
{
    public class TableCell
    {
        public int ColumnSpan { get; set; } = 1;
        public MarkdownBlock Content { get; set; }

        public TableCell()
        {
        }

        public TableCell(MarkdownBlock content)
        {
            Content = content;
        }

        public static implicit operator TableCell(MarkdownBlock content) => new(content);
    }
}