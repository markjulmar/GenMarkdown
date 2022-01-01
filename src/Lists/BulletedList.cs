namespace Julmar.GenMarkdown
{
    public class BulletedList : MarkdownList
    {
        protected override string GetPrefix(int index) => "* ";
    }
}