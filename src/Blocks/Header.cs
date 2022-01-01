using System;

namespace Julmar.GenMarkdown
{
    public class Header : MarkdownBlock
    {
        public int Level { get; set; }
        public string Text { get; set; }

        public Header(int level, string text)
        {
            Level = level;
            Text = text
                .Replace("\r", "")
                .Replace("\n", "");
        }

        public override string ToString() => new string('#', Level) + " " + Text + Environment.NewLine + Environment.NewLine;
    }
}