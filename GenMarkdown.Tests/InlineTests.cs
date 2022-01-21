using System.IO;
using Julmar.GenMarkdown;
using Xunit;

namespace GenMarkdown.Tests
{
    public class InlineTests
    {
        [Fact]
        public void BasicBoldTest()
        {
            var item = new BoldText("test");
            Assert.Equal("**test**", item.ToString());
        }

        [Fact]
        public void BasicItalicTest()
        {
            var item = new ItalicText("test");
            Assert.Equal("_test_", item.ToString());
        }

        [Fact]
        public void ItalicUsesAsterisksWithFormatterTest()
        {
            var item = new ItalicText("test");

            var sw = new StringWriter();
            item.Write(sw, new MarkdownFormatting { UseAsterisksForEmphasis = true });
            Assert.Equal("*test*", sw.ToString());
        }

        [Fact]
        public void BasicCodeTest()
        {
            var item = new InlineCode("test");
            Assert.Equal("`test`", item.ToString());
        }

        [Fact]
        public void CodeWithTicksEscapesText()
        {
            var item = new InlineCode("This is a `test`");
            Assert.Equal("``This is a `test```", item.ToString());
        }

        [Fact]
        public void LineBreakGeneratesEOL()
        {
            var item = new LineBreak();
            Assert.Equal("\\\r\n", item.ToString());
        }

        [Fact]
        public void BasicEmojiTest()
        {
            var item = new Emoji("smile");
            Assert.Equal(":smile:", item.ToString());
        }

        [Fact]
        public void BasicHighlightTest()
        {
            var item = new HighlightText("test");
            Assert.Equal("==test==", item.ToString());
        }

        [Fact]
        public void InlinesKeepSpaces()
        {
            var item = new BoldText(" test ");
            Assert.Equal("** test **", item.ToString());
        }
    }
}
