namespace Julmar.GenMarkdown
{
    public sealed class MarkdownFormatting
    {
        /// <summary>
        /// True to use '*' instead of '-' for bulleted lists.
        /// </summary>
        public bool UseAsterisksForBullets { get; set; }

        /// <summary>
        /// Emphasis character ('*' vs. '_')
        /// </summary>
        public bool UseAsterisksForEmphasis { get; set; }

        /// <summary>
        /// Numbered lists will always use proper numeric sequence (1.2.3) vs. (1.1.1)
        /// </summary>
        public bool NumberedListUsesSequence { get; set; }
    }
}