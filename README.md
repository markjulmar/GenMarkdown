# GenMarkdown

.NET Core library to make it easy to generate Markdown with an object graph.

[![Build and Publish GenMarkdown on NuGet](https://github.com/markjulmar/GenMarkdown/actions/workflows/dotnet.yml/badge.svg)](https://github.com/markjulmar/GenMarkdown/actions/workflows/dotnet.yml)

## Example code

```csharp
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

    "Here's an image:",
    new Image("Some alt text", "../media/image1.png", "A description"),

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
            new Image("and an image", "../media/image2.png")
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
        new Image("Image", "../media/image3.png")
    },

    new Header(2, "Tables"),

    "And finally, tables.",

    new Table
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

// Can use ToString
Console.WriteLine(doc.ToString());

// Or write it out with a stream
doc.Write(new StreamWriter(@"./out.md"))
```

## Output

```markdown
# Example title

This is some text with **some inline bold text** and _some inline italics_. There's an explicit converter from strings to create plain text.

Can also have a single paragraph as part of the constructor.

Or use the explicit Text objects to add plain text or **BoldText** or _ItalicText_ objects.

Can inline code with the Text.Code helper - for example: `Program` object.Or use the explicit `InlineCode` object.

Here's an image:

![Some alt text](../media/image1.png "A description")

And a link:

[www.microsoft.com](https://microsoft.com)

And a horizontal rule.

---

## Lists

Here's an example bulleted list.

* Item #1 - [google.com](https://google.com).
* Item #2
* Item #3
  With some additional text
  ![and an image](../media/image2.png)

Here's a numbered list.

1. One
2. Two
3. Three

You can also start it at a specific number:

5. Five
6. Six
7. Seven

## Quotes

Some block quotes.

> This is a quote.

> This is also a quote.
> ![Image](../media/image3.png)

## Tables

And finally, tables.

| |**Math** |**Science** |
|-|-|-|
|**John Smith** |A |B |
|**Susan Green** |C |A |
```

# Example title

This is some text with **some inline bold text** and _some inline italics_. There's an explicit converter from strings to create plain text.

Can also have a single paragraph as part of the constructor.

Or use the explicit Text objects to add plain text or **BoldText** or _ItalicText_ objects.

Can inline code with the Text.Code helper - for example: `Program` object.Or use the explicit `InlineCode` object.

Here's an image:

![Some alt text](../media/image1.png "A description")

And a link:

[www.microsoft.com](https://microsoft.com)

And a horizontal rule.

---

## Lists

Here's an example bulleted list.

* Item #1 - [google.com](https://google.com).
* Item #2
* Item #3
  With some additional text
  ![and an image](../media/image2.png)

Here's a numbered list.

1. One
2. Two
3. Three

You can also start it at a specific number:

5. Five
6. Six
7. Seven

## Quotes

Some block quotes.

> This is a quote.

> This is also a quote.
> ![Image](../media/image3.png)

## Tables

And finally, tables.

| |**Math** |**Science** |
|-|-|-|
|**John Smith** |A |B |
|**Susan Green** |C |A |
