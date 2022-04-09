namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This generates an ordered Markdown list
    /// 1. Item 1
    /// 2. Item 2
    /// </summary>
    public class OrderedList : MarkdownList
    {
        /// <summary>
        /// The starting number for this list.
        /// </summary>
        public int StartingNumber { get; set; }

        /// <summary>
        /// True will emit A,B,C instead of numbers.
        /// The starting number will be used to determine the starting letter.
        /// </summary>
        public bool Lettered { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public OrderedList()
        {
            StartingNumber = 1;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="startingNumber">Starting number for list.</param>
        public OrderedList(int startingNumber)
        {
            StartingNumber = startingNumber;
        }

        /// <summary>
        /// Get the prefix for each list item based on the formatting options and current index.
        /// </summary>
        /// <param name="formatting"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override string GetPrefix(MarkdownFormatting formatting, int index)
        {
            if (Lettered)
            {
                int value = StartingNumber + index;
                string result = "";
                if (value > 26)
                {
                    result += (char)('a' + value / 26 - 1);
                    result += (char)('a' + value % 26 - 1);
                }
                else
                {
                    result += (char)('a' + value - 1);
                }
                return formatting?.OrderedListUsesSequence == true || index == 0 ? $"{result}. " : "a. ";
            }
            else
            {
                int value = formatting?.OrderedListUsesSequence == true
                    ? StartingNumber + index
                    : index == 0
                        ? StartingNumber
                        : 1;
                return $"{value}. ";
            }
        }
    }
}