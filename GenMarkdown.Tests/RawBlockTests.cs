using Julmar.GenMarkdown;
using Xunit;

namespace GenMarkdown.Tests;

public class RawBlockTests
{
    [Fact]
    public void HtmlIsUnaltered()
    {
        var html = "<text src='test'/>";
        var rb = new RawBlock(html);
        Assert.Equal(html+"\r\n\r\n", rb.ToString());
    }

    [Fact]
    public void IndentAddsSpaces()
    {
        var html = "<text src='test'/>";
        var rb = new RawBlock(html) { IndentLevel = 1 };
        Assert.Equal("   " + html + "\r\n\r\n", rb.ToString());
    }

    [Fact]
    public void MultiLineHmtlIsUnaltered()
    {
        var html = "<video width=\"320\" height=\"240\" controls>\r\n"
                   + "   <source src=\"test.mp4\" type=\"video/mp4\">\r\n"
                   + "Your browser does not support the video tag.</video>\r\n";
        var rb = new RawBlock(html);
        Assert.Equal(html + "\r\n", rb.ToString());
    }

    [Fact]
    public void MultilineIndentAddsSpaces()
    {
        var html = "<video width=\"320\" height=\"240\" controls>\r\n"
                       + "   <source src=\"test.mp4\" type=\"video/mp4\">\r\n"
                       + "   Your browser does not support the video tag.\r\n"
                       + "</video>\r\n";
        var rb = new RawBlock(html) { IndentLevel = 1 };

        var expected = "   <video width=\"320\" height=\"240\" controls>\r\n"
                   + "      <source src=\"test.mp4\" type=\"video/mp4\">\r\n"
                   + "      Your browser does not support the video tag.\r\n"
                   + "   </video>\r\n\r\n";

        Assert.Equal(expected, rb.ToString());
    }
}