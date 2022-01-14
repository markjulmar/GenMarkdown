using System.IO;

namespace Julmar.GenMarkdown
{
    public class ItalicText : Text
    {
        public ItalicText(string text) : base(text) { }
        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            char emphasis = formatting?.UseAsterisksForEmphasis == true ? '*' : '_';
            var sw = new StringWriter();
            base.Write(sw, formatting);

            writer.Write(emphasis);
            writer.Write(sw.ToString());
            writer.Write(emphasis);
        }
    }
}