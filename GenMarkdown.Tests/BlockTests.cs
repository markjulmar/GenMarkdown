using System;
using Julmar.GenMarkdown;
using Xunit;

namespace GenMarkdown.Tests
{
    public class BlockTests
    {
        private static readonly string LineEnd = Environment.NewLine + Environment.NewLine;

        [Fact]
        void IndentAddsSpacesToFrontOfBlock()
        {
            string text = "This is a test";
            var block = new Paragraph(text);
            Assert.Equal(text + LineEnd, block.ToString());

            block.IndentLevel = 1;
            Assert.Equal("   " + text + LineEnd, block.ToString());

            block.IndentLevel = 2;
            Assert.Equal("      " + text + LineEnd, block.ToString());
        }

        [Fact]
        void IndentMustBeBetween0And3()
        {
            var block = new Paragraph();
            Assert.Throws<ArgumentOutOfRangeException>(() => block.IndentLevel = -1);
            block.IndentLevel = 0;
            block.IndentLevel = 1;
            block.IndentLevel = 2;
            block.IndentLevel = 3;
            Assert.Throws<ArgumentOutOfRangeException>(() => block.IndentLevel = 4);
        }

        [Fact]
        void HorizontalRuleIsThreeDashes()
        {
            var block = new HorizontalRule();
            Assert.Equal("---\r\n\r\n", block.ToString());
        }
    }
}
