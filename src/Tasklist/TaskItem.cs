using System.IO;

namespace Julmar.GenMarkdown
{
    public class TaskItem : Paragraph
    {
        public bool IsChecked { get; set; }

        public TaskItem()
        {
        }

        public TaskItem(string text, bool isChecked = false) : base(text)
        {
            IsChecked = isChecked;
        }

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