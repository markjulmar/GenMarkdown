namespace Julmar.GenMarkdown
{
    public class BulletedList : MarkdownList
    {
        protected override string GetPrefix(MarkdownFormatting formatting, int index) 
            => formatting?.UseAsterisksForBullets==true ? "* " : "- ";
    }
}