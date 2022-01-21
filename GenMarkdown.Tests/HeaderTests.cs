using System;
using System.IO;
using Julmar.GenMarkdown;
using Xunit;

namespace GenMarkdown.Tests
{
    public class HeaderTests
    {
        private static readonly string BlockTerminator = Environment.NewLine + Environment.NewLine;

        [Fact]
        public void HeaderRemovesNewLines()
        {
            var h = new Heading
            {
                $"This is a\n",
                $"test of the\n",
                "emergency broadcast system."
            };
            Assert.Equal($"# This is a test of the emergency broadcast system.{BlockTerminator}", h.ToString());
        }
        
        [Fact]
        public void HeaderGeneratesH1()
        {
            var h1 = new Heading(1, "Test");
            Assert.Equal($"# Test{BlockTerminator}", h1.ToString());
        }

        [Fact]
        public void HeaderGeneratesH2()
        {
            var h1 = new Heading(2, "Test");
            Assert.Equal($"## Test{BlockTerminator}", h1.ToString());
        }

        [Fact]
        public void HeaderGeneratesH3()
        {
            var h1 = new Heading(3, "Test");
            Assert.Equal($"### Test{BlockTerminator}", h1.ToString());
        }

        [Fact]
        public void HeaderGeneratesH4()
        {
            var h1 = new Heading(4, "Test");
            Assert.Equal($"#### Test{BlockTerminator}", h1.ToString());
        }

        [Fact]
        public void DefaultHeaderIsH1()
        {
            var h1 = new Heading { "Test" };
            Assert.Equal($"# Test{BlockTerminator}", h1.ToString());
        }

        [Fact]
        public void HeaderCombinesText()
        {
            var h1 = new Heading { "This ", "is ", "a ", "test" };
            Assert.Equal($"# This is a test{BlockTerminator}", h1.ToString());
        }

        [Fact]
        public void HeaderRemovesCrLf()
        {
            var h1 = new Heading(1, "This is a\r\ntest\r\nof the emergency broadcast system");
            Assert.Equal("# This is a test of the emergency broadcast system" + BlockTerminator, h1.ToString());
        }

        [Fact]
        public void HeaderAllows1Through5()
        {
            for (int i = 1; i <= 5; i++)
                new Heading(i, "");

            Assert.Throws<ArgumentOutOfRangeException>(() => new Heading(0, ""));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Heading(6, ""));
        }

        [Fact]
        public void AltHeaderSyntaxForH1UsesEquals()
        {
            var h1 = new Heading(1, "Heading");
            var sw = new StringWriter();
            h1.Write(sw, new MarkdownFormatting() { UseAlternateHeaderSyntax = true });
            var lines = sw.ToString().Split("\r\n");
            Assert.Equal(4, lines.Length);
            Assert.Equal("Heading", lines[0]);
            Assert.Equal("=======", lines[1]);
            Assert.Equal("", lines[2]);
            Assert.Equal("", lines[3]);
        }

        [Fact]
        public void AltHeaderSyntaxForH2UsesDashes()
        {
            var h1 = new Heading(2, "Heading");
            var sw = new StringWriter();
            h1.Write(sw, new MarkdownFormatting() { UseAlternateHeaderSyntax = true });
            var lines = sw.ToString().Split("\r\n");
            Assert.Equal(4, lines.Length);
            Assert.Equal("Heading", lines[0]);
            Assert.Equal("-------", lines[1]);
        }
    }
}
