using System.Collections;
using System.Collections.Generic;
using UnityEngine;                                              

namespace FTProject
{
    public class AStarPath : MonoBehaviour
    {
        public static AStarPath Instance;

        private Transform startPos, endPos;
        public Node startNode { get; set; }
        public Node goalNode { get; set; }
        public List<Node> pathArray;
        [SerializeField]
        private GameObject objStartCube, objEndCube;
        private float elapsedTime = 0f;
        public float intervalTime = 1.0f;

        private int cols = 0;
        private int rows = 0;
        public int ObstacleCount = 10;

        public List<GameObject> nodeList = new List<GameObject>();
        public Transform parent;

        private void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            cols = GridManager.Instance.numOfColums;
            rows = GridManager.Instance.numOfRows;
            //objStartCube = nodeList[0];
            //objEndCube = nodeList[nodeList.Count - 1];
            //objStartCube.GetComponent<MeshRenderer>().material.color = Color.green;
            //objEndCube.GetComponent<MeshRenderer>().material.color = Color.blue;
            GridManager.Instance.Init();
            pathArray = new List<Node>();
        }

        private void InitNode()
        {
            GameObject parent = ResourcesManager.Instance.LoadAndInitGameObject("MapParent");
            GameObject obj1 = ResourcesManager.Instance.LoadAndInitGameObject("Cube");
            GameObject obj;
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    obj = GameObject.Instantiate(obj1);
                    obj.transform.SetParent(parent.transform);
                    obj.transform.localPosition = new Vector3(i, 0, j);
                    obj.transform.localScale = Vector3.one;
                    nodeList.Add(obj);
                }
            }
            int index = 0;
            GameObject[] gos = new GameObject[10];
            for (int i = 1; i < nodeList.Count - 1; i++)
            {
                int random = Random.Range(10, 90);
                if (index < ObstacleCount)
                {
                    gos[index] = nodeList[random];
                    gos[index].gameObject.tag = "obstacle";
                    gos[index].GetComponent<MeshRenderer>().material.color = Color.red;
                    gos[index].GetComponent<MeshRenderer>().enabled = true;
                    index++;
                }
            }
            GridManager.Instance.SetObstacleList(gos);
        }

        private void OnDrawGizmos()
        {
            if (pathArray == null)
            {
                return;
            }
            if (pathArray.Count > 0)
            {
                int index = 1;
                foreach (Node node in pathArray)
                {
                    if (index < pathArray.Count)
                    {
                        Node nextNode = (Node)pathArray[index];
                        Debug.DrawLine(node.position, nextNode.position, Color.green);
                        index++;
                    }
                }
            }
        }

        public List<Node> GetPath()
        {
            
            startPos = objStartCube.transform;
            
            endPos = objEndCube.transform;

            startNode = new Node(GridManager.Instance.GetGridCellCenter(GridManager.Instance.GetGridIndex(startPos.position)));
            goalNode = new Node(GridManager.Instance.GetGridCellCenter(GridManager.Instance.GetGridIndex(endPos.position)));

            pathArray = AStar.FindPath(startNode, goalNode);
            return pathArray;
        }

    }
}