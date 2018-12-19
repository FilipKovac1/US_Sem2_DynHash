using System.Collections.Generic;

namespace DynHash
{
    public class Node
    {
        public int BlockSize { get; set; }
        public int Address { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public LinkedList<Node> Next { get; set; }

        public Node()
        {
            this.BlockSize = -1;
            this.Address = -1;
            this.Next = new LinkedList<Node>();
        }

        public Node(int blockSize, int address) : this()
        {
            BlockSize = blockSize;
            Address = address;
        }

        public bool IsInternal() => this.Left != null || this.Right != null;
        public string ToString2() => this.IsInternal() || this.BlockSize < 0 ? "" : this.Address + " " + string.Join(" ", this.Next) + "\n";
        public override string ToString() => this.Address + "";
    }
}
