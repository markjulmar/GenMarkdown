# GenMarkdown

.NET Core library to make it easy to generate Markdown with an object graph.

## Example code

```csharp
// Markdown documents are composed of blocks which contain inline objects.
// You can add through Add methods, or directly as part of creation.
MarkdownDocument doc = new()
{
    new Header(1, "Title"),
    new Paragraph
    {
        "This is some text with ",
        Text.Bold("some inline bold text"),
        " and ",
        Text.Italic("some inline italics"),
        "."
    },
    new Paragraph
    {
        "Make sure to check out the ",
        Text.Code("Program"), " object."
    },
    new Header(2, "Bulleted list"),
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
    new Image("Some alt text", "../media/image1.png", "A description"),
    new Link("www.microsoft.com", "https://microsoft.com"),
    new NumberedList(4)
    {
        "One",
        "Two",
        "Three"
    },
    new BlockQuote("This is a quote."),
    new BlockQuote
    {
        "This is also a quote.",
        new Image("Image", "../media/image3.png")
    },
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
# Title

This is some text with **some inline bold text** and _some inline italics_.
Make sure to check out the `Program` object.
## Bulleted list

* Item #1 - [google.com](https://google.com).
* Item #2
* Item #3
  With some additional text
  ![and an image](../media/image2.png)

![Some alt text](../media/image1.png "A description")
[www.microsoft.com](https://microsoft.com)
4. One
5. Two
6. Three

> This is a quote.

> This is also a quote.
> ![Image](../media/image3.png)

| |**Math** |**Science** |
|-|-|-|
|**John Smith** |A |B |
|**Susan Green** |C |A |
```

# Title

This is some text with **some inline bold text** and _some inline italics_.
Make sure to check out the `Program` object.
## Bulleted list

* Item #1 - [google.com](https://google.com).
* Item #2
* Item #3
  With some additional text
  ![and an image](../media/image2.png)

![Some alt text](../media/image1.png "A description")
[www.microsoft.com](https://microsoft.com)
4. One
5. Two
6. Three

> This is a quote.

> This is also a quote.
> ![Image](../media/image3.png)

| |**Math** |**Science** |
|-|-|-|
|**John Smith** |A |B |
|**Susan Green** |C |A |