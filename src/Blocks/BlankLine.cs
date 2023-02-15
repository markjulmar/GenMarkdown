using System.IO;

namespace Julmar.GenMarkdown;

/// <summary>
/// This emits a single blank line into the Markdown document. All other block elements
/// always insert an empty line after they emit their contents. This block allows you to insert
/// a blank line intentionally.
/// </summary>
public sealed class BlankLine : MarkdownBlock
{
    /// <summary>
    /// Writes the block to the given TextWriter.
    /// </summary>
    /// <param name="writer">writer</param>
    /// <param name="formatting">optional formatting</param>
    public override void Write(TextWriter writer, MarkdownFormatting formatting)
    {
        writer.WriteLine();
    }
}