namespace Julmar.GenMarkdown
{
    /// <summary>
    /// Emoji - :value:
    /// See https://api.github.com/emojis for possible characters.
    /// </summary>
    public class Emoji : DelimitedText
    {
        public Emoji(string emoji) : base(":", emoji?.Trim())
        {
        }
    }
}
