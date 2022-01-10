using System;
using Julmar.GenMarkdown;
using Xunit;

namespace GenMarkdown.Tests
{
    public class HeaderTests
    {
        private static readonly string NewLine = Environment.NewLine;

        [Fact]
        public void HeaderRemovesNewLines()
        {
            var h = new Header
            {
                $"This is a{NewLine}",
                $"test of the{NewLine}",
                "emergency broadcast system."
            };
            Assert.Equal($"# This is a test of the emergency broadcast system.{NewLine}", h.ToString());
        }
        
        [Fact]
        public void HeaderGeneratesH1()
        {
            var h1 = new Header(1, "Test");
            Assert.Equal($"# Test{NewLine}", h1.ToString());
        }

        [Fact]
        public void HeaderGeneratesH2()
        {
            var h1 = new Header(2, "Test");
            Assert.Equal($"## Test{NewLine}", h1.ToString());
        }

        [Fact]
        public void HeaderGeneratesH3()
        {
            var h1 = new Header(3, "Test");
            Assert.Equal($"### Test{NewLine}", h1.ToString());
        }

        [Fact]
        public void HeaderGeneratesH4()
        {
            var h1 = new Header(4, "Test");
            Assert.Equal($"#### Test{NewLine}", h1.ToString());
        }

        [Fact]
        public void DefaultHeaderIsH1()
        {
            var h1 = new Header { "Test" };
            Assert.Equal($"# Test{NewLine}", h1.ToString());
        }

        [Fact]
        public void HeaderCombinesText()
        {
            var h1 = new Header { "This ", "is ", "a ", "test" };
            Assert.Equal($"# This is a test{NewLine}", h1.ToString());
        }
    }
}
