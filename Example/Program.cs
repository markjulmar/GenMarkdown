using System;
using Julmar.GenMarkdown;

namespace Example
{
    internal static class Program
    {
        static void Main()
        {
            //MethodExample();
            //ConstructorExample();
            TableExample();
        }

        private static void TableExample()
        {
            var doc = new MarkdownDocument();

            doc.Add(new Table(TableTypes.RowExtension, new[] {ColumnAlignment.Default, ColumnAlignment.Center, ColumnAlignment.Right})
            {
                new()
                {
                    new TableCell(),
                    new Paragraph {Text.Bold("Math")}, new Paragraph {Text.Bold("Science")},
                },
                new()
                {
                    new Paragraph {Text.Bold("John Smith")},
                    "A",
                    "B"
                },
                new()
                {
                    new Paragraph {Text.Bold("Susan Green")},
                    "C",
                    "A"
                },
                new()
                {
                    new Paragraph {Text.Bold("Dave Wilson")},
                    new TableCell("N/A") { ColumnSpan = 2 }
                }
            });


            doc.Write(Console.Out);
        }

        private static void MethodExample()
        {
            var doc = new MarkdownDocument();

            doc.Add(new Header(1, "Example title"));
            doc.Add(new Paragraph());

            var p = (Paragraph) doc[1];
            p.Add("This is some text with ");
            p.Add(Text.Bold("some inline bold text"));
            p.Add(" and ");
            p.Add(Text.Italic("some inline italics"));
            p.Add(new Text("."));

            doc.Add(new Paragraph("Can also have a single paragraph as part of the constructor."));

            doc.Write(Console.Out);

            p.RemoveAt(4);
            p.Add(" which is cool!");

            doc.Write(Console.Out);
        }

        static void ConstructorExample()
        {
            // Markdown documents are composed of blocks which contain inline objects.
            // You can add through Add methods, or directly as part of creation.
            MarkdownDocument doc = new()
            {
                new Header(1, "Example title"),

                new Paragraph
                {
                    "This is some text with ",
                    Text.Bold("some inline bold text"),
                    " and ",
                    Text.Italic("some inline italics"),
                    ". There's an explicit converter from strings to create plain text."
                },

                new Paragraph("Can also have a single paragraph as part of the constructor."),

                new Paragraph
                {
                    new Text("Or use the explicit Text objects to add plain text or "),
                    new BoldText("BoldText"),
                    new Text(" or "),
                    new ItalicText("ItalicText"),
                    new Text(" objects.")
                },

                new Paragraph
                {
                    "Can inline code with the Text.Code helper - for example: ",
                    Text.Code("Program"), " object.",
                    "Or use the explicit ",
                    new InlineCode("InlineCode"), " object."
                },

                new Header(2)
                {
                    "Headers can have ",
                    Text.Bold("inline elements"),
                    " too."
                },
                
                "Here's an image:",
                new Image("Nuget logo", "https://www.nuget.org/Content/gallery/img/logo-header.svg", "A description"),

                "And a link:",
                new Link("www.microsoft.com", "https://microsoft.com"),

                "And a horizontal rule.",
                new HorizontalRule(),

                new Header(2, "Lists"),

                new Paragraph("Here's an example bulleted list."),

                new BulletedList
                {
                    new Paragraph { "Item #1 - ", Text.Link("google.com", "https://google.com"), "." },
                    new Paragraph("Item #2"),
                    {
                        new Paragraph("Item #3"),
                        new Paragraph("With some additional text"),
                        new Image("and an image", "https://www.nuget.org/Content/gallery/img/logo-header.svg")
                    }
                },

                new Paragraph("Here's a numbered list."),

                new NumberedList
                {
                    "One",
                    "Two",
                    "Three"
                },

                "You can also start it at a specific number:",

                new NumberedList(5)
                {
                    "Five",
                    "Six",
                    "Seven"
                },

                new Header(2, "Quotes"),

                "Some block quotes.",

                new BlockQuote("This is a quote."),
                new BlockQuote
                {
                    "This is also a quote.",
                    new Image("Image", "https://www.nuget.org/Content/gallery/img/logo-header.svg")
                },

                new Header(2, "Tables"),

                "And finally, tables.",

                new Table(3)
                {
                    new()
                    {
                        "", new Paragraph { Text.Bold("Math") }, new Paragraph { Text.Bold("Science") },
                    },
                    new()
                    {
                        new Paragraph { Text.Bold("John Smith") },
                        "A",
                        "B"
                    },
                    new()
                    {
                        new Paragraph { Text.Bold("Susan Green") },
                        "C",
                        "A"
                    }
                }
            };

            Console.WriteLine(doc.ToString());
        }
    }
}
