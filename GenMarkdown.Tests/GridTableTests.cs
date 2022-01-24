using System;
using System.Linq;
using Julmar.GenMarkdown;
using Xunit;

namespace GenMarkdown.Tests
{
    public class GridTableTests
    {
        [Fact]
        public void CheckHeaderWidth()
        {
            var table = new GridTable(3)
            {
                new() {"A1", "B2","C3"},
                new() {"A1", "B2","C3"},
                new() {"A1", "B2","C3"},
            };

            // 3 columns, 6 chars per column, 4 separators.
            int width = 3 * 3 * "A1".Length + 3 + 1;

            var lines = table.ToString().Split(Environment.NewLine);
            Assert.Equal(width, lines[0].Length);
        }
    }
}
