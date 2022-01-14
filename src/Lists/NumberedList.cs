namespace Julmar.GenMarkdown
{
    public class NumberedList : MarkdownList
    {
        public int StartingNumber { get; set; }

        public NumberedList()
        {
            StartingNumber = 1;
        }

        public NumberedList(int startingNumber)
        {
            StartingNumber = startingNumber;
        }

        protected override string GetPrefix(MarkdownFormatting formatting, int index)
        {
            int value = formatting?.NumberedListUsesSequence==true
                ? StartingNumber + index
                : index == 0
                    ? StartingNumber
                    : 1;
            return $"{value}. ";
        }
    }
}