namespace Julmar.GenMarkdown;

public class RawBlock : Paragraph
{
    public RawBlock(string text)
    {
        Children.Add(new RawInline(text));
    }

    protected override bool ShouldAddChild(MarkdownInline item) => false;
}