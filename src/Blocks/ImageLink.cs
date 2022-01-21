using System;
using System.IO;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This creates a link to an image
    /// [![image-text](image-url) "image description")](image-link)
    /// </summary>
    public class ImageLink : MarkdownBlock
    {
        private readonly Image theImage = new();
        private string linkUrl;

        /// <summary>
        /// Rendered alt-text
        /// </summary>
        public string AltText
        {
            get => theImage.AltText;
            set => theImage.AltText = value;
        }

        /// <summary>
        /// Path to the image
        /// </summary>
        public string ImagePath
        {
            get => theImage.ImagePath;
            set => theImage.ImagePath = value;
        }

        /// <summary>
        /// Optional description for the image
        /// </summary>
        public string Description
        {
            get => theImage.Description;
            set => theImage.Description = value;
        }

        /// <summary>
        /// The URL for the link
        /// </summary>
        public string LinkUrl
        {
            get => linkUrl;
            set => linkUrl = value ?? throw new ArgumentNullException(nameof(LinkUrl));
        }

        /// <summary>
        /// Constructor for the ImageLink
        /// </summary>
        public ImageLink(string linkUrl, string altText, string imagePath, string description = null)
        {
            LinkUrl = linkUrl ?? throw new ArgumentNullException(nameof(linkUrl));
            AltText = altText;
            ImagePath = imagePath ?? throw new ArgumentNullException(nameof(imagePath));
            Description = description;
        }

        /// <summary>
        /// Writes the block to the given TextWriter.
        /// </summary>
        /// <param name="writer">writer</param>
        /// <param name="formatting">optional formatting</param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            var sw = new StringWriter();
            theImage.Write(sw, formatting);
            writer.Write("[");
            writer.Write(sw.ToString().TrimEnd('\r','\n'));
            writer.WriteLine($"]({LinkUrl})");
            writer.WriteLine();
        }
    }
}