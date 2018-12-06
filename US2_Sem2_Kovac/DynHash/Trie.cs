using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DynHash
{
    class Trie
    {
        private Node Root { get; set; }

        public Trie()
        {
            this.Root = new Node();
        }

        public Node Find(BitArray arr, out int depth)
        {
            Node act = this.Root;
            bool right = true;
            depth = 0;

            while (act.IsInternal())
            {
                right = arr[depth++];
                if (right)
                    act = act.Right;
                else
                    act = act.Left;

                if (act == null)
                    return act;
            }
            return act == this.Root ? null : act;
        }

        public Node Add(BitArray arr, int address, int steps, bool parent = false)
        {
            Node newNode = new Node();
            Node act = this.Root;
            int index = 0;
            bool right = true;

            while (steps > 0)
            {
                right = arr[index++];
                if (!this.SetNext(right, ref act))
                {
                    act.Right = new Node(); // create new internal node
                    act.Left = new Node();
                    if (steps == 1)
                    {
                        if (right || parent)
                        {
                            act.Right.Address = address;
                            act.Right.BlockSize = 1;
                        }
                        else
                        {
                            act.Left.Address = address;
                            act.Left.BlockSize = 1;
                        }
                        newNode = parent ? act : (right ? act.Right : act.Left);
                    }
                    else
                        this.SetNext(right, ref act);
                }

                steps--;
            }

            return newNode;
        }

        private bool SetNext(bool right, ref Node act)
        {
            if (right && act.Right != null)
                act = act.Right;
            else if (!right && act.Left != null)
                act = act.Left;
            else
                return false;
            return true;
        }

        public override string ToString()
        {
            string ret = "";
            if (this.Root == null)
                return ret;
            LinkedList<Node> s = new LinkedList<Node>();
            s.AddLast(this.Root);
            Node act = this.Root;
            while (s.Count > 0) // go through all items in the tree
            {
                act = s.Last();
                s.RemoveLast();

                ret += act.ToString2();

                if (act.Right != null)
                    s.AddLast(act.Right);
                if (act.Left != null)
                    s.AddLast(act.Left);
            }

            return ret;
        }
    }
}
