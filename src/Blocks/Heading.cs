using System;
using System.IO;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This creates a Markdown heading.
    /// # Heading text
    /// </summary>
    public class Heading : Paragraph
    {
        private int level;

        /// <summary>
        /// Heading level (1-5)
        /// </summary>
        public int Level
        {
            get => level;
            set
            {
                if (value < 1 || value > 5)
                    throw new ArgumentOutOfRangeException(nameof(Level), "Heading level must be between 1 and 5.");
                level = value;
            }
        }

        /// <summary>
        /// Optional ID - rendered as {#id}
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Default constructor - creates an empty H1 header
        /// </summary>
        public Heading() : this(1)
        {
        }

        /// <summary>
        /// Constructor to create an H1 header from a string
        /// </summary>
        /// <param name="text">Text for the heading</param>
        public Heading(string text) : this(1, text)
        {
        }

        /// <summary>
        /// Constructor to create an empty Hn header.
        /// </summary>
        /// <param name="level">Heading level (1-5)</param>
        public Heading(int level)
        {
            Level = level;
        }

        /// <summary>
        /// Constructor to create an Hn heading from a string
        /// </summary>
        /// <param name="level">Heading Level (1-5)</param>
        /// <param name="text">Text for the heading</param>
        public Heading(int level, string text) : base(text)
        {
            Level = level;
        }

        /// <summary>
        /// Writes the block to the given TextWriter.
        /// </summary>
        /// <param name="writer">writer</param>
        /// <param name="formatting">optional formatting</param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            var sw = new StringWriter();
            base.Write(sw, formatting);

            string title = sw.ToString()
                .Replace("\r", "")
                .Replace("\n", " ")
                .TrimEnd();

            if (formatting?.UseAlternateHeaderSyntax == true && Level <= 2)
            {
                writer.WriteLine(title);
                writer.Write(new string( Level == 1 ? '=' : '-', title.Length));
            }
            else
            {
                writer.Write(new string('#', Level) + " " + title);
                if (!string.IsNullOrEmpty(Id))
                    writer.Write(" {#" + Id.Trim() + "}");
            }

            writer.WriteLine();
            writer.WriteLine();
        }
    }
}