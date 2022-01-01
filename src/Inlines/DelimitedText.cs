namespace Julmar.GenMarkdown
{
    public abstract class DelimitedText : Text
    {
        private readonly string delimiter;

        protected DelimitedText(string delimiter, string text) : base(text)
        {
            this.delimiter = delimiter;
        }

        public override string ToString() => $"{delimiter}{Text}{delimiter}";
    }
}