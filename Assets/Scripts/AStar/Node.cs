using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FTProject
{
    public class Node : IComparable
    {
        public float nodeTotalCost;
        public float estimatedCost;
        public bool isObstacle;
        public Node parent;
        public Vector3 position;
        public GameObject gameObject;

        public Node()
        {
            this.estimatedCost = 0;
            this.nodeTotalCost = 1;
            this.isObstacle = false;
            this.parent = null;
        }

        public Node(Vector3 pos)
        {
            this.estimatedCost = 0;
            this.nodeTotalCost = 1;
            this.isObstacle = false;
            this.parent = null;
            this.position = pos;
        }

        public Node(GameObject go)
        {
            this.estimatedCost = 0;
            this.nodeTotalCost = 1;
            this.isObstacle = false;
            this.parent = null;
            this.position = go.transform.localPosition;
            this.gameObject = go;
        }

        public void MarkAsObstacle()
        {
            this.isObstacle = true;
        }

        public int CompareTo(object obj)
        {
            Node node = (Node)obj;
            if (this.estimatedCost < node.estimatedCost)
            {
                return -1;
            }
            if (this.estimatedCost > node.estimatedCost)
            {
                return 1;
            }
            return 0;
        }
    }
}