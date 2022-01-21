using System.Collections.Generic;
using System.IO;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This holds all the Markdown content for a document.
    /// </summary>
    public class MarkdownDocument : List<MarkdownBlock>
    {
        /// <summary>
        /// Write the document out in a structured Markdown format.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="formatting"></param>
        public virtual void Write(TextWriter writer, MarkdownFormatting formatting = null)
        {
            ForEach(b => b.Write(writer, formatting));
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var sw = new StringWriter();
            Write(sw);
            var s = sw.ToString();
            return s.Remove(s.Length - 2);
        }
    }
}
