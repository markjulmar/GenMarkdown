using System;

namespace Julmar.GenMarkdown
{
    public class HorizontalRule : MarkdownBlock
    {
        public override string ToString() => "---" + Environment.NewLine;
    }
}
