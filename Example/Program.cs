using System;
using Julmar.GenMarkdown;

namespace Example
{
    internal static class Program
    {
        static void Main()
        {
            Run("Table Example", TableExample);
            Run("Pretty Table Example", TableExample, new MarkdownFormatting() { PrettyPipeTables = true });
            Run("Grid Table", GridTableExample);
            Run("Column-spanned grid table", GridTableColumnSpanExample);
            Run("Code Block", ExampleCodeBlock);
            Run("Code Block w/Format", ExampleCodeBlock, new MarkdownFormatting() { UseIndentsForCodeBlocks = true });
            Run("ListWithEmbeddedCodeBlocks", ListWithEmbeddedCodeBlocks);
            Run("MultipleLists", MultipleLists);
            Run("NestedList", NestedList);
            Run("TaskList", TaskList);
            Run("DefinitionList", DefinitionList);
            Run("ModifiedDocument", ModifiedDocument);
            Run("Create a document", FullDocument);
            Run("Nested Blockquote", NestedBlockQuote);
            Run("Complex quoteblock", ComplexQuoteBlock);
        }

        static void Run(string header, Func<MarkdownDocument> method, MarkdownFormatting formatting = null)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("-------");
            Console.WriteLine(header);
            Console.WriteLine("-------");
            Console.WriteLine();

            method().Write(Console.Out, formatting);

            Console.WriteLine();
            Console.WriteLine("Press [ENTER]");
            Console.ReadLine();
        }

        private static MarkdownDocument GridTableColumnSpanExample()
        {
            var table = new GridTable(true,
                new GridColumnDefinition(ColumnAlignment.Left),
                new GridColumnDefinition(ColumnAlignment.Center),
                new GridColumnDefinition(ColumnAlignment.Right, .5))
            {
                // Header
                new()
                {
                    new TableCell(Text.Bold("Student")),
                    new TableCell(Text.Bold("Class")),
                    new TableCell(Text.Bold("Grade"))
                },

                // Rows
                new() {"Johnny Miller Smith", "Mathmatics", "A"},
                new() {"Johnny Miller Smith", "English", "A"},
                new() {"Johnny Miller Smith", "Science", "A"},

                // Sep
                new() {new TableCell {ColumnSpan = 3}},

                // Rows
                new() {"David Joseph Brown", "Mathmatics", "B"},
                new() {"David Joseph Brown", "English", "C"},
                new() {"David Joseph Brown", "Science", "A"},

                // Sep
                new() {new TableCell {ColumnSpan = 3}},

                // Rows
                new() {"Susan Deloris Green", "Mathmatics", "B"},
                new() {"Susan Deloris Green", "English", "C"},
                new() {"Susan Deloris Green", "Science", "A"},
            };

            var table2 = new GridTable(false, 3)
            {
                new()
                {
                    new TableCell(Text.Bold("Name")), new TableCell("John Smith") { ColumnSpan = 2 }
                },
                new()
                {
                    new TableCell(Text.Bold("Phone")), "214", "555-1212"
                },
                new()
                {
                    new TableCell() { ColumnSpan = 3 }
                },
                new()
                {
                    new TableCell("I accept all damages included:") { ColumnSpan = 2 }, "Yes/No"
                }

            };

            return new MarkdownDocument { table, new HorizontalRule(), table2 };
        }

        private static MarkdownDocument ComplexQuoteBlock()
        {
            return new MarkdownDocument
            {
                new BlockQuote
                {
                    new Heading(4, "The quarterly results look great!"),
                    "",
                    new List
                    {
                        "Revenue was off the chart.",
                        "Profits were higher than ever."
                    },
                    "",
                    new Paragraph
                    {
                        Text.Italic("Everything"), " is going according to ", Text.Bold("plan"), "."
                    }
                }
            };
        }

