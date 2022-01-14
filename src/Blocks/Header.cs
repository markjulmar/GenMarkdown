using System.IO;

namespace Julmar.GenMarkdown
{
    public class Header : Paragraph
    {
        /// <summary>
        /// Header level (1-5)
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Optional ID - rendered as {#id}
        /// </summary>
        public string Id { get; set; }

        public Header() : this(1)
        {
        }

        public Header(string text) : this(1, text)
        {
        }

        public Header(int level)
        {
            Level = level;
        }

        public Header(int level, string text) : base(text)
        {
            Level = level;
        }

        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            var sw = new StringWriter();
            base.Write(sw, formatting);

            writer.Write(new string('#', Level) + " " +
                         sw.ToString()
                             .Replace("\r", "")
                             .Replace("\n", " ")
                             .TrimEnd());
            if (!string.IsNullOrEmpty(Id))
                writer.Write(" {#" + Id.Trim() + "}");
            writer.WriteLine();
            writer.WriteLine();
        }
    }
}