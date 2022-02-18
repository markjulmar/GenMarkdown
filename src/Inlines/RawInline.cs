namespace Julmar.GenMarkdown
{
    /// <summary>
    /// A raw Markdown element - no translation is done, text is emitted exactly as presented.
    /// </summary>
    public class RawInline : Text
    {
        public RawInline(string text) : base(text)
        {
            checkForEscapedCharacters = false;
        }
    }
}
