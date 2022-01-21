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
            table.MaxWidth = 80;

            int width = table.MaxWidth / 3; // each column
            width *= 3; // all columns
            width += 1; // ending +

            var lines = table.ToString().Split(Environment.NewLine);
            Assert.Equal(width, lines[0].Length);
        }
    }
}
