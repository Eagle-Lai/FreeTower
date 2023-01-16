using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class PriorityQueue
    {
        private ArrayList nodes = new ArrayList();
        public int Length
        {
            get { return nodes.Count; }
        }
        public bool Contains(Node node)
        {
            return nodes.Contains(node);
        }

        public Node First()
        {
            if(this.nodes.Count > 0)
            {
                return (Node)this.nodes[0];
            }
            return null;
        }

        public void Push(Node node)
        {
            this.nodes.Add(node);
            this.nodes.Sort();
        }

        public void Remove(Node node)
        {
            this.nodes.Remove(node);
            this.nodes.Sort();
        }
    }
}