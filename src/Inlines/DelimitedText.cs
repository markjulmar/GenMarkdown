using System.IO;

namespace Julmar.GenMarkdown
{
    public abstract class DelimitedText : Text
    {
        protected readonly string Delimiter;

        protected DelimitedText(string delimiter, string text) : base(text)
        {
            this.Delimiter = delimiter;
        }

        public override void Write(TextWriter writer, MarkdownFormatting formatting) 
            => writer.Write($"{Delimiter}{Text}{Delimiter}");
    }
}