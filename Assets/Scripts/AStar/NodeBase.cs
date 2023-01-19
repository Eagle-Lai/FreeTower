using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FTProject
{
    public class NodeBase : MonoBehaviour, IComparable
    {
        public float nodeTotalCost;
        public float estimatedCost;
        public bool isObstacle;
        public NodeBase parent;
        public Vector3 position;
        public NodeBase node;
        //public NodeBase()
        //{
        //    this.estimatedCost = 0;
        //    this.nodeTotalCost = 1;
        //    this.isObstacle = false;
        //    this.parent = null;
        //}

        //public NodeBase(Vector3 pos)
        //{
        //    this.estimatedCost = 0;
        //    this.nodeTotalCost = 1;
        //    this.isObstacle = false;
        //    this.parent = null;
        //    this.position = pos;
        //}

        //public NodeBase(GameObject go)
        //{
        //    this.estimatedCost = 0;
        //    this.nodeTotalCost = 1;
        //    this.isObstacle = false;
        //    this.parent = null;
        //    this.position = go.transform.localPosition;
        //    this.gameObject = go;
        //}

        public void MarkAsObstacle()
        {
            this.isObstacle = true;
        }

        public int CompareTo(object obj)
        {
            NodeBase node = (NodeBase)obj;
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