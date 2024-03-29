﻿using System;
using Julmar.GenMarkdown;
using Xunit;

namespace GenMarkdown.Tests
{
    public class LinkTests
    {
        [Fact]
        public void BasicLinkTest()
        {
            Link link = new Link("", "https://www.microsoft.com");
            Assert.Equal("[](https://www.microsoft.com)\r\n\r\n", link.ToString());
        }

        [Fact]
        public void NullTitleEmitsBlanks()
        {
            Link link = new Link(null, "https://www.microsoft.com");
            Assert.Equal("[](https://www.microsoft.com)\r\n\r\n", link.ToString());
        }

        [Fact]
        public void LinkWithTextTest()
        {
            Link link = new Link("Test", "#1");
            Assert.Equal("[Test](#1)\r\n\r\n", link.ToString());
        }

        [Fact]
        public void UrlSameAsTextEmitsSimpleLink()
        {
            InlineLink link = new InlineLink("www.microsoft.com", "www.microsoft.com");
            Assert.Equal("<www.microsoft.com>", link.ToString());
        }

        [Fact]
        public void UrlSameAsTextWithBoldEmitsComplexLink()
        {
            InlineLink link = new InlineLink("www.microsoft.com", "www.microsoft.com") { Bold = true };
            Assert.Equal("[**www.microsoft.com**](www.microsoft.com)", link.ToString());
        }

        [Fact]
        public void LinkWithDescriptionTest()
        {
            Link link = new Link("Test", "#1", "Description");
            Assert.Equal("[Test](#1 \"Description\")\r\n\r\n", link.ToString());
        }

        [Fact]
        public void NullUrlNotAllowed()
        {
            Assert.Throws<ArgumentException>(() => new Link(null, null));
            Assert.Throws<ArgumentException>(() => new Link(null, ""));
            Assert.Throws<ArgumentException>(() => new Link(null, " "));
        }

        [Fact]
        public void LinkWithNoTextIgnoresBold()
        {
            var link = new Link("", "#1") {Bold = true};
            Assert.Equal("[](#1)\r\n\r\n", link.ToString());
        }

        [Fact]
        public void LinkWithNoTextIgnoresItalic()
        {
            var link = new Link("", "#1") { Italic = true };
            Assert.Equal("[](#1)\r\n\r\n", link.ToString());
        }

        [Fact]
        public void LinkWithNoTextIgnoresBoldAndItalic()
        {
            var link = new Link("", "#1") { Italic = true, Bold = true };
            Assert.Equal("[](#1)\r\n\r\n", link.ToString());
        }

        [Fact]
        public void LinkRendersBold()
        {
            var link = new Link("Test", "#1") { Bold = true };
            Assert.Equal("[**Test**](#1)\r\n\r\n", link.ToString());
        }

        [Fact]
        public void LinkRendersItalic()
        {
            var link = new Link("Test", "#1") { Italic = true };
            Assert.Equal("[*Test*](#1)\r\n\r\n", link.ToString());
        }

        [Fact]
        public void LinkRendersBoldAndItalic()
        {
            var link = new Link("Test", "#1") { Italic = true, Bold = true };
            Assert.Equal("[***Test***](#1)\r\n\r\n", link.ToString());
        }

        [Fact]
        public void LinkRendersCode()
        {
            var link = new Link("Test", "#1") { Code = true };
            Assert.Equal("[`Test`](#1)\r\n\r\n", link.ToString());
        }

        [Fact]
        public void LinkRendersCodeBold()
        {
            var link = new Link("Test", "#1") { Code = true, Bold = true };
            Assert.Equal("[**`Test`**](#1)\r\n\r\n", link.ToString());
        }

        [Fact]
        public void LinkRendersCodeItalic()
        {
            var link = new Link("Test", "#1") { Code = true, Italic = true };
            Assert.Equal("[*`Test`*](#1)\r\n\r\n", link.ToString());
        }

        [Fact]
        public void LinkRendersCodeBoldItalic()
        {
            var link = new Link("Test", "#1") { Code = true, Italic = true, Bold = true };
            Assert.Equal("[***`Test`***](#1)\r\n\r\n", link.ToString());
        }

        [Fact]
        public void LinkWithNoTextIgnoresCode()
        {
            var link = new Link("", "#1") { Code = true };
            Assert.Equal("[](#1)\r\n\r\n", link.ToString());
        }

        [Fact]
        public void ImageLinkBasicTest()
        {
            var link = new ImageLink("www.microsoft.com", "", "image.png");
            Assert.Equal("[![](image.png)](www.microsoft.com)\r\n\r\n", link.ToString());
        }
    }
}
