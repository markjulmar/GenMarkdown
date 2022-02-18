using System;
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
        public void TextIsEscaped()
        {
            var text = new Text("Replace <your-function-app-name> with the name");
            string expectedText = @"Replace \<your-function-app-name> with the name";
            Assert.Equal(expectedText, text.ToString());
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
        public void SingleIsIgnored()
        {
            var text = new Text("One > Two");
            string expectedText = @"One > Two";
            Assert.Equal(expectedText, text.ToString());
        }

        [Fact]
        public void DoubleIsNotEscaped()
        {
            var text = new Text("One <> Two");
            string expectedText = @"One <> Two";
            Assert.Equal(expectedText, text.ToString());
        }

        [Fact]
        public void EmptyCaptureNotEscaped()
        {
            var text = new Text("One < > Two");
            string expectedText = @"One < > Two";
            Assert.Equal(expectedText, text.ToString());
        }

        [Fact]
        public void CodeIsNotEscaped()
        {
            string expectedText = "x + y <10>11";
            var code = new InlineCode(expectedText);
            Assert.Equal($"`{expectedText}`", code.ToString());
        }

        [Fact]
        public void OutOfOrderNotEscaped()
        {
            var text = new Text("One > Two and < One");
            string expectedText = @"One > Two and < One";
            Assert.Equal(expectedText, text.ToString());
        }
    }
}
