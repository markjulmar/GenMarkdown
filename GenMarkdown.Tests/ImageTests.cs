using Julmar.GenMarkdown;
using Xunit;

namespace GenMarkdown.Tests
{
    public class ImageTests
    {
        [Fact]
        public void BasicImageTest()
        {
            var image = new Image("", "image.png");
            Assert.Equal("![](image.png)\r\n\r\n", image.ToString());
        }

        [Fact]
        public void ImageTrimsAltText()
        {
            var image = new Image(" ", "image.png");
            Assert.Equal("![](image.png)\r\n\r\n", image.ToString());
        }

        [Fact]
        public void ImageStripsCrLfAltText()
        {
            var image = new Image("This is a \r\ntest", "image.png");
            Assert.Equal("![This is a  test](image.png)\r\n\r\n", image.ToString());
        }

        [Fact]
        public void ImageSupportsDescription()
        {
            var image = new Image("test", "image.png", "Description");
            Assert.Equal("![test](image.png \"Description\")\r\n\r\n", image.ToString());
        }

        [Fact]
        public void ImageTrimsDescription()
        {
            var image = new Image("test", "image.png", " Description  ");
            Assert.Equal("![test](image.png \"Description\")\r\n\r\n", image.ToString());
        }

        [Fact]
        public void ImageStripsCrLfDescription()
        {
            var image = new Image("test", "image.png", "The\r\nDescription");
            Assert.Equal("![test](image.png \"The Description\")\r\n\r\n", image.ToString());
        }
    }
}
