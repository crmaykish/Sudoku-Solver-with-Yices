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

        public TreeNode(TreeNode t)
        {
            this.r = t.r;
            this.c = t.c;
            this.lastValue = t.lastValue;
            this.parent = t.parent;
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

        public void setChildren(List<TreeNode> newChildren)
        {
            this.children = new List<TreeNode>();
            foreach (TreeNode t in newChildren)
            {
                TreeNode newChild = new TreeNode(t);
                newChild.setParent(this);
                this.children.Add(newChild);
            }
        }

        public void setChildren()
        {
            this.children = new List<TreeNode>();
            foreach (TreeNode t in parent.children)
            {
                TreeNode newChild = new TreeNode(t);
                newChild.setParent(this);
                this.children.Add(newChild);
            }

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
