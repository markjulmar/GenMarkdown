namespace Julmar.GenMarkdown
{
    /// <summary>
    /// Optional formatting directives for the markdown library
    /// </summary>
    public class MarkdownFormatting
    {
        /// <summary>
        /// True to use '*' instead of '-' for unordered lists.
        /// </summary>
        public bool UseAsterisksForBullets;

        /// <summary>
        /// Emphasis character ('*' vs. '_')
        /// </summary>
        public bool UseAsterisksForEmphasis;

        /// <summary>
        /// Numbered lists will always use proper numeric sequence (1.2.3) vs. (1.1.1)
        /// </summary>
        public bool OrderedListUsesSequence;

        /// <summary>
        /// Headers have '=' row under them instead of '#' prefix.
        /// </summary>
        public bool UseAlternateHeaderSyntax;

        /// <summary>
        /// True to use 4-space indents vs. fenced characters for code blocks.
        /// </summary>
        public bool UseIndentsForCodeBlocks;

        /// <summary>
        /// True to add spaces to make pipe tables line up.
        /// </summary>
        public bool PrettyPipeTables;
    }
}