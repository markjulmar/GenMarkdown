using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Julmar.GenMarkdown
{
    public abstract class MarkdownList : MarkdownBlock, IEnumerable<MarkdownBlock>
    {
        protected readonly List<List<MarkdownBlock>> Blocks = new();

        protected abstract string GetPrefix(MarkdownFormatting formatting, int index);

        public override void Write(TextWriter writer, MarkdownFormatting formatting)
        {
            string indent = Indent;

            var sb = new StringBuilder();
            for (int i = 0; i < Blocks.Count; i++)
            {
                var block = Blocks[i];
                string prefix = GetPrefix(formatting, i);
                for (var index = 0; index < block.Count; index++)
                {
                    if (index > 0)
                        sb.AppendLine();

                    var item = block[index];
                    sb.Append(indent + prefix);

                    if (index == 0)
                        prefix = new string(' ', prefix.Length);

                    var sw = new StringWriter();
                    item.Write(sw, formatting);

                    sb.AppendLine(sw.ToString()
                        .TrimEnd('\r', '\n')
                        .Replace("\n", "\n" + indent+prefix));
                }

                if (block.Count > 1)
                    sb.AppendLine();
            }

            writer.Write(sb.ToString().TrimEnd('\r', '\n') + Environment.NewLine + Environment.NewLine);
        }


        public void Add(MarkdownBlock item) => Blocks.Add(new List<MarkdownBlock> { item });
        public void Add(params MarkdownBlock[] items) => Blocks.Add(new List<MarkdownBlock>(items));
        public void Clear() => Blocks.Clear();
        public bool Contains(MarkdownBlock item) => this.FirstOrDefault(c => c == item) != null;
        public bool Remove(MarkdownBlock item) => Blocks.Any(items => items.Remove(item));
        public int Count => Blocks.Sum(mb => mb.Count);
        public int IndexOf(MarkdownBlock item)
        {
            for (int i = 0; i < Blocks.Count; i++)
            {
                int pos = Blocks[i].IndexOf(item);
                if (pos >= 0)
                    return pos + i;
            }

            return -1;
        }

        public int BlockCount => Blocks.Count;
        public List<MarkdownBlock> GetBlockSet(int index) => Blocks[index];

        private bool GetBlock(int index, out List<MarkdownBlock> mbo, out int startingPos)
        {
            for (int i = 0; i < Blocks.Count; i++)
            {
                for (int x = 0; x < Blocks[i].Count; x++)
                {
                    if (i+x == index)
                    {
                        mbo = Blocks[i];
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

        public IEnumerator<MarkdownBlock> GetEnumerator() => Blocks.SelectMany(item => item).GetEnumerator();
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
