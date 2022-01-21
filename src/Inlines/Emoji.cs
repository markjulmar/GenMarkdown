namespace Julmar.GenMarkdown
{
    /// <summary>
    /// Emoji - :value:
    /// See https://api.github.com/emojis for possible characters.
    /// </summary>
    public class Emoji : DelimitedText
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="emoji">Emoji to render</param>
        public Emoji(string emoji) : base(":", emoji?.Trim()) { }
    }
}
