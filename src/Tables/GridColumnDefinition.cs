using System;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// Column definition for the GridTable.
    /// </summary>
    public sealed class GridColumnDefinition
    {
        /// <summary>
        /// Width in percentage - 1 = 100%, .5 = 50%, etc.
        /// 0 = sized to column.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// The alignment for the column
        /// </summary>
        public ColumnAlignment Alignment { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public GridColumnDefinition()
        {
            Width = 0;
            Alignment = ColumnAlignment.Default;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alignment">Alignment to use</param>
        /// <param name="width">Width (%)</param>
        public GridColumnDefinition(ColumnAlignment alignment, double width = 0)
        {
            if (width < 0 || width > 1) throw new ArgumentOutOfRangeException(nameof(width));
            Alignment = alignment;
            Width = width;
        }
    }
}