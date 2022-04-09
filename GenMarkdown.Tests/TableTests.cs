using Julmar.GenMarkdown;
using Xunit;

namespace GenMarkdown.Tests
{
    public class TableTests
    {
        [Fact]
        public void SimpleTableRender()
        {
            var table = new Table(3)
            {
                new() {"H1", "H2", "H3"},
                new() {"c1", "c2", "c3"},
                new() {"c4", "c5", "c6"}
            };

            Assert.Equal("| H1 | H2 | H3 |\r\n|---|---|---|\r\n| c1 | c2 | c3 |\r\n| c4 | c5 | c6 |\r\n\r\n", table.ToString());
        }

        [Fact]
        public void PipeCharsAreHandledInTableContent()
        {
            var table = new Table(2)
            {
                new() {"1", "2"},
                new() {"1|2", "2|3"},
            };

            var lines = table.ToString().Split("\r\n");
            Assert.Equal("| 1&#124;2 | 2&#124;3 |", lines[2]);
        }

        [Fact]
        public void NoEntriesRendersBlank()
        {
            var table = new Table(3);
            Assert.Equal("", table.ToString());
        }

        [Fact]
        public void JustifyTableColumns()
        {
            var table = new Table(ColumnAlignment.Left, ColumnAlignment.Right, ColumnAlignment.Center);
            table.Add(new TableRow("c1", "c2", "c3"));
            var lines = table.ToString().Split("\r\n");
            Assert.Equal("|:---|---:|:---:|", lines[1]);
        }
    }
}
