using System;

namespace Julmar.GenMarkdown
{
    public class Link : MarkdownBlock
    {
        public string Text { get; set; }
        public string Url { get; set; }

        public Link(string text, string url)
        {
            Text = text;
            Url = url;
        }

        public override string ToString() => $"[{Text}]({Url}){Environment.NewLine}";
    }
}