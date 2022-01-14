using System;

namespace Julmar.GenMarkdown
{
    public class LineBreak : Text
    {
        public LineBreak() : base(@"\" + Environment.NewLine)
        {
        }
    }
}
