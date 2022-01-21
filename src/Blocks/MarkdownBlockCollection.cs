using System.Collections;
using System.Collections.Generic;

namespace Julmar.GenMarkdown
{
    /// <summary>
    /// This is a MarkdownBlock which can hold children.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MarkdownBlockCollection<T> : MarkdownBlock, IList<T> where T : class
    {
        /// <summary>
        /// The block children - each block is rendered with a preceding quote character
        /// </summary>
        protected readonly List<T> Children = new();

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator() => Children.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Children).GetEnumerator();
        
        /// <summary>
        /// Add a new child to the collection.
        /// </summary>
        /// <param name="item">Item to add</param>
        public void Add(T item) => Children.Add(item);
        
        /// <summary>
        /// Clear all children from the collection.
        /// </summary>
        public void Clear() => Children.Clear();
        
        /// <summary>
        /// Returns whether the given item is in our child collection.
        /// </summary>
        /// <param name="item">Item to look for</param>
        /// <returns></returns>
        public bool Contains(T item) => Children.Contains(item);
        
        /// <summary>
        /// Copy the collection of children to an array
        /// </summary>
        /// <param name="array">Array to copy to</param>
        /// <param name="arrayIndex">Starting index</param>
        void ICollection<T>.CopyTo(T[] array, int arrayIndex) => Children.CopyTo(array, arrayIndex);

        /// <summary>
        /// Remove the item from the child collection
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <returns>True if item was removed.</returns>
        public bool Remove(T item) => Children.Remove(item);
        
        /// <summary>
        /// Count of children
        /// </summary>
        public int Count => Children.Count;
        
        /// <summary>
        /// True/False if this is a readonly collection.
        /// </summary>
        bool ICollection<T>.IsReadOnly => false;
        
        /// <summary>
        /// Returns the index of the specified child item.
        /// </summary>
        /// <param name="item">Item to look for</param>
        /// <returns>Zero-based index, or -1 if not found.</returns>
        public int IndexOf(T item) => Children.IndexOf(item);
        
        /// <summary>
        /// Inserts a new child into the collection.
        /// </summary>
        /// <param name="index">Zero-based insertion position</param>
        /// <param name="item">Item to insert.</param>
        public void Insert(int index, T item) => Children.Insert(index, item);
        
        /// <summary>
        /// Removes a specific child by index.
        /// </summary>
        /// <param name="index">Index to remove at</param>
        public void RemoveAt(int index) => Children.RemoveAt(index);
        
        /// <summary>
        /// Gets or sets an item at a given index.
        /// </summary>
        /// <param name="index">Zero-based index</param>
        /// <returns>Item</returns>
        public T this[int index]
        {
            get => Children[index];
            set => Children[index] = value;
        }
    }
}