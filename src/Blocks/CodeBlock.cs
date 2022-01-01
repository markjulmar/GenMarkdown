using System;

namespace Julmar.GenMarkdown
{
    public class CodeBlock : Paragraph
    {
        private readonly string language;

        public CodeBlock(string language, string code) : base(code)
        {
            this.language = language;
        }

        public CodeBlock(string code) : base(code)
        {
        }

        public CodeBlock()
        {
        }

        public override string ToString()
        {
            string delimiter = "```" + Environment.NewLine;
            return delimiter + base.ToString() + delimiter;
        }
    }
}
