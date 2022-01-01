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

        protected override string GetPrefix(int index) => $"{index + StartingNumber}. ";
    }
}