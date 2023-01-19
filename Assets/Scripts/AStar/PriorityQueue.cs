using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class PriorityQueue
    {
        private List<NodeBase> nodes = new List<NodeBase>();
        public int Length
        {
            get { return nodes.Count; }
        }
        public bool Contains(NodeBase node)
        {
            return nodes.Contains(node);
        }

        public NodeBase First()
        {
            if(this.nodes.Count > 0)
            {
                NodeBase node = this.nodes[0];
                nodes.RemoveAt(0);
                return node;
            }
            return null;
        }

        public void Push(NodeBase node)
        {
            this.nodes.Add(node);
            this.nodes.Sort();
        }

        public void Remove(NodeBase node)
        {
            this.nodes.Remove(node);
            this.nodes.Sort();
        }
    }
}