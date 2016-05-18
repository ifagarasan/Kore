using System;
using System.Diagnostics;

using KoreCode.Exceptions;
using KoreCode.Node.Builders;

namespace KoreCode.Trees.Binary.RedBlackTree
{
    public class RedBlackTree : Bst
    {
        protected override BinaryNodeBuilder CreateNodeBuilder()
        {
            return new RedBlackNodeBuilder();
        }

        #region IsBalanced

        public override bool IsBalanced()
        {
            if (Root.Color == Color.Red)
                return false;

            bool result = true;

            Inorder((x) => {
                if (IsRedNodeWithARedChild(x) || (x.IsLeaf && BlackHeightDiffers(x)))
                {
                    result = false;
                    return false;
                }

                return true;
            });

            return result;
        }

        private bool IsRedNodeWithARedChild(IBinaryNode node)
        {
            return node.Color == Color.Red && (node.Left.Color == Color.Red || node.Right.Color == Color.Red);
        }

        private bool BlackHeightDiffers(IBinaryNode node)
        {
            if (!node.IsLeaf)
                throw new Exception("node with Key '" + node.Key + "' is not a leaf");

            return Root.Height != GetBlackHeighFromNodeUpwards(node);
        }

        private int GetBlackHeighFromNodeUpwards(IBinaryNode node)
        {
            int height = 0;

            while (node != Nil)
            {
                if (node.Color == Color.Black)
                    height++;

                node = node.Parent;
            }

            return height;
        }

        #endregion

        public override void Insert(IBinaryNode node)
        {
            base.Insert(node);
            InsertFixup(node);
        }

        protected override void Remove(IBinaryNode node)
        {
            Color originalColor = node.Color;
            IBinaryNode fixTarget = null;

            if (node.Left == Nil)
            {
                fixTarget = node.Right;
                Transplant(node, node.Right);
            }
            else if (node.Right == Nil)
            {
                fixTarget = node.Left;
                Transplant(node, node.Left);
            }
            else
            {
                IBinaryNode successor = Min(node.Right);
                originalColor = successor.Color;
                fixTarget = successor.Right;

                if (successor.Parent == node)
                    fixTarget.Parent = successor;
                else
                {
                    Transplant(successor, successor.Right);
                    successor.Right = node.Right;
                    successor.Right.Parent = successor;
                }

                Transplant(node, successor);
                successor.Left = node.Left;
                successor.Left.Parent = successor;
                successor.Color = node.Color;
            }

            if (originalColor == Color.Black)
                RemoveFixup(fixTarget);
        }

        private void RemoveFixup(IBinaryNode node)
        {
            while (node != Root && node.Color == Color.Black)
            {
                 // Case 1:
                if (node.Sibling.Color == Color.Red)
                {
                    node.Sibling.Color = Color.Black;
                    node.Parent.Color = Color.Red;

                    Rotate(node.Parent, node.IsLeftChild ? RotateDirection.Left : RotateDirection.Right);
                }
                
                // In all cases below the sibling is Black

                // Case 2:
                if (node.Sibling.Left.Color == Color.Black && node.Sibling.Right.Color == Color.Black)
                {
                    node.Sibling.Color = Color.Red;
                    node = node.Parent;
                }
                else
                {
                    // Case 3:
                    if (node.Sibling.Right.Color == Color.Black)
                    {
                        node.Sibling.Color = Color.Red;
                        node.Sibling.Left.Color = Color.Black;

                        Rotate(node.Sibling, node.IsLeftChild ? RotateDirection.Right : RotateDirection.Left);
                    }

                    // Case 4:
                    node.Sibling.Color = node.Parent.Color;
                    node.Parent.Color = node.Sibling.Right.Color = Color.Black;
    
                    Rotate(node.Parent, node.IsLeftChild ? RotateDirection.Left : RotateDirection.Right);

                    node = Root;
                }
            }

            node.Color = Color.Black;
        }

        private void InsertFixup(IBinaryNode node)
        {
            while (node.Parent.Color == Color.Red)
            {
                if (node.Uncle.Color == Color.Red)
                {
                    node.Parent.Color = node.Uncle.Color = Color.Black;
                    node.Grandparent.Color = Color.Red;
                    node = node.Grandparent;
                }
                else
                {
                    // the primary rotation takes place around the node grandparent
                    RotateDirection primaryRotationDirection = node.Parent.IsLeftChild ? RotateDirection.Right : RotateDirection.Left;

                    // the secondary rotation takes place around the node parent, only if the node and grand parent are not colinear
                    RotateDirection? secondaryRotationDirection = null;

                    if (node.Parent.IsLeftChild && node.IsRightChild)
                        secondaryRotationDirection = RotateDirection.Left;
                    else if (node.Parent.IsRightChild && node.IsLeftChild)
                        secondaryRotationDirection = RotateDirection.Right;

                    if (secondaryRotationDirection.HasValue)
                        Rotate(node.Parent, secondaryRotationDirection.Value);
                    else
                        node = node.Parent;

                    // the node in the middle will become the parent, so its color is set to Black
                    node.Color = Color.Black;

                    // the node in the top will become a leaf so it will be Red
                    node.Parent.Color = Color.Red;

                    node = node.Parent;

                    // rotation around the grandfather
                    Rotate(node, primaryRotationDirection);
                }
            }

            Root.Color = Color.Black;
        }
    }
}