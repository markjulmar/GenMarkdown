namespace Julmar.GenMarkdown
{
    public class Header : Paragraph
    {
        public int Level { get; set; }

        public Header(int level)
        {
            Level = level;
        }

        public Header(int level, string text) : base(text)
        {
            Level = level;
        }

        public override string ToString() => new string('#', Level) + " " + base.ToString();
    }
}