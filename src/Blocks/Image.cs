using System.IO;

namespace Julmar.GenMarkdown
{
    public class Image : MarkdownBlock
    {
        public string AltText { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }

        public Image(string altText, string imagePath, string description = "")
        {
            AltText = altText;
            ImagePath = imagePath;
            Description = description;
        }

        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            writer.Write(Indent);
            writer.WriteLine(string.IsNullOrEmpty(Description)
                ? $"![{AltText}]({ImagePath})" : $"![{AltText}]({ImagePath} \"{Description}\")");
            writer.WriteLine();
        }
    }
}