        private static MarkdownDocument GridTableExample()
        {
            string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var table = new GridTable(3)
            {
                new()
                {
                    new TableCell(),
                    new Paragraph {Text.Bold("Math")},
                    new Paragraph {Text.Bold("Science")},
                },
                new()
                {
                    new Paragraph {Text.Bold("John Smith")},
                    alpha,
                    "B"
                },
                new()
                {
                    new Paragraph {Text.Bold("Susan Green")},
                    "C",
                    alpha + alpha + alpha
                },
            };

            table.HasHeader = true;
            return new MarkdownDocument {table};
        }


        private static MarkdownDocument NestedBlockQuote()
        {
            return new MarkdownDocument
            {
                new BlockQuote
                {
                    "Dorothy followed her through many of the beautiful rooms in her castle.",
                    "",
                    new BlockQuote
                    {
                        "The Witch bade her clean the pots and kettles and sweep the floor and keep the fire fed with wood."
                    }
                }
            };
        }

        private static MarkdownDocument NestedList()
        {
            return new MarkdownDocument
            {
                new List
                {
                    "One",
                    {
                        "Two",
                        new List
                        {
                            "Sub-entry 1",
                            "Sub-entry 2"
                        }
                    },
                    "Three"
                }
            };
        }

        private static MarkdownDocument DefinitionList()
        {
            return new MarkdownDocument
            {
                new DefinitionList
                {
                    new("First term", "This is a definition"),
                    new("Second term")
                    {
                        "First definition",
                        "Second definition"
                    }
                },

                new List
                {
                    {
                        "Definition list in a bulleted list:",
                        new DefinitionList
                        {
                            new("First term", "This is a definition"),
                            new("Second term", "An explanation")
                            {
                                "And another one."
                            }
                        }
                    },
                    "Bullet #2"
                }
            };
        }

        private static MarkdownDocument TaskList()
        {
            return new MarkdownDocument
            {
                new TaskList
                {
                    new("Item #1"),
                    new("Item #2", isChecked: true),
                    new()
                    {
                        "This is a ", Text.Bold("Bold"), " choice."
                    },
                    new("Final shot!")
                }
            };
        }

        private static MarkdownDocument MultipleLists()
        {
            return new MarkdownDocument
            {
                new OrderedList {"One", "Two", "Three" },
                new OrderedList(4) {"Four", "Five", "Six"},
                new OrderedList {"One", "Two", "Three"}
            };
        }

        private static MarkdownDocument ListWithEmbeddedCodeBlocks()
        {
            var doc = new MarkdownDocument();

            var list = new OrderedList
            {
                "Do this:",
                {
                    new Paragraph
                    {
                        "Use the following command to make a new directory named ",
                        Text.Code("data"),
                        "."
                    },
                    new CodeBlock("bash", "mkdir data")
                },
                {
                    new Paragraph
                    {
                        "Use the ", Text.Code("wget"), " command to download the dataset.",
                    },
                    new CodeBlock("bash")
                    {
                        "wget -P data/ https://raw.githubusercontent.com/MicrosoftDocs/mslearn-data-wrangling-shell/main/NASA-logs-1995.txt\r\n",
                        "wget - P data / https://raw.githubusercontent.com/MicrosoftDocs/mslearn-data-wrangling-shell/main/NASA-software-API.txt"
                    }
                },
                {
                    new Paragraph
                    {
                        "Change to the new directory by using the command ", Text.Code("cd"), "."
                    },
                    new CodeBlock("bash", "cd data")
                },
                {
                    new Paragraph
                    {
                        "Verify that you have the correct files by using the command ", Text.Code("ls"), "."
                    },
                    new CodeBlock("bash", "ls")
                }
            };

            doc.Add(list);
            doc.Add("You should see a `NASA-software-API.txt` file and a `NASA-logs-1995.txt` file.");
            doc.Add("## This is an ending paragraph.");

            return doc;
        }

