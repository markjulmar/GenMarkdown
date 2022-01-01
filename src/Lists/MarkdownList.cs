using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Julmar.GenMarkdown
{
    public abstract class MarkdownList : MarkdownBlock, IEnumerable<MarkdownBlock>
    {
        private readonly List<List<MarkdownBlock>> blocks = new();

        protected abstract string GetPrefix(int index);

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < blocks.Count; i++)
            {
                var block = blocks[i];
                string prefix = GetPrefix(i);
                foreach (var item in block)
                {
                    sb.Append(prefix);
                    prefix = new string(' ', prefix.Length);
                    sb.Append(item);
                }
            }

            return sb.ToString();
        }

        public void Add(MarkdownBlock item) => blocks.Add(new List<MarkdownBlock> { item });
        public void Add(params MarkdownBlock[] items) => blocks.Add(new List<MarkdownBlock>(items));
        public void Clear() => blocks.Clear();
        public bool Contains(MarkdownBlock item) => this.FirstOrDefault(c => c == item) != null;
        public bool Remove(MarkdownBlock item) => blocks.Any(items => items.Remove(item));
        public int Count => blocks.Sum(mb => mb.Count);
        public int IndexOf(MarkdownBlock item)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                int pos = blocks[i].IndexOf(item);
                if (pos >= 0)
                    return pos + i;
            }

            return -1;
        }

        private bool GetBlock(int index, out List<MarkdownBlock> mbo, out int startingPos)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                for (int x = 0; x < blocks[i].Count; x++)
                {
                    if (i+x == index)
                    {
                        mbo = blocks[i];
                        startingPos = x;
                        return true;
                    }
                }
            }

            mbo = null;
            startingPos = -1;
            return false;
        }

        public void Insert(int index, MarkdownBlock item)
        {
            if (GetBlock(index, out var mbo, out int pos))
            {
                mbo.Insert(pos, item);
            }
        }

        public void RemoveAt(int index)
        {
            if (GetBlock(index, out var mbo, out int pos))
            {
                mbo.RemoveAt(pos);
            }
        }

        public IEnumerator<MarkdownBlock> GetEnumerator() => blocks.SelectMany(item => item).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public MarkdownBlock this[int index]
        {
            get
            {
                if (GetBlock(index, out var mbo, out int pos))
                    return mbo[pos];
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (GetBlock(index, out var mbo, out int pos))
                    mbo[pos] = value;
                throw new IndexOutOfRangeException();
            }
        }
    }
}
