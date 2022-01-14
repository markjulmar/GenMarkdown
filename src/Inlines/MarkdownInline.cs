using System.IO;

namespace Julmar.GenMarkdown
{
    public abstract class MarkdownInline
    {
        public string Text { get; set; }
        public static implicit operator MarkdownInline(string s) => new Text(s);
        public virtual void Write(TextWriter writer, MarkdownFormatting formatting) => writer.Write(Text);
        public override string ToString()
        {
            var sw = new StringWriter();
            Write(sw, null);
            return sw.ToString();
        }
    }
}