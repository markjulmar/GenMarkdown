﻿using System.Collections.Generic;
using System.IO;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This is the base class for all inline markdown elements
    /// </summary>
    public abstract class MarkdownInline
    {
        private Dictionary<string, object> metadata;

        /// <summary>
        /// Optional metadata - this is strictly for the user of the library
        /// to associate bits of data as the Markdown document is being created.
        /// </summary>
        public IDictionary<string, object> Metadata
            => metadata ??= new Dictionary<string, object>();

        /// <summary>
        /// Text being rendered
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        protected MarkdownInline(string text)
        {
            Text = text;
        }

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