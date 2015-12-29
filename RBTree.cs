using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RBTree<TKey, TValue> where TKey : IComparable
{

    public class RBNode
    {
        public RBNode()
        {
        }

        public enum Color
        {
            RED,
            BLACK
        }

        public uint id;
        public Color color;
        public TKey key;
        public TValue data;
        public RBNode parent;
        public RBNode left;
        public RBNode right;
    }

    public RBNode root;

    public RBTree()
    {
        root = null;
    }


    public void Add(TKey key, TValue value)
    {
        if (root == null)
        {
            root = new RBNode();
            root.color = RBNode.Color.BLACK;
            root.parent = null;
            root.left = null;
            root.right = null;
        }
        else
        {


        }
        //RBNode<T> y = null;
        //RBNode<T> x = null;
        //while (x != null)
        //{
        //    y = x;
        //    if (z.key.CompareTo(x.key) < 0)
        //    {
        //        x = x.left;
        //    }
        //    else
        //    {
        //        x = x.right;
        //    }
        //}
        //z.parent = y;
        //if (y == null)
        //{
        //    root = z;
        //}
        //else if (z.key.CompareTo(y.key) < 0)
        //{
        //    y.left = z;
        //}
        //else
        //{
        //    y.right = z;
        //}
        //z.left = null;
        //z.right = null;
        //z.color = 1;
        //while (z.parent != null && z.parent.color == 1)
        //{
        //    if (z.parent == z.parent.parent.left)
        //    {
        //        y = z.parent.parent.right;
        //        if (y != null && y.color == 1)
        //        {
        //            y.color = 0;
        //            z.parent.color = 0;
        //            z.parent.parent.color = 1;
        //            z = z.parent.parent;
        //        }
        //        else if (z == z.parent.right)
        //        {
        //            z = z.parent;
        //            rotatel(z);
        //        }
        //        z.parent.color = 0;
        //        z.parent.parent.color = 1;
        //        rotater(z.parent.parent);
        //    }
        //    else
        //    {
        //        y = z.parent.parent.left;
        //        if (y != null && y.color == 1)
        //        {
        //            y.color = 0;
        //            y.parent.color = 0;
        //            z.parent.parent.color = 1;
        //            z = z.parent.parent;
        //        }
        //        else if (z == z.parent.left)
        //        {
        //            z = z.parent;
        //            rotater(z);
        //        }
        //        z.parent.color = 0;
        //        z.parent.parent.color = 1;
        //        rotater(z.parent.parent);
        //    }
        //}
        //z.color = 0;
    }

    //public void rotatel(RBNode<T> x)
    //{
    //    RBNode<T> y = x.right;
    //    x.right = y.left;
    //    if (y.left != null)
    //    {
    //        y.left.parent = x;

    //    }
    //    y.parent = x.parent;
    //    if (x.parent == null)
    //    {
    //        root = y;
    //    }
    //    else if (x == x.parent.left)
    //    {
    //        x.parent.left = y;
    //    }
    //    else
    //    {
    //        x.parent.right = y;
    //    }
    //    y.left = x;
    //    x.parent = y;
    //}

    //public void rotater(RBNode<T> x)
    //{
    //    RBNode<T> y = x.left;
    //    x.left = y.right;
    //    if (y.right != null)
    //    {
    //        y.right.parent = x;
    //    }
    //    y.parent = x.parent;
    //    if (x.parent == null)
    //    {
    //        root = y;
    //    }
    //    else if (x == x.parent.left)
    //    {
    //        x.parent.left = y;
    //    }
    //    else
    //    {
    //        x.parent.right = y;
    //    }
    //    y.right = x;
    //    x.parent = y;
    //}

    //public void Remove(TKey key)
    //{

    //}

    //public void visit()
    //{
    //    Queue o = new Queue();
    //    Queue q = new Queue();
    //    q.Enqueue(root);
    //    uint id = 0;
    //    while (q.Count > 0)
    //    {
    //        RBNode<T> n = q.Dequeue() as RBNode<T>;
    //        n.id = id;
    //        id++;
            
    //        q.Enqueue(n.left);
    //        q.Enqueue(n.right);
    //    }

    //}

   

}
