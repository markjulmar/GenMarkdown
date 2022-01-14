# GenMarkdown

.NET Core library to make it easy to generate Markdown with an object graph.

[![Build and Publish GenMarkdown on NuGet](https://github.com/markjulmar/GenMarkdown/actions/workflows/dotnet.yml/badge.svg)](https://github.com/markjulmar/GenMarkdown/actions/workflows/dotnet.yml)

## Supported elements

The library supports all standard Markdown and several extensions including:

- Highlight text
- Superscript text
- Subscript text
- Strikethrough text
- Pipe tables
- Grid tables
- Definition lists
- Task lists

## Basic example

Markdown is constructed with _blocks_ and _inlines_. The `MarkdownDocument` object holds blocks, where each block can contain inlines. All the block objects implement collection semantics so you can use standard `Add` methods, or even constructor initializers.

### Example code

Here's an example document with headers and paragraphs.

```csharp
var doc = new MarkdownDocument
{
    // Level 1 header (# Example title)
    new Header(1, "Example title"),
    
    // Paragraph with some inline bold and italic text
    new Paragraph {
        "This is some text with ", Text.Bold("some inline bold text"), " and ", Text.Italic("some inline italics"),
        ". There's an implicit converter from strings to create plain text.",
    },
    
    "Implicit converter works in the initializer too.",
    new Paragraph("Can also have a single paragraph as part of the constructor."),
    new Paragraph { "Can inline code with the Text.Code helper - for example: ", Text.Code("Program"), " object." }
};
```

This will generate the following output:

```markdown
# Example title

This is some text with **some inline bold text** and _some inline italics_. There's an implicit converter from strings to create plain text.

Implicit converter works in the initializer too.

Can also have a single paragraph as part of the constructor.

Can inline code with the Text.Code helper - for example: `Program` object.

```

The same document can be created through methods:

```csharp
var doc = new MarkdownDocument();
// Level 1 header (# Example title)
doc.Add(new Header(1, "Example title")); // Paragraph with some inline bold and italic text
doc.Add(new Paragraph {
    "This is some text with ", Text.Bold("some inline bold text"), " and ",
    Text.Italic("some inline italics"),
    ". There's an implicit converter from strings to create plain text.",
});
doc.Add("Implicit converter works in the initializer too.");
doc.Add(new Paragraph("Can also have a single paragraph as part of the constructor."));
doc.Add(new Paragraph {
    "Can inline code with the Text.Code helper - for example: ", Text.Code("Program"), " object."
});
```

## Generating Markdown

Once you have created the object graph, you can use the `Write` method to generate the text Markdown content.

```csharp
void Write(TextWriter writer, MarkdownFormatting formatting);
```

The `MarkdownFormatting` object has options to control how Markdown is generated - for example whether to use `_` or `*` for emphasis (italic) inlines.

You can also use the `ToString()` method on any object to generate Markdown, however this approach does not support customization of the markdown through formatting options.

```csharp
var doc = new MarkdownDocument();
// Add things..

// Write to the specified file.
doc.Write(File.OpenWrite("somefile.md"));

// Customize the output
doc.Write(Console.Out, 
    new MarkdownFormatting {
        UseAsterisksForBullets = true,
        UseAsterisksForEmphasis = true,
        NumberedListUsesSequence = true
    });

```

## Modifying the document

The document itself is a writable collection of `MarkdownBlock` objects - you can modify the document at any time by adjusting these objects.

```csharp
var doc = new MarkdownDocument
{
    // Level 1 header (# Example title)
    new Header(1, "Example title"), // Paragraph with some inline bold and italic text
    new Paragraph
    {
        "This is some text with ", Text.Bold("some inline bold text"), " and ",
        Text.Italic("some inline italics"),
        ". There's an implicit converter from strings to create plain text.",
    }
};

// Modify the header to be ## and add an {#main-header} identifier.
var header = (Header) doc[0];
header.Level = 2;
header.Id = "main-header";
```

This produces:

```markdown
## Example title {#main-header}

This is some text with **some inline bold text** and _some inline italics_. There's an implicit converter from strings to create plain text.
```

## Inlines

The library supports several inlines, along with helper methods on the `Text` object to generate the most common types.

| Type | Description |
|------|-------------|
| `BoldText` | Creates bolded text. |
| `Emoji` | Creates an emoji (:xyz:) |
| `HighlightText` | Highlights the specified text with `==`. This is an extension and not supported by all Markdown parsers. |
| `InlineCode` | Displays inline code surrounded by tilde characters. |
| `InlineLink` | Creates an inline link. |
| `ItalicText` | Creates emphasized text. |
| `LineBreak` | Generates a line-break (back slash at end of line). |
| `StrikethroughText` | Strikes the specified text with `~~`. This is an extension and not supported by all Markdown parsers. |
| `SubscriptText` | Subscript text specified with `~`. This is an extension and not supported by all Markdown parsers. |
| `SuperscriptText` | Superscript text specified with `^`. This is an extension and not supported by all Markdown parsers. |
| `Text` | Base text element. There is an implicit converter from `string` to this type. |

### Examples

```csharp
new Paragraph 
{
    "Implicit converter to Text() or can use ",
    new Text("Normal text. Or can use inline types like "),
    new BoldText("BoldText"), " or ", new ItalicText("ItalicText"), "."
};
```

This generates:

```markdown
Implicit converter to Text() or can use Normal text. Or can use inline types like **BoldText** or _ItalicText_.
```

### Helpers

The `Text` type has several helpers to generate the most common types - this removes the need to call `new` inline:

| Helper | Creates |
|---------------|---------|
| `Text.Bold` method | `BoldText` object. |
| `Text.Italic` method | `ItalicText` object. |
| `Text.Code` method | `InlineCode` object. |
| `Text.Link` method | `InlineLink` object. |
| `Text.LineBreak` property | `LineBreak` object. |

```csharp
new Paragraph
{
    "This is an example with ", Text.Bold("bold"), " and ", Text.Italic("italic"), " text.",
    " You can also have inline ", Text.Code("Code"), " or ", Text.Link("links", "https://www.msn.com"), "."
}
```

```output
This is an example with **bold** and _italic_ text. You can also have inline `Code` or [links](https://www.msn.com).
```

## Supported blocks

Here's examples of each supported block type.

### Paragraph

The `Paragraph` is the main block type and represents a paragraph of Markdown text. It can contain any inline content and has implicit conversions from strings.

```csharp
var doc = new MarkdownDocument
{
    "Direct text is turned into a paragraph.",

    new Paragraph
    {
        "This is some text with ", Text.Bold("some inline bold text"), " and ",
        Text.Italic("some inline italics"), ". Now a line break ->",
        Text.LineBreak,
        "There's an explicit converter from strings to create plain text.",
        "You can also escape ", Text.Code("any `code` items used in the file"), "."
    },

    new Paragraph("Can also have a single paragraph as part of the constructor."),

    new Paragraph
    {
        new Text("Or use the explicit Text objects to add plain text or "),
        new BoldText("BoldText"), new Text(" or "), 
        new ItalicText("ItalicText"), new Text(" objects.")
    },

    new Paragraph
    {
        "Can inline code with the Text.Code helper - for example: ",
        Text.Code("Program"), " object.", "Or use the explicit ",
        new InlineCode("InlineCode"), " object."
    },
};
```

This generates:

```markdown
Direct text is turned into a paragraph.

This is some text with **some inline bold text** and _some inline italics_. Now a line break ->\
There's an explicit converter from strings to create plain text.You can also escape ``any `code` items used in the file``.

Can also have a single paragraph as part of the constructor.

Or use the explicit Text objects to add plain text or **BoldText** or _ItalicText_ objects.

Can inline code with the Text.Code helper - for example: `Program` object.Or use the explicit `InlineCode` object.
```

### Header

The `Header` object takes a numeric level (1-5) and generates a Markdown header. It allows inline Markdown as part of the creation and also supports an optional `Id` property which will create the identifier extension on the header (`{#id}`).

```csharp
new Header(1, "Example title"),
"Here some example paragraph text under the title.",
new Header(2) { "Headers can have ", Text.Bold("inline elements"), " too." },
"With some more text.",
new Header(3, "Or identifiers") { Id = "level3-hdr" },
"Fini."
```

Generates:

```markdown
# Example title

Here some example paragraph text under the title.

## Headers can have **inline elements** too.

With some more text.

### Or identifiers {#level3-hdr}

Fini.
```

### Image

The `Image` object creates a Markdown image.

```csharp
new Image("alt-text goes here", "https://avatars.githubusercontent.com/u/5099741?v=4")
```

```output
![alt-text goes here](https://avatars.githubusercontent.com/u/5099741?v=4)
```

### BlockQuote

`BlockQuote` creates quotes (`>`) in the document. It can contain one or more blocks.

```csharp
new BlockQuote("This is a quote.");

// Can contain multiple blocks.
new BlockQuote
{
    "This is also a quote.",
    new Image("Image", "https://www.nuget.org/Content/gallery/img/logo-header.svg")
};

new BlockQuote
{
    "This is a quote.",
    "With multiple lines",
    "And an embedded\r\ncarriage return!"
}
```

```output
> This is a quote.

> This is also a quote.
> ![Image](https://www.nuget.org/Content/gallery/img/logo-header.svg)

> This is a quote.
> With multiple lines
> And an embedded
> carriage return!
```

### CodeBlock

The `CodeBlock` element generates a fenced codeblock with optional language.

```csharp
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
```

Generates

```markdown
```
using namespace System;

namespace Test
{
   public static class Program
   {
       Console.WriteLine("Hello World");
   }
}
```
```

Adding a language will place that onto the code fence.

```csharp
new CodeBlock("csharp")
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

```

```markdown
```csharp
    ... same code as above

```

### HorizontalRule

The `HorizontalRule` block generates a horizontal rule comprised of three dashes.

```csharp
"Starting paragraph",
new HorizontalRule(),
"Ending paragraph"
```

```markdown
Starting paragraph

---

Ending paragraph
```

### Image

The `Image` block creates an embedded image in the document, this includes an alt-tag for accessibility, the URL, and an optional description.

```csharp
"Here's an image:",
new Image("An image representing the NuGet logo", "https://www.nuget.org/Content/gallery/img/logo-header.svg", "A description for the image")
```

```markdown
Here's an image:

![An image representing the NuGet logo](https://www.nuget.org/Content/gallery/img/logo-header.svg "A description for the image")
```

### Link

The `Link` block creates a link to some other content.

```csharp
"An example link:",
new Link("www.microsoft.com", "https://microsoft.com"),
```

```markdown
An example link:

[www.microsoft.com](https://microsoft.com)
```

### Tables

The `Table` block generates standard Markdown tables, sometimes referred to as pipe tables. These are structured rows, each containing a specific number of columns. This table type does not support column or row spanning.

The table is comprised of `TableRow` objects, with each row having one or more `TableCell` objects. The `TableCell` is a block container - so it can contain one or more Markdown blocks. There's also an implicit converter to take a `Paragraph` object and turn it into a `TableCell` to simplify the inline collection syntax.

Here's a simple example of a 3x3 table:

```csharp
new Table(3)
{
    new TableRow
    {
        new TableCell("Header 0"),
        new TableCell("Header 1"),
        new TableCell("Header 2"),
    },
    new TableRow
    {
        new TableCell("(0,0)"),
        new TableCell("(0,1)"),
        new TableCell("(0,2)"),
    },
    new TableRow
    {
        new TableCell("(1,0)"),
        new TableCell("(1,1)"),
        new TableCell("(1,2)"),
    },
    new TableRow
    {
        new TableCell("(2,0)"),
        new TableCell("(2,1)"),
        new TableCell("(2,2)"),
    }
}
```

This generates:

```markdown
|Header 0|Header 1|Header 2|
|---|---|---|
|(0,0)|(0,1)|(0,2)|
|(1,0)|(1,1)|(1,2)|
|(2,0)|(2,1)|(2,2)|
```

You can simplify the creation by relying on the implicit `TableCell` conversion, and using the new targeted-type new support in C#9. This code generates the exact same Markdown:

```csharp
new Table(3)
{
    new() {"Header 0", "Header 1", "Header 2"},
    new() { "(0,0)", "(0,1)", "(0,2)" },
    new() { "(1,0)", "(1,1)", "(1,2)" },
    new() { "(2,0)", "(2,1)", "(2,2)" },
}
```

The `TableCell` can contain any type of `Paragraph` text:

```csharp
new Table(3)
{
    new()
    {
        "", new Paragraph { Text.Bold("Math") }, new Paragraph { Text.Bold("Science") },
    },
    new()
    {
        new Paragraph { Text.Bold("John Smith") }, "A", "B"
    },
    new()
    {
        new Paragraph { Text.Bold("Susan Green") }, "C", "A"
    }
}
```

This generates:

```markdown
||**Math**|**Science**|
|---|---|---|
|**John Smith**|A|B|
|**Susan Green**|C|A|
```

A second constructor allows you to control the column alignment by passing in an array of `ColumnAlignment` values:

```csharp
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
}
```

This changes the second row of the generated markdown to include justification hints:

```markdown
||**Math**|**Science**|
|---|:---:|---:|
|**John Smith**|A|B|
|**Susan Green**|C|A|
```

### Numeric Lists

The `NumberedList` block creates sequenced numeric lists of content. The list itself is a container and can contain different block types including other lists, tables, quotes, etc.

The simplest form of numbered list is text:

```csharp
new NumberedList {"One", "Two", "Three" },
new NumberedList(4) {"Four", "Five", "Six"}, // Can start at a specific number.
new NumberedList {"One", "Two", "Three"}
```

This will generate three lists.

```markdown
1. One
1. Two
1. Three

4. Four
1. Five
1. Six

1. One
1. Two
1. Three
```

Notice that the generator follows Markdown guidelines and emits the sequence "1" for each item unless it has a given starting number. This behavior can be controlled with the `MarkdownFormatting.NumberedListUsesSequence` property:

```csharp
// Same list as above:
doc.Write(Console.Out, new MarkdownFormatting() { NumberedListUsesSequence = true });
```

Will now create:

```markdown
1. One
2. Two
3. Three

4. Four
5. Five
6. Six

1. One
2. Two
3. Three
```

The items in the list are blocks themselves, for example you can have code blocks in the list:

```csharp
new NumberedList
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

```

Which will generate the following Markdown - notice that the blocks are indented to keep the structure:

````markdown
1. Do this:
1. Use the following command to make a new directory named `data`.

   ```bash
   mkdir data
   ```

1. Use the `wget` command to download the dataset.

   ```bash
   wget -P data/ https://raw.githubusercontent.com/MicrosoftDocs/mslearn-data-wrangling-shell/main/NASA-logs-1995.txt
   wget - P data / https://raw.githubusercontent.com/MicrosoftDocs/mslearn-data-wrangling-shell/main/NASA-software-API.txt
   ```

1. Change to the new directory by using the command `cd`.

   ```bash
   cd data
   ```

1. Verify that you have the correct files by using the command `ls`.

   ```bash
   ls
   ```
````

#### A note about indenting

Another way to keep bits of Markdown together is through the `IndentLevel` property. This is on every `MarkdownBlock`-derived object and allows you to force an element to be indented in the generated structure. This can be useful if an item is added into the document in between two lists which are sequentially tied together. For example, take the following code:

```csharp
var doc = new MarkdownDocument
{
    new NumberedList
    {
        "Item #1",
        "Item #2"
    },

    new BlockQuote("A quote is here"),

    new NumberedList(3)
    {
        "Item #3",
        "Item #4"
    }
};
```

The intent here is to have a 1-4 sequence, however the block quote in the middle will break the sequence and not be technically part of the list itself. This will generate the following markdown:

```markdown
1. Item #1
1. Item #2

> A quote is here

3. Item #3
1. Item #4
```

This isn't _wrong_, but it will not look quite right as the quote block won't be indented. One way to fix it would be to include it into the numbered list directly as a sibling of "Item #2":

```csharp
new NumberedList
{
    "Item #1",
    {
        "Item #2",
        new BlockQuote("A quote is here")
    }
},
```

However, another way to solve this is to set the `IndentLevel` onto the block quote:

```csharp
var doc = new MarkdownDocument
{
    new NumberedList
    {
        "Item #1",
        "Item #2"
    },

    // Set the indent level to '1' to push this under the list.
    new BlockQuote("A quote is here") { IndentLevel = 1},

    new NumberedList(3)
    {
        "Item #3",
        "Item #4"
    }
};
```

This will change the Markdown to essentially space in the quote block - giving us the syntax we want.

```markdown
1. Item #1
1. Item #2

   > A quote is here

3. Item #3
1. Item #4
```

The `IndentLevel` property must be between 0 and 3 and each level will push the block further into the content. It's set to zero by default (no indentation).

### Bullet lists

The `BulletedList` is identical to the `NumberedList` except it doesn't sequence items, but instead prefaces them with a dash to create a bulleted list. Here's an example:

```csharp
new BulletedList
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
```

This generates

````markdown
- Item #1 - [google.com](https://google.com).
- Item #2
- Item #2.5

  With some additional text

  ![and an image](https://www.nuget.org/Content/gallery/img/logo-header.svg)

  ```
  using namespace System;

  namespace Test
  {
     public static class Program
     {
         Console.WriteLine("Hello World");
     }
  }
  ```

- Item #3

  > This is a quote.
  > With multiple lines
  > And an embedded
  > carriage return!
````

The default bullet character is the dash, however you can change that to an asterisk with the `MarkdownFormatting.UseAsterisksForBullets` property when using the formatted `Write` method.

## TaskList

The `TaskList` block will create a checklist of tasks in Markdown. This is an extension and not supported by all Markdown parsers. Each task is represented by the `TaskItem` object and a list is simply a collection of these items. The `TaskItem` has a `IsChecked` property which can be set to indicate a checkmark. This value can also be passed as a constructor parameter as shown below.

```csharp
new TaskList()
{
    new TaskItem("Item #1"),
    new TaskItem("Item #2", isChecked: true),
    new TaskItem()
    {
        "This is a ", Text.Bold("Bold"), " choice."
    },
    new TaskItem("Final shot!")
};
```

This generates:

```markdown
- [ ] Item #1
- [x] Item #2
- [ ] This is a **Bold** choice.
- [ ] Final shot!
```

### Definition lists

The `DefinitionList` block creates definition lists in Markdown. This is an extension and not supported by all Markdown parsers. It consists of a term and one or more definitions. The term and definitions can only be string types - inline and Markdown blocks are not supported for this element.

```csharp
new DefinitionList
{
    new Definition("First term", "This is a definition"),
    new Definition("Second term")
    {
        "First definition",
        "Second definition"
    }
},
```

This generates:

```markdown
First term
: This is a definition

Second term
: First definition
: Second definition
```
