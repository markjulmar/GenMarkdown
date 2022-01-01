using System;

namespace Julmar.GenMarkdown
{
    public class LineBreak : MarkdownInline
    {
        public LineBreak()
        {
            Text = @"\" + Environment.NewLine;
        }
    }
}
