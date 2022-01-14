using System.Collections.Generic;
using System.IO;

namespace Julmar.GenMarkdown
{
    public class MarkdownDocument : List<MarkdownBlock>
    {
        public virtual void Write(TextWriter writer, MarkdownFormatting formatting = null)
        {
            foreach (var block in this)
            {
                block.Write(writer, formatting);
            }
        }

        public override string ToString()
        {
            var sw = new StringWriter();
            Write(sw);
            var s = sw.ToString();
            return s.Remove(s.Length - 2);
        }
    }
}
