using Julmar.GenMarkdown;
using Xunit;

namespace GenMarkdown.Tests
{
    public class DefinitionListTests
    {
        [Fact]
        public void BasicDefinitionList()
        {
            var dl = new DefinitionList
            {
                new("Color", "Something we see")
            };

            Assert.Equal("Color\r\n: Something we see\r\n\r\n", dl.ToString());
        }

        [Fact]
        public void DefinitionListSupportsMultipleDefs()
        {
            var dl = new DefinitionList
            {
                new("Color", "Something we see")
                {
                    "Makes the world awesome"
                }
            };

            Assert.Equal("Color\r\n: Something we see\r\n: Makes the world awesome\r\n\r\n", dl.ToString());
        }
    }
}
