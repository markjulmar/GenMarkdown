namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This generates an unordered Markdown list
    /// - Item 1
    /// - Item 2
    /// </summary>
    public class List : MarkdownList
    {
        /// <summary>
        /// Get the prefix for each list item based on the formatting options and current index.
        /// </summary>
        /// <param name="formatting"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override string GetPrefix(MarkdownFormatting formatting, int index) 
            => formatting?.UseAsterisksForBullets==true ? "* " : "- ";
    }
}