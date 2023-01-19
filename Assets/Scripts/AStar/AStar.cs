using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class AStar
    {
        public static PriorityQueue closeList, openList;
        private static float HeuristicEstimateCost(NodeBase curNode, NodeBase goalNode)
        {
            Vector3 vecCost = curNode.position - goalNode.position;
            return vecCost.magnitude;
        }

        public static List<NodeBase> FindPath(NodeBase start, NodeBase goal)
        {
            openList = new PriorityQueue();
            openList.Push(start);
            start.nodeTotalCost = 0;
            start.estimatedCost = HeuristicEstimateCost(start, goal);
            closeList = new PriorityQueue();
            NodeBase node = null;
            while (openList.Length != 0)
            {
                node = openList.First();
                if (node.position == goal.position)
                {
                    return CalculatePath(node);
                }
                List<NodeBase> neighbours = new List<NodeBase>();
                GridManager.Instance.GetNeighbours(node, neighbours);
                for (int i = 0; i < neighbours.Count; i++)
                {
                    NodeBase neighbourNode = neighbours[i];
                    if (!closeList.Contains(neighbourNode))
                    {
                        float cost = HeuristicEstimateCost(node, neighbourNode);
                        float totalCost = node.nodeTotalCost + cost;
                        float neighbourNodeEstCost = HeuristicEstimateCost(neighbourNode, goal);
                        neighbourNode.nodeTotalCost = totalCost;
                        neighbourNode.parent = node;
                        neighbourNode.estimatedCost = totalCost + neighbourNodeEstCost;
                        if (!openList.Contains(neighbourNode))
                        {
                            openList.Push(neighbourNode);
                        }
                    }
                }
                closeList.Push(node);
                openList.Remove(node);
            }
            if (node.position != goal.position)
            {
                Debug.LogError("Path Not Found");
                return null;
            }
            return CalculatePath(node);
        }

        public static List<NodeBase> CalculatePath(NodeBase node)
        {
            List<NodeBase> list = new List<NodeBase>();
            while(node != null)
            {
                list.Add(node);
                node = node.parent;
            }
            list.Reverse();
            return list;
        }
    }
}