using System;
using System.IO;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This renders a Markdown image
    /// ![alt-text](image-path "description")
    /// </summary>
    public class Image : MarkdownBlock
    {
        private string imagePath;

        /// <summary>
        /// Rendered alt-text
        /// </summary>
        public string AltText { get; set; }

        /// <summary>
        /// Path to the image
        /// </summary>
        public string ImagePath
        {
            get => imagePath;
            set => imagePath = value ?? throw new ArgumentNullException(nameof(ImagePath));
        }

        /// <summary>
        /// Optional description for the image
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Internal constructor used by ImageLink
        /// </summary>
        internal Image()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="altText">Alt-text</param>
        /// <param name="imagePath">Path to image</param>
        /// <param name="description">Description of image</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Image(string altText, string imagePath, string description = null)
        {
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
            string altText = (AltText ?? "").Replace("\n", " ").Replace("\r", "").Trim();
            string description = (Description??"").Replace("\n", " ").Replace("\r", "").Trim();

            writer.Write(Indent);
            writer.Write($"![{altText}]({ImagePath}");
            if (!string.IsNullOrWhiteSpace(description))
                writer.Write($" \"{description}\"");
            writer.WriteLine(")");
            writer.WriteLine();
        }
    }
}
