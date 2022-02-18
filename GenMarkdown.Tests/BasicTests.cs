using System;
using System.IO;
using Julmar.GenMarkdown;
using Xunit;

namespace GenMarkdown.Tests
{
    public class BasicTests
    {
        static readonly string EOL = Environment.NewLine + Environment.NewLine;

        [Fact]
        public void SplitMarkerIsEscaped()
        {
            var p = new Paragraph("Replace <your-function-app") {"-name> with the name"};

            string expectedText = $@"Replace \<your-function-app-name> with the name{EOL}";
            Assert.Equal(expectedText, p.ToString());
        }

        [Fact]
        public void RawInlineDoesNoTranslation()
        {
            string text = "<kbd>Raw</kbd> with some <b>Text</b> and _Markdown_ \\>";
            var raw = new RawInline(text);
            Assert.Equal(text, raw.ToString());
        }

        [Fact]
        public void TextIsMerged()
        {
            var p = new Paragraph("This is a ") {"test", " of the emergency", " broadcast system."};
            Assert.Single(p);
            Assert.Equal($"This is a test of the emergency broadcast system.{EOL}", p.ToString());
        }

        [Fact]
        public void TextWithBreakIsNotMerged()
        {
            var p = new Paragraph("This is a ") { Text.Bold("test"), " of the emergency", " broadcast system." };
            Assert.Equal(3, p.Count);
            Assert.Equal($"This is a **test** of the emergency broadcast system.{EOL}", p.ToString());
        }

        [Fact]
        public void CodeIsMerged()
        {
            var p = new Paragraph("This is code: ") { Text.Code("var "), Text.Code("x = "), Text.Code("10;") };
            Assert.Equal(2, p.Count);
            Assert.Equal($"This is code: `var x = 10;`{EOL}", p.ToString());
        }

        [Fact]
        public void EmphasisIsEscaped()
        {
            Assert.Equal(@"This is a \_\_test__", new Text("This is a __test__").ToString());
            Assert.Equal(@"*This is a \_\_test__*", new ItalicText("This is a __test__").ToString());
            Assert.Equal(@"This is a \*test*", new Text("This is a *test*").ToString());
            Assert.Equal(@"This is a \*name for* a variable", new Text("This is a *name for* a variable").ToString());
            Assert.Equal(@"This is a \*name for a variable", new Text("This is a *name for a variable").ToString());
            Assert.Equal(@"This is a \_name for a variable", new Text("This is a _name for a variable").ToString());
        }

        [Fact]
        public void ForcedAllEscapesConvertsAllUnderscores()
        {
            var sw = new StringWriter();
            new Text("foo_bar_baz").Write(sw, new MarkdownFormatting() { EscapeAllIntrawordEmphasis =  true });
            Assert.Equal(@"foo\_bar\_baz", sw.ToString());

        }

        [Fact]
        public void EmphasisIsNotEscaped()
        {
            var expectedText = "This is a node_name for a variable";
            Assert.Equal(expectedText, new Text(expectedText).ToString());
            Assert.Equal(@"This is a __name for a variable", new Text("This is a __name for a variable").ToString());
            Assert.Equal(@"This is a node__name for a variable", new Text("This is a node__name for a variable").ToString());
            Assert.Equal(@"This is a **name for a variable", new Text("This is a **name for a variable").ToString());
        }

        [Fact]
        public void BoldTextIsEscaped()
        {
            var p = new Paragraph("Replace ") {Text.Bold("<your-function-app-name>"), " with the name"};
            string expectedText = @"Replace **\<your-function-app-name>** with the name" + EOL;
            Assert.Equal(expectedText, p.ToString());
        }

        [Fact]
        public void SpacedBoldTextIsNotEscaped()
        {
            var p = new Paragraph("Replace ") { Text.Bold("<   >"), " with the name" };
            string expectedText = @"Replace **<   >** with the name" + EOL;
            Assert.Equal(expectedText, p.ToString());
        }


        [Fact]
        public void EmptyBoldTextIsNotEscaped()
        {
            var p = new Paragraph("Replace ") { Text.Bold("<>"), " with the name" };
            string expectedText = @"Replace **<>** with the name" + EOL;
            Assert.Equal(expectedText, p.ToString());
        }

        [Fact]
        public void BracketsAreEscaped()
        {
            Assert.Equal(@"One \<Two>", new Text("One <Two>").ToString());
            Assert.Equal(@"Replace \<your-function-app-name> with the name", new Text("Replace <your-function-app-name> with the name").ToString());
        }

        [Fact]
        public void BracketsAreNotEscaped()
        {
            Assert.Equal(@"One < Two", new Text("One < Two").ToString());
            Assert.Equal(@"One > Two", new Text("One > Two").ToString());
            Assert.Equal(@"One <> Two", new Text("One <> Two").ToString());
            Assert.Equal(@"One < > Two", new Text("One < > Two").ToString());
            Assert.Equal(@"One > Two and < One", new Text("One > Two and < One").ToString());
            Assert.Equal(@"One < Two and > One", new Text("One < Two and > One").ToString());
        }

        [Fact]
        public void CodeIsNotEscaped()
        {
            string expectedText = "x + y <10>11";
            var code = new InlineCode(expectedText);
            Assert.Equal($"`{expectedText}`", code.ToString());
        }
    }
}
