using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// Basic markdown text
    /// </summary>
    public class Text : MarkdownInline
    {
        /// <summary>
        /// Internal flag to turn off escaped character support.
        /// This needs to be done for code blocks
        /// </summary>
        protected internal bool checkForEscapedCharacters;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">Text</param>
        public Text(string text) : base(text)
        {
            checkForEscapedCharacters = true;
        }

        /// <summary>
        /// Creates an inline Bold text element
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>BoldText object</returns>
        public static BoldText Bold(string text) => new(text);

        /// <summary>
        /// Creates an inline BoldItalic text element
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>BoldItalicText object</returns>
        public static BoldItalicText BoldItalic(string text) => new(text);

        /// <summary>
        /// Creates an inline Italic text element
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>ItalicText object</returns>
        public static ItalicText Italic(string text) => new(text);

        /// <summary>
        /// Creates an inline code text element
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>InlineCode object</returns>
        public static InlineCode Code(string text) => new(text);

        /// <summary>
        /// Creates an inline link text element
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="url">Link url</param>
        /// <param name="title">Optional title</param>
        /// <returns>InlineLink object</returns>
        public static InlineLink Link(string text, string url, string title = null) => new(text, url, title);

        /// <summary>
        /// Creates an inline line break element
        /// </summary>
        /// <returns>LineBreak object</returns>
        public static LineBreak LineBreak => new();

        /// <summary>
        /// Writes the text as markdown.
        /// </summary>
        /// <param name="writer">Text writer to write object to</param>
        /// <param name="formatting">Optional formatting information</param>
        public override void Write(TextWriter writer, MarkdownFormatting formatting) =>
            writer.Write(checkForEscapedCharacters ? EscapeReservedCharacters(Text, formatting) : Text);

        /// <summary>
        /// Cleans the text of possible misinterpretations of Markdown.
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="formatting">Formatting information</param>
        /// <returns>Cleaned text</returns>
        protected static string EscapeReservedCharacters(string text, MarkdownFormatting formatting)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = EscapeUrls(text);
                text = EscapeEmphasis(text, '_', formatting?.EscapeAllIntrawordEmphasis == true);
                text = EscapeEmphasis(text, '*', formatting?.EscapeAllIntrawordEmphasis == true);
            }

            return text;
        }

        private static bool IsPunctuation(char? ch, char start) =>
            ch != start && ch != null && char.IsPunctuation(ch.Value);

        private static string EscapeEmphasis(string text, char start, bool escapeAll)
        {
            int npos = text.IndexOf(start);
            while (npos >= 0)
            {
                bool opensRun = escapeAll;

                if (!opensRun)
                {
                    char? before = npos > 0 ? text[npos - 1] : null;
                    char? after = npos+1 < text.Length ? text[npos + 1] : null;

                    // A right-flanking delimiter run is a delimiter run that is (1) not preceded by Unicode whitespace,
                    // and either(2a) not preceded by a Unicode punctuation character, or (2b) preceded by a Unicode
                    // punctuation character and followed by Unicode whitespace or a Unicode punctuation character.
                    bool rfr = before != null && before != ' '
                           && (!IsPunctuation(before, start) ||
                               (IsPunctuation(before, start)
                                && (after == ' ' || IsPunctuation(after, start))));

                    // A left-flanking delimiter run is a delimiter run that is (1) not followed by Unicode whitespace,
                    // and either (2a) not followed by a Unicode punctuation character, or (2b) followed by a Unicode
                    // punctuation character and preceded by Unicode whitespace or a Unicode punctuation character.
                    bool lfr = after != null && after != ' '
                         && (!IsPunctuation(after, start) ||
                             (IsPunctuation(after, start)
                              && (before == ' ' || IsPunctuation(before, start))));

                    // A single * character can open emphasis iff (if and only if)
                    // it is part of a left-flanking delimiter run
                    if (start == '*' && lfr)
                        opensRun = true;

                    // A single _ character can open emphasis if it is part of a left-flanking delimiter run and
                    // either(a) not part of a right - flanking delimiter run or(b) part of a right-flanking delimiter run
                    // preceded by a Unicode punctuation character.
                    else if (start == '_' && lfr && !rfr || (rfr && char.IsPunctuation(before.Value)))
                        opensRun = true;

                    // Special case double characters with no end.
                    if (opensRun && (before == start && text.IndexOf(start, npos + 1) == -1) 
                        || (after == start && text.IndexOf(start, npos + 2) == -1))
                        opensRun = false;
                }

                if (opensRun)
                {
                    text = text.Remove(npos, 1).Insert(npos, @$"\{start}");
                    npos++;
                }

                npos = npos < text.Length ? text.IndexOf(start, npos + 1) : -1;
            }
            return text;
        }

        private static string EscapeUrls(string text)
        {
            const char start = '<';
            const char end = '>';

            int npos = text.IndexOf(start);
            while (npos >= 0)
            {
                npos++;
                int epos = text.IndexOf(end, npos);
                if (epos > npos)
                {
                    string test = text.Substring(npos, epos - npos);
                    if (test.Trim().Length > 0 && !test.Contains(' '))
                    {
                        text = text.Remove(npos-1, 1).Insert(npos-1, @$"\{start}");
                    }

                    npos++;
                }
                npos = text.IndexOf(start, npos+1);
            }
            return text;

        }
    }
}