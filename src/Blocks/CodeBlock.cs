using System.Text;

namespace Julmar.GenMarkdown
{
    public class CodeBlock : MarkdownBlock
    {
        public string Code { get; set; }
        private readonly string language;

        public CodeBlock(string language, string code)
        {
            Code = code;
            this.language = language;
        }

        public CodeBlock(string code)
        {
            Code = code;
        }

        public override string ToString()
        {
            const string delimiter = "```";

            var sb = new StringBuilder(delimiter);
            if (!string.IsNullOrEmpty(language))
                sb.Append(" " + language);
            sb.AppendLine();
            sb.AppendLine(Code);
            sb.AppendLine(delimiter);

            return sb.ToString();
        }
    }
}
