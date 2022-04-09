using System.IO;
using System.Linq;
using Julmar.GenMarkdown;
using Xunit;

namespace GenMarkdown.Tests
{
    public class ListTests
    {
        [Fact]
        public void BasicListTest()
        {
            var list = new List {"one", "two", "three"};
            Assert.Equal(3, list.Count);
            Assert.Equal(3, list.BlockCount);
            Assert.Equal("- one\r\n- two\r\n- three\r\n\r\n", list.ToString());
        }

        [Fact]
        public void NoEntriesRendersBlank()
        {
            var list = new List();
            Assert.Equal("", list.ToString());
        }

        [Fact]
        public void LetteredRendersAAA()
        {
            var list = new OrderedList() {Lettered = true};
            list.Add("one");
            list.Add("two");
            list.Add("three");

            Assert.Equal(3, list.Count);
            Assert.Equal("a. one\r\na. two\r\na. three\r\n\r\n", list.ToString());
        }

        [Fact]
        public void LetteredSequenceRendersABC()
        {
            var list = new OrderedList() { Lettered = true };
            list.Add("one");
            list.Add("two");
            list.Add("three");

            Assert.Equal(3, list.Count);

            var sw = new StringWriter();
            list.Write(sw, new MarkdownFormatting { OrderedListUsesSequence = true });

            Assert.Equal("a. one\r\nb. two\r\nc. three\r\n\r\n", sw.ToString());
        }

        [Fact]
        public void StartNumberAffectsLetteredList()
        {
            var list = new OrderedList() { Lettered = true, StartingNumber = 100 };
            list.Add("one");
            list.Add("two");
            list.Add("three");

            Assert.Equal(3, list.Count);

            var sw = new StringWriter();
            list.Write(sw, new MarkdownFormatting { OrderedListUsesSequence = true });

            Assert.Equal("cv. one\r\ncw. two\r\ncx. three\r\n\r\n", sw.ToString());
        }

        [Fact]
        public void ListWithMultipleBlocks()
        {
            var list = new OrderedList
            {
                "one",
                {
                    "two",
                    new CodeBlock("bash", "mkdir data")
                },
                {
                    "three",
                    new CodeBlock("bash", "ls -la")
                }
            };

            Assert.Equal(3, list.Count);
            Assert.Equal(5, list.BlockCount);
            Assert.Single(list[0]);
            Assert.Equal(2, list[1].Count);
            Assert.Equal(2, list[2].Count);
        }

        [Fact]
        public void CheckListEnumerable()
        {
            var item1 = new Paragraph("One");
            var item2 = new Paragraph("Two");
            var item3 = new Paragraph("Three");

            var list = new List
            {
                item1, item2, item3
            };

            Assert.Equal(item2, list.AllBlocks().ElementAt(1));
            Assert.Equal(item2, list.AllBlocks().Skip(1).First());
        }

        [Fact]
        public void CheckListIndexOf()
        {
            var item1 = new Paragraph("One");
            var item2 = new Paragraph("Two");
            var item3 = new Paragraph("Three");

            var list = new List
            {
                item1, item2, item3
            };

            Assert.Equal(1, list.IndexOf(item2));
        }

        [Fact]
        public void CheckListContains()
        {
            var item1 = new Paragraph("One");
            var item2 = new Paragraph("Two");
            var item3 = new Paragraph("Three");

            var list = new List
            {
                item1, item2, item3
            };

            Assert.Contains(item2, list.AllBlocks());
            Assert.True(list.Contains(item2));
            Assert.DoesNotContain("One", list.AllBlocks());
            Assert.False(list.Contains("Two"));
        }

        [Fact]
        public void CanAddDynamicItemsToList()
        {
            var item1 = new Paragraph("One");
            var item2 = new Paragraph("Two");
            var item3 = new Paragraph("Three");

            var list = new List
            {
                item1, item2, item3
            };

            var item4 = new Paragraph("Four");
            list.Add(item4);
            Assert.Equal(4, list.Count);
        }

        [Fact]
        public void CanInsertDynamicItemsToList()
        {
            var item1 = new Paragraph("One");
            var item2 = new Paragraph("Two");
            var item3 = new Paragraph("Three");

            var list = new List
            {
                item1, item2, item3
            };

            Assert.Equal(item3, list[2].Single());

            var item2p5 = new Paragraph("Four");
            list.Insert(2, item2p5);
            Assert.Equal(4, list.Count);
            Assert.Equal(item2p5, list[2].Single());
            Assert.Equal(item3, list[3].Single());
        }

        [Fact]
        public void CanRemoveItemsFromList()
        {
            var item1 = new Paragraph("One");
            var item2 = new Paragraph("Two");
            var item3 = new Paragraph("Three");

            var list = new List
            {
                item1, item2, item3
            };

            list.Remove(item2);
            Assert.Equal("- One\r\n- Three\r\n\r\n", list.ToString());
            Assert.Equal(2, list.Count);
            Assert.Equal(2, list.BlockCount);

            list.Insert(1, item2);
            Assert.Equal(3, list.Count);
            Assert.Equal(3, list.BlockCount);
            Assert.Equal("- One\r\n- Two\r\n- Three\r\n\r\n", list.ToString());
            Assert.Equal(3, list.Count);

            Assert.False(list.Remove("One"));
        }

        [Fact]
        public void BLUsesAsterisksWithFormatter()
        {
            var list = new List { "one", "two", "three" };
            var sw = new StringWriter();
            list.Write(sw, new MarkdownFormatting() { UseAsterisksForBullets = true });

            Assert.Equal("* one\r\n* two\r\n* three\r\n\r\n", sw.ToString());
        }

        [Fact]
        public void NLUsesOneForEachSequence()
        {
            var list = new OrderedList { "one", "two", "three" };
            Assert.Equal(3, list.Count);
            Assert.Equal("1. one\r\n1. two\r\n1. three\r\n\r\n", list.ToString());
        }

        [Fact]
        public void NLUsesNumberForEachSequenceWithFormatter()
        {
            var list = new OrderedList { "one", "two", "three" };
            var sw = new StringWriter();
            list.Write(sw, new MarkdownFormatting() { OrderedListUsesSequence = true});
            Assert.Equal("1. one\r\n2. two\r\n3. three\r\n\r\n", sw.ToString());
        }

        [Fact]
        public void NLStartsAtSpecifiedSequence()
        {
            var list = new OrderedList(4) { "one", "two", "three" };
            Assert.Equal("4. one\r\n1. two\r\n1. three\r\n\r\n", list.ToString());
        }

        [Fact]
        public void AddedParagraphIsAlwaysQuotedInList()
        {
            var list = new List { "one",  { "two", "three" } };
            Assert.Equal(3, list.BlockCount);
            Assert.Equal(2, list.Count);

            var lines = list.ToString().Split("\r\n");
            Assert.Equal(6, lines.Length);
            Assert.StartsWith("  ", lines[3]);
        }
    }
}
