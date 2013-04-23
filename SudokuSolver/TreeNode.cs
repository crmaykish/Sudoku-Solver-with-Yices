using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSolver
{
    class TreeNode
    {
        
        private TreeNode parent;
        private List<TreeNode> children;
        public int r, c;
        public int lastValue;

        public TreeNode(int r, int c, int v)
        {
            this.r = r;
            this.c = c;
            this.lastValue = v;
            this.parent = null;
        }

        public TreeNode getNextNode()
        {
            if (children == null && parent != null)
            {
                this.setChildren();
            }
            if (children.Count == 0)
            {
                return null;
            }
            else
            {
                int i = new Random().Next(0, children.Count - 1);
                TreeNode child = children[i];
                children.RemoveAt(i);
                return child;
            }
        }

        public TreeNode getParent()
        {
            return parent;
        }

        public void setChildren(List<TreeNode> children)
        {
            this.children = new List<TreeNode>(children);
            foreach (TreeNode t in this.children)
            {
                t.setParent(this);
            }
        }

        public void setChildren()
        {
            this.children = new List<TreeNode>(parent.children);
            this.children.Remove(this);
        }

        public void setParent(TreeNode parent)
        {
            this.parent = parent;
        }

        public override string ToString()
        {
            return "(" + r + "," + c + " = " + lastValue + ")";
        }
    }
}
