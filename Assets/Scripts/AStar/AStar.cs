using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class AStar
    {
        public static PriorityQueue closeList, openList;

        private static float HeuristicEstimateCost(Node curNode, Node goalNode)
        {
            Vector3 vecCost = curNode.position - goalNode.position;
            return vecCost.magnitude;
        }
    }
}