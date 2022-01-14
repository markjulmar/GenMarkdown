using System;
using System.Linq;
using Julmar.GenMarkdown;
using Xunit;

namespace GenMarkdown.Tests
{
    public class CodeBlockTests
    {
        [Fact]
        public void EmptyLanguageThrows()
        {
            Assert.Throws<ArgumentException>(() => new CodeBlock(null, "Test"));
        }

        [Fact]
        public void LanguageTrimsSpaces()
        {
            var cb = new CodeBlock(" test ", "");
            Assert.Equal("test", cb.Language);
            Assert.Equal($"```test", cb.ToString().Split(Environment.NewLine)[0]);
        }

        [Fact]
        public void NoLanguageOmitsSpaces()
        {
            var cb = new CodeBlock("Test");
            var lines = cb.ToString().Split(Environment.NewLine);
            Assert.Equal("Test", cb.Language);
            Assert.Equal($"```Test", lines[0]);
            Assert.Equal($"```", lines[2]);
        }

        [Fact]
        public void IndentAddsSpaces()
        {
            var cb = new CodeBlock
            {
                "using namespace System;\r\n",
                "\r\n",
                "namespace Test\r\n",
                "{\r\n",
                "   public static class Program\r\n",
                "   {\r\n",
                "       Console.WriteLine(\"Hello World\");\r\n",
                "   }\r\n",
                "}\r\n"
            };

            cb.IndentLevel = 1;

            var lines = cb.ToString().Split('\n');
            Assert.True(lines.All(l => l.Length<3||l.StartsWith("   ")));
        }

    }
}
