using System;
using System.IO;

namespace Julmar.GenMarkdown
{
    public class CodeBlock : Paragraph
    {
        protected const string ticks = "```";

        public string Language { get; }

        public CodeBlock(string language, string code = "") : base(code)
        {
            if (string.IsNullOrEmpty(language))
                throw new ArgumentException("Value cannot be null or empty.", nameof(language));
            this.Language = language.Trim();
        }

        public CodeBlock()
        {
        }

        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            string indent = Indent;

            var sw = new StringWriter();
            base.Write(sw, formatting);

            writer.Write(indent);
            writer.Write(ticks);
            writer.WriteLine(Language);
            writer.WriteLine(sw.ToString().TrimEnd('\r', '\n').Replace("\n", "\n" + indent));
            writer.Write(indent);
            writer.WriteLine(ticks);
            writer.WriteLine();
        }
    }
}
