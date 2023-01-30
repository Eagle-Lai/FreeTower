using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace FTProject
{

    //Grid manager class handles all the grid properties
    public class GridManager : MonoBehaviour
    {
        private static GridManager s_Instance = null;

        private Vector3 nodeVector3 = new Vector3(1.9f, 0.5f, 1.9f);

        private float scale = 2f;

        public static GridManager Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = FindObjectOfType(typeof(GridManager)) as GridManager;
                    if (s_Instance == null)
                        Debug.Log("Could not locate an GridManager object. \n You have to have exactly one GridManager in the scene.");
                }
                return s_Instance;
            }
        }

        void OnApplicationQuit()
        {
            s_Instance = null;
        }
        private Transform startPos, endPos;
        public StartNode startNode { get; set; }
        public EndNode endNode { get; set; }

        public List<Node> pathArray;

        GameObject objStartCube, objEndCube;


        #region Fields
        private int numOfRows;
        private int numOfColumns;
        public float gridCellSize;
        public bool showGrid = true;
        public bool showObstacleBlocks = true;

        private float elapsedTime = 0.0f;
        public float intervalTime = 1.0f; //Interval time between path finding

        private Vector3 origin = new Vector3();
        //private GameObject[] obstacleList;
       // public Node[,] nodes { get; set; }
        public BaseNode[,] nodesObj { get; set; }
        #endregion

        public Vector3 Origin
        {
            get { return origin; }
        }

        void Awake()
        {
            CalculateObstacles();
        }


        void CalculateObstacles()
        {
            GameObject go;
            GameObject parent = ResourcesManager.Instance.LoadAndInitGameObject("NodeParent");
            //nodes = new Node[numOfColumns, numOfRows];
            //nodesObj = new BaseNode[numOfColumns, numOfRows];
            TextAsset textAsset = Resources.Load<TextAsset>("Map");
            int index = 0;
            string path = Application.dataPath + "/Resources/Map.txt";
            string[] infos = File.ReadAllLines(path);
            for (int i = 0; i < infos.Length; i++)
            {
                char[] temp = infos[i].ToCharArray();
                for (int j = 0; j < temp.Length; j++)
                {
                    if (nodesObj == null)
                    {
                        numOfColumns = infos.Length;
                        numOfRows = temp.Length;
                        nodesObj = new BaseNode[infos.Length, temp.Length];
                    }
                    Vector3 cellPos = GetGridCellCenter(index);
                    Node node = new Node(cellPos);

                    BaseNode nodeObj = null;
                    switch (temp[j])
                    {
                        case '-':
                            go = ResourcesManager.Instance.LoadAndInitGameObject("NormalNode", parent.transform, null, Vector3.zero,nodeVector3);
                            nodeObj = go.AddComponent<NormalNode>();
                            nodeObj.NodeType = NodeType.Normal;
                            break;

                        case '*':       //开始点
                            go = ResourcesManager.Instance.LoadAndInitGameObject("StartNode", parent.transform, null, Vector3.zero, Vector3.one * scale);
                            nodeObj = go.AddComponent<StartNode>();
                            startNode = nodeObj as StartNode;
                            startNode.node = node;
                            nodeObj.NodeType = NodeType.Start;
                            break;

                        case '&':       //结束点
                            go = ResourcesManager.Instance.LoadAndInitGameObject("EndNode", parent.transform, null, Vector3.zero, Vector3.one * scale);
                            nodeObj = go.AddComponent<EndNode>();
                            endNode = nodeObj as EndNode;
                            endNode.node = node;
                            nodeObj.NodeType = NodeType.End;
                            break;

                        case '#':
                            go = ResourcesManager.Instance.LoadAndInitGameObject("NormalObstacle", parent.transform, null, Vector3.zero, Vector3.one * scale);
                            nodeObj = go.AddComponent<NormalObstacle>();
                            node.MarkAsObstacle();
                            nodeObj.NodeType = NodeType.Obstacle;
                            break;

                        default:
                            break;
                    }
                    nodeObj.node = node;
                    nodeObj.transform.position = cellPos;
                    nodesObj[i, j] = nodeObj;
                    nodeObj.name += string.Format("({0},{1})", i, j);
                    index++;
                }
            }

            //for (int i = 0; i < 100; i++)
            //{
            //    int x = Random.Range(0, 20);
            //    int y = Random.Range(0, 20);
            //    nodesObj[x, y].GetComponent<MeshRenderer>().enabled = true;
            //    nodesObj[x, y].GetComponent<MeshRenderer>().material.color = Color.red;
            //    nodesObj[x, y].node.MarkAsObstacle();
            //}
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
            float xPosInGrid = col * gridCellSize;
            float zPosInGrid = row * gridCellSize;

            return Origin + new Vector3(xPosInGrid, 0.0f, zPosInGrid);
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

            return (row * numOfColumns + col);
        }


        public int GetRow(int index)
        {
            int row = index / numOfColumns;
            return row;
        }


        public int GetColumn(int index)
        {
            int col = index % numOfColumns;
            return col;
        }

        public bool IsInBounds(Vector3 pos)
        {
            float width = numOfColumns * gridCellSize;
            float height = numOfRows * gridCellSize;

            return (pos.x >= Origin.x && pos.x <= Origin.x + width && pos.x <= Origin.z + height && pos.z >= Origin.z);
        }


        public void GetNeighbours(Node node, List<Node> neighbors)
        {
            Vector3 neighborPos = node.position;
            int neighborIndex = GetGridIndex(neighborPos);

            int row = GetRow(neighborIndex);
            int column = GetColumn(neighborIndex);

            //Bottom
            int leftNodeRow = row - 1;
            int leftNodeColumn = column;
            AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors);

            //Top
            leftNodeRow = row + 1;
            leftNodeColumn = column;
            AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors);

            //Right
            leftNodeRow = row;
            leftNodeColumn = column + 1;
            AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors);

            //Left
            leftNodeRow = row;
            leftNodeColumn = column - 1;
            AssignNeighbour(leftNodeRow, leftNodeColumn, neighbors);
        }

        void AssignNeighbour(int row, int column, List<Node> neighbors)
        {
            if (row != -1 && column != -1 && row < numOfRows && column < numOfColumns)
            {
                Node nodeToAdd = nodesObj[row, column].node;
                if (!nodeToAdd.bObstacle)
                {
                    neighbors.Add(nodeToAdd);
                }
            }
        }

        void OnDrawGizmos()
        {
            //Draw Grid
            if (showGrid)
            {
                DebugDrawGrid(transform.position, numOfRows, numOfColumns, gridCellSize, Color.blue);
            }

            //Grid Start Position
            Gizmos.DrawSphere(transform.position, 0.5f);

        }

        public void DebugDrawGrid(Vector3 origin, int numRows, int numCols, float cellSize, Color color)
        {
            float width = (numCols * cellSize);
            float height = (numRows * cellSize);

            // Draw the horizontal grid lines
            for (int i = 0; i < numRows + 1; i++)
            {
                Vector3 startPos = origin + i * cellSize * new Vector3(0.0f, 0.0f, 1.0f);
                Vector3 endPos = startPos + width * new Vector3(1.0f, 0.0f, 0.0f);
                Debug.DrawLine(startPos, endPos, color);
            }

            // Draw the vertial grid lines
            for (int i = 0; i < numCols + 1; i++)
            {
                Vector3 startPos = origin + i * cellSize * new Vector3(1.0f, 0.0f, 0.0f);
                Vector3 endPos = startPos + height * new Vector3(0.0f, 0.0f, 1.0f);
                Debug.DrawLine(startPos, endPos, color);
            }

            if (pathArray == null)
                return;

            if (pathArray.Count > 0)
            {
                int index = 1;
                foreach (Node node in pathArray)
                {
                    if (index < pathArray.Count)
                    {
                        Node nextNode = (Node)pathArray[index];
                        Debug.DrawLine(node.position + new Vector3(0, 10, 0), nextNode.position + new Vector3(0, 10, 0), Color.black);
                        index++;
                    }
                };
            }
        }

        public List<Node> GetPath()
        {
            //startPos = startNode.transform;
            //endPos = endNode.transform;

            //Assign StartNode and Goal Node
            //startNode = new Node(GetGridCellCenter(GetGridIndex(startPos.position)));
            //endNode = new Node(GetGridCellCenter(GetGridIndex(endPos.position)));

            pathArray = AStar.FindPath(startNode.node, endNode.node);
            return pathArray;
        }
        void Update()
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= intervalTime)
            {
                elapsedTime = 0.0f;
                GetPath();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                EnemyManager.Instance.GenerateEnemy(GameObject.Find("EnemyParent").transform);
                
            }
        }
    }
}