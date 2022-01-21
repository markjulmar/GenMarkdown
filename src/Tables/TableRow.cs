using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This is a single row in a Markdown table.
    /// </summary>
    public class TableRow : IList<TableCell>
    {
        private readonly List<TableCell> cells = new();

        /// <summary>
        /// Constructor
        /// </summary>
        public TableRow()
        {
        }

        /// <summary>
        /// Constructor which takes a set of cells as this row
        /// </summary>
        /// <param name="cells"></param>
        public TableRow(params MarkdownBlock[] cells)
        {
            if (cells == null) throw new ArgumentNullException(nameof(cells));
            if (cells.Length == 0) throw new ArgumentNullException(nameof(cells));

            this.AddRange(cells.Select(c => c == null ? null : new TableCell(c)));
        }

        /// <summary>
        /// Constructor which creates an empty row of a set of cells.
        /// </summary>
        /// <param name="columns">Number of columns in this row</param>
        public TableRow(int columns)
        {
            for (int i = 0; i < columns; i++)
                this.Add(new TableCell());            
        }

        /// <summary>
        /// This adds a new cell to the row
        /// </summary>
        /// <param name="block"></param>
        public void Add(MarkdownBlock block) => this.Add(block == null?null:new TableCell(block));

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<TableCell> GetEnumerator() => cells.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)cells).GetEnumerator();

        /// <summary>
        /// Add a new cell to the row.
        /// </summary>
        /// <param name="item">Cell to add</param>
        public void Add(TableCell item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            cells.Add(item);
        }

        /// <summary>
        /// Add a set of cells to the row.
        /// </summary>
        /// <param name="addedCells">cells to add</param>
        public void AddRange(IEnumerable<TableCell> addedCells)
        {
            if (addedCells == null) throw new ArgumentNullException(nameof(addedCells));

            var e = addedCells.ToList();
            if (e.Any(c => c == null)) throw new ArgumentNullException(nameof(addedCells));
            this.cells.AddRange(e);
        }

        /// <summary>
        /// Clear all cells from the row.
        /// </summary>
        public void Clear() => cells.Clear();

        /// <summary>
        /// Returns true if the given cell is in this row.
        /// </summary>
        /// <param name="item">Cell to look for</param>
        /// <returns>True/False if it exists in this row</returns>
        public bool Contains(TableCell item) => cells.Contains(item);

        /// <summary>
        /// Copy the given cells to the given array.
        /// </summary>
        /// <param name="array">Array to copy into</param>
        /// <param name="arrayIndex">index to start at</param>
        void ICollection<TableCell>.CopyTo(TableCell[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            cells.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Remove the specified cell from the row.
        /// </summary>
        /// <param name="item">Cell to remove</param>
        /// <returns>True if the cell was located and removed.</returns>
        public bool Remove(TableCell item) => cells.Remove(item);

        /// <summary>
        /// Returns the count of cells in this row
        /// </summary>
        public int Count => cells.Count;

        /// <summary>
        /// True if this is a read-only list.
        /// </summary>
        bool ICollection<TableCell>.IsReadOnly => false;

        /// <summary>
        /// Returns the index of the given cell in this row, or -1 if not found.
        /// </summary>
        /// <param name="item">Cell to look for</param>
        /// <returns>Index of the found cell, or -1 if not found</returns>
        public int IndexOf(TableCell item) => cells.IndexOf(item);

        /// <summary>
        /// Insert a cell at a given index.
        /// </summary>
        /// <param name="index">Index to insert at</param>
        /// <param name="item">Cell to insert</param>
        public void Insert(int index, TableCell item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (index <= 0) throw new ArgumentOutOfRangeException(nameof(index));
            cells.Insert(index, item);
        }

        /// <summary>
        /// Remove the cell at the given index.
        /// </summary>
        /// <param name="index">Index to remove</param>
        public void RemoveAt(int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            cells.RemoveAt(index);
        }

        /// <summary>
        /// Get the cell at the given index.
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Cell</returns>
        public TableCell this[int index]
        {
            get => cells[index];
            set => cells[index] = value;
        }
    }
}