using System.IO;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This is the base class for all inline markdown elements
    /// </summary>
    public abstract class MarkdownInline
    {
        /// <summary>
        /// Text being rendered
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Operator to convert a string into Markdown text wrapper.
        /// </summary>
        /// <param name="s"></param>
        public static implicit operator MarkdownInline(string s) => new Text(s);

        /// <summary>
        /// Writes the text as markdown.
        /// </summary>
        /// <param name="writer">Text writer to write object to</param>
        /// <param name="formatting">Optional formatting information</param>
        public virtual void Write(TextWriter writer, MarkdownFormatting formatting) => writer.Write(Text);
        
        /// <summary>
        /// Render the object as Markdown text
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sw = new StringWriter();
            Write(sw, null);
            return sw.ToString();
        }
    }
}