        private static MarkdownDocument ExampleCodeBlock()
        {
            return new MarkdownDocument
            {
                new CodeBlock
                {
                    "using namespace System;\r\n",
                    "\r\n",
                    "namespace Test\r\n",
                    "{\r\n",
                    "   public static class Program\r\n",
                    "   {\r\n",
                    "       Console.WriteLine(\"Hello World\");\r\n",
                    "   }\r\n",
                    "}\r\n",
                }
            };
        }

        private static MarkdownDocument TableExample()
        {
            return new MarkdownDocument
            {
                new Heading(1, "Heading"),
                "Welcome to the document with a table.",
                new Table(new[] {ColumnAlignment.Default, ColumnAlignment.Center, ColumnAlignment.Right})
                {
                    new()
                    {
                        new TableCell(),
                        new Paragraph {Text.Bold("Math")}, 
                        new Paragraph {Text.Bold("Science")},
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
                },

                "End of the table",
            };
        }

        /*
        private static void ColumnSpanExample()
        {
            var doc = new MarkdownDocument();

            doc.Add(new Heading(2, "Heading"));
            doc.Add("Welcome to the document with a table.");

            doc.Add(new DocxTable(new[] { ColumnAlignment.Default, ColumnAlignment.Center, ColumnAlignment.Right })
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

            doc.Add("End of the table");
            doc.Add(new Heading(2, "Start of a new header"));
            doc.Add("End of the document.");

            doc.Write(Console.Out);
        }
        */

        private static MarkdownDocument ModifiedDocument()
        {
            var doc = new MarkdownDocument
            {
                new Heading(1, "Example title") {Id = "main"},
                new Paragraph()
            };

            var p = (Paragraph) doc[1];
            p.Add("This is some text with ");
            p.Add(Text.Bold("some inline bold text"));
            p.Add(" and ");
            p.Add(Text.Italic("some inline italics"));
            p.Add(new Text("."));

            doc.Add(new Paragraph("Can also have a single paragraph as part of the constructor."));

            p.RemoveAt(4);
            p.Add(" which is cool!");

            return doc;
        }

        static MarkdownDocument FullDocument()
        {
            return new()
            {
                new Heading(1, "Example title"),

                new Paragraph
                {
                    "This is some text with ",
                    Text.Bold("some inline bold text"),
                    " and ",
                    Text.Italic("some inline italics"),
                    ". Now a line break ->",
                    Text.LineBreak,
                    "There's an explicit converter from strings to create plain text.",
                    "You can also escape ", Text.Code("any `code` items used in the file"), "."
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

                new Heading(2)
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

                new Heading(2, "Lists"),

                new Paragraph("Here's an example bulleted list."),

                new List
                {
                    new Paragraph { "Item #1 - ", Text.Link("google.com", "https://google.com"), "." },
                    new Paragraph("Item #2"),
                    {
                        new Paragraph("Item #2.5"),
                        new Paragraph("With some additional text"),
                        new Image("and an image", "https://www.nuget.org/Content/gallery/img/logo-header.svg"),
                        new CodeBlock
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
                        }
                    },
                    {
                        new Paragraph("Item #3"),
                        new BlockQuote
                        {
                            "This is a quote.",
                            "With multiple lines",
                            "And an embedded\r\ncarriage return!"
                        }
                    },
                },

            new Paragraph("Here's a numbered list."),

                new OrderedList
                {
                    "One",
                    "Two",
                    "Three"
                },

                "You can also start it at a specific number:",

                new OrderedList(5)
                {
                    "Five",
                    "Six",
                    "Seven"
                },

                new Paragraph("Another line in the list.") { IndentLevel = 1 },

                new CodeBlock("bash", "ls -la\r\ncd /\r\n") { IndentLevel = 1 },

                new Heading(2, "Quotes"),

                "Some block quotes.",

                new BlockQuote("This is a quote."),
                new BlockQuote
                {
                    "This is also a quote.",
                    new Image("Image", "https://www.nuget.org/Content/gallery/img/logo-header.svg")
                },

                new Heading(2, "Tables"),

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
        }
    }
}
