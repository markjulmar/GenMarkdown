using System.IO;

namespace Julmar.GenMarkdown
{
    public class Link : MarkdownBlock
    {
        private readonly InlineLink link;

        /// <summary>
        /// True to add '**' around link to make it render bold.
        /// </summary>
        public bool Bold
        {
            get => link.Bold;
            set => link.Bold = value;
        }

        /// <summary>
        /// True to add '`' around text to render as code.
        /// </summary>
        public bool Code
        {
            get => link.Code;
            set => link.Code = value;
        }

        /// <summary>
        /// True to add '*' around link to render italic. Bold takes precedence if set.
        /// </summary>
        public bool Italic
        {
            get => link.Italic;
            set => link.Italic = value;
        }

        /// <summary>
        /// The text for the link
        /// </summary>
        public string Text
        {
            get => link.Text;
            set => link.Text = value;
        }

        /// <summary>
        /// The URL for the link
        /// </summary>
        public string Url
        {
            get => link.Url;
            set => link.Url = value;
        }

        /// <summary>
        /// The title for the link (optional)
        /// </summary>
        public string Title
        {
            get => link.Title;
            set => link.Title = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text"></param>
        /// <param name="url"></param>
        /// <param name="title"></param>
        public Link(string text, string url, string title = "")
        {
            link = new InlineLink(text, url, title);
        }

        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            link.Write(writer, formatting);
            writer.WriteLine();
            writer.WriteLine();
        }
    }
}