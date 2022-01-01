using System;

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

        public override string ToString() => 
            (string.IsNullOrEmpty(Description) 
                ? $"![{AltText}]({ImagePath})" 
                : $"![{AltText}]({ImagePath} \"{Description}\")") + Environment.NewLine;
    }
}
