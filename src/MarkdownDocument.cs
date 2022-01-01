using System.Collections.Generic;
using System.IO;

namespace Julmar.GenMarkdown
{
    public class MarkdownDocument : List<MarkdownBlock>
    {
        public void Write(TextWriter writer) => ForEach(writer.Write);
        
        public override string ToString()
        {
            var sw = new StringWriter();
            Write(sw);
            return sw.ToString();
        }
    }
}
