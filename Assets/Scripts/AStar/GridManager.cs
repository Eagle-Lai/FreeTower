using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class GridManager : MonoBehaviour
    {
        public static GridManager _instance;
        
        public static GridManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new GridManager();
                }
                return _instance;
            }
        }
       
        /// <summary>
        /// 行数
        /// </summary>
        public int numOfRows = 10;
        /// <summary>
        /// 列数
        /// </summary>
        public int numOfColums = 10;
        public float gridCellSize = 1;
        public bool showGrid = true;
        public bool showObstacleBlocks = true;

        private Vector3 origin = new Vector3();
        private GameObject[] obstacleList;
        public static Node[,] nodes { get; set; }
        public Vector3 Origin
        {
            get { return this.origin; }
        }

        public void Init()
        {
            CalculateObstacle();
        }

        public void SetObstacleList(GameObject[] gameObjects)
        {
            obstacleList = gameObjects;
        }

        private void CalculateObstacle()
        {
            nodes = new Node[numOfColums, numOfRows];
            int index = 0;
            for (int i = 0; i < numOfColums; i++)
            {
                for (int j = 0; j < numOfRows; j++)
                {
                    Vector3 cellPos = GetGridCellCenter(index);
                    Node node = new Node(cellPos);
                    nodes[i, j] = node;
                    index++;
                }
            }
            if(obstacleList != null && obstacleList.Length > 0)
            {
                foreach (GameObject data in obstacleList)
                {
                    int indexCell = GetGridIndex(data.transform.position);
                    int col = GetColumn(indexCell);
                    int row = GetRow(indexCell);
                    nodes[row, col].MarkAsObstacle();
                }
            }
        }

        public int GetGridIndex(Vector3 pos)
        {
            if (!IsInBounds(pos))
            {
                return -1;
            }
            pos -= Origin;
            int col = (int)(pos.x / gridCellSize);
            int row = (int)(pos.z / gridCellSize);
            return (row * numOfColums + col);
        }

        public bool IsInBounds(Vector3 pos)
        {
            float width = numOfColums * gridCellSize;
            float height = numOfRows * gridCellSize;
            return (pos.x >= Origin.x && pos.x <= Origin.x + width && 
                        pos.x <= Origin.z + height && pos.z >= Origin.z);
        }

        public Vector3 GetGridCellCenter(int index)
        {
            Vector3 cellPosition = GetGridCellPosition(index);
            cellPosition.x += (gridCellSize / 2.0f);
            cellPosition.z += (gridCellSize / 2.0f);
            return cellPosition;
        }

        public Vector3 GetGridCellPosition(int index)
        {
            int row = GetRow(index);
            int col = GetColumn(index);
            float xPosition = col * gridCellSize;
            float zPosition = row * gridCellSize;
            return Origin + new Vector3(xPosition, 0, zPosition);
        }

        public int GetRow(int index)
        {
            return index / numOfColums;
        }

        public int GetColumn(int index)
        {
            return index % numOfColums;
        }

        public void GetNeighbours(Node node, List<Node> neighbours)
        {
            Vector3 neighbourPos = node.position;
            int neighbourIndex = GetGridIndex(neighbourPos);

            int row = GetRow(neighbourIndex);
            int column = GetColumn(neighbourIndex);

            int leftNodeRow = row - 1;
            int leftNodeColumn = column;
            AssignNeighour(leftNodeRow, leftNodeColumn, neighbours);

            leftNodeRow = row + 1;
            leftNodeColumn = column;
            AssignNeighour(leftNodeRow, leftNodeColumn, neighbours);

            leftNodeRow = row;
            leftNodeColumn = column + 1;
            AssignNeighour(leftNodeRow, leftNodeColumn, neighbours);

            leftNodeRow = row;
            leftNodeColumn = column - 1;
            AssignNeighour(leftNodeRow, leftNodeColumn, neighbours);
        }

        private void AssignNeighour(int row, int column, List<Node> neighbors)
        {
            if (row != -1 && column != -1 && row < numOfColums && column < numOfColums)
            {
                Node nodeToAdd = nodes[column, row];
                if (nodeToAdd != null)
                {
                    if (!nodeToAdd.isObstacle)
                    {
                        neighbors.Add(nodeToAdd);
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (showGrid)
            {
                DebugDrawGrid(transform.position, numOfRows, numOfColums, gridCellSize, Color.red);
            }
            Gizmos.DrawSphere(transform.position, 0.5f);
            if (showObstacleBlocks)
            {
                Vector3 cellSize = new Vector3(gridCellSize, 1.0f, gridCellSize);
                if(obstacleList != null && obstacleList.Length > 0)
                {
                    foreach (GameObject item in obstacleList)
                    {
                        Gizmos.DrawCube(GetGridCellCenter(GetGridIndex(item.transform.position)), cellSize);
                    }
                }
            }
        }

        public void DebugDrawGrid(Vector3 origin, int numRows, int numCols, float cellSize, Color color)
        {
            float width = (numCols * cellSize);
            float height = numRows * cellSize;

            for (int i = 0; i < numRows; i++)
            {
                Vector3 startPos = origin + i * cellSize * new Vector3(0, 0, 1);
                Vector3 endPos = startPos + width * new Vector3(1, 0, 0);
                Debug.DrawLine(startPos, endPos, color);
            }

            for (int i = 0; i < numCols; i++)
            {
                Vector3 startPos = origin + i * cellSize * new Vector3(1, 0, 0);
                Vector3 endPos = startPos + height * new Vector3(0, 0, 1);
                Debug.DrawLine(startPos, endPos, color);
            }
        }
    }
}