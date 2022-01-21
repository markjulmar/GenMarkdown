using System;
using Julmar.GenMarkdown;
using Xunit;

namespace GenMarkdown.Tests
{
    public class BlockQuoteTests
    {
        private static readonly string Terminator = Environment.NewLine + Environment.NewLine;
        
        [Fact]
        public void BasicBlockQuote()
        {
            string text = "This is a quote";
            var block = new BlockQuote(text);
            Assert.Equal("> " + text + Terminator, block.ToString());
        }

        [Fact]
        public void CanAddParagraphToBlockQuote()
        {
            var block = new BlockQuote
            {
                new Heading(2, "Heading"),
                new Paragraph(),
                "Here's some text"
            };

            Assert.Equal(
                "> ## Heading\r\n"
                     + "> \r\n"
                     + "> Here's some text" + Terminator, block.ToString());
        }

        [Fact]
        public void NestedListIsPartOfQuote()
        {
            var block = new BlockQuote()
            {
                new Heading(4, "The quarterly results look great!"),
                "",
                new List
                {
                    "Revenue was off the chart.",
                    "Profits were higher than ever."
                },
                "",
                new Paragraph
                {
                    Text.Italic("Everything"), " is going according to ", Text.Bold("plan"), "."
                }
            };

            var lines = block.ToString().Split("\r\n");
            Assert.StartsWith("> - ", lines[2]);
            Assert.StartsWith("> - ", lines[3]);
        }

        [Fact]
        public void NestedQuotesOmitSpace()
        {
            var block = new BlockQuote
            {
                "Dorothy followed her through many of the beautiful rooms in her castle.",
                "",
                new BlockQuote
                {
                    "The Witch bade her clean the pots and kettles and sweep the floor and keep the fire fed with wood."
                }
            };

            var lines = block.ToString().Split("\r\n");
            Assert.Equal(5, lines.Length);
            Assert.StartsWith(">> ", lines[2]);
        }
    }
}
