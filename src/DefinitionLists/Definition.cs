using System;
using System.IO;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This represents a single definition in a definition list.
    /// </summary>
    public class Definition : MarkdownBlockCollection<string>
    {
        /// <summary>
        /// The term being defined
        /// </summary>
        public string Term { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="term">Term to define</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Definition(string term)
        {
            Term = term ?? throw new ArgumentNullException(nameof(term));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="term">Term to define</param>
        /// <param name="definition">Definition for term</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Definition(string term, string definition)
        {
            if (definition == null) throw new ArgumentNullException(nameof(definition));
            Term = term ?? throw new ArgumentNullException(nameof(term));
            Children.Add(definition);
        }

        /// <summary>
        /// Writes the block to the given TextWriter.
        /// </summary>
        /// <param name="writer">writer</param>
        /// <param name="formatting">optional formatting</param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            writer.Write(Indent);
            writer.WriteLine(Term.Trim());
            foreach (var child in Children)
            {
                writer.Write(Indent);
                writer.WriteLine($": {child.TrimEnd('\r','\n')}");
            }
        }
    }
}