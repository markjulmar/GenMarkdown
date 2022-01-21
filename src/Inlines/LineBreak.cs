using System;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// Renders a line break
    /// </summary>
    public class LineBreak : Text
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LineBreak() : base(@"\" + Environment.NewLine) { }
    }
}
