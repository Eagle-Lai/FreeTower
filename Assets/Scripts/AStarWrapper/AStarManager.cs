using AStar;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace FTProject
{
    public class AStarManager : MonoBehaviour
    {
        public int mapWidth;
        public int mapHeight;
        Point[,] map = null;

        Point startPoint = null;
        Point targetPoint = null;

        List<Point> newPath = null;

        Color normalColor = Color.white;
        Color wallColor = Color.black;
        Color startPosColor = Color.green;
        Color targetPosColor = Color.red;
        Color pathPosColor = Color.yellow;

        public static AStarManager Instance;
        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        void Init()
        {
           
            newPath = new List<Point>();
            InitMap();
            if (startPoint != null && targetPoint != null)
            {
                bool isFind = AStarWrapper.Instance.FindPath(startPoint, targetPoint, map, mapWidth, mapHeight);
                if (isFind == true)
                {

                    newPath = GetAStarPath();
                }
            }
        }

        void InitMap()
        {
            TextAsset textAsset = Resources.Load<TextAsset>("Map");
            GameObject parent = ResourcesManager.Instance.LoadAndInitGameObject("PointParent");
            int index = 0;
            string path = Application.dataPath + "/Resources/Map.txt";
            string[] infos = File.ReadAllLines(path);
            //mapWidth = 12;
            //mapHeight = 10;
            //map = new Point[mapWidth, mapHeight];
            GameObject go;
            for (int i = 0; i < infos.Length; i++)
            {
                char[] temp = infos[i].ToCharArray();
                for (int j = 0; j < temp.Length; j++)
                {
                    if (map == null)
                    {
                        mapWidth = infos.Length;
                        mapHeight = temp.Length;
                        map = new Point[infos.Length, temp.Length];
                    }
                    BasePoint pointObj = null;
                    Point tempPoint = new Point(i, j);
                    tempPoint.position = new Vector3(i * 2, 0, j * 2);
                    switch (temp[j])
                    {
                        case '-':
                            go = ResourcesManager.Instance.LoadAndInitGameObject("NormalPoint", parent.transform, null, Vector3.zero, GlobalConst.PointScale);
                            pointObj = go.AddComponent<NormalPoint>();
                            pointObj.PointType = PointType.Normal;
                            break;

                        case '*':
                            go = ResourcesManager.Instance.LoadAndInitGameObject("StartPoint", parent.transform, null, Vector3.zero, GlobalConst.PointScale);
                            pointObj = go.AddComponent<StartPoint>();
                            pointObj.PointType = PointType.Start;
                            startPoint = tempPoint;
                            break;

                        case '&':
                            go = ResourcesManager.Instance.LoadAndInitGameObject("EndPoint", parent.transform, null, Vector3.zero, GlobalConst.PointScale);
                            pointObj = go.AddComponent<EndPoint>();
                            pointObj.PointType = PointType.End;
                            targetPoint = tempPoint;
                            break;
                                   
                        case '#':
                            go = ResourcesManager.Instance.LoadAndInitGameObject("NormalObstacle", parent.transform, null, Vector3.zero, GlobalConst.PointScale);
                            pointObj = go.AddComponent<NormalObstacle>();
                            pointObj.PointType = PointType.Obstacle;
                            tempPoint.IsWall = true;
                            break;
                        default:
                            break;
                    }
                    pointObj.name += string.Format("({0},{1})", i, j);
                    pointObj.transform.position = tempPoint.position;
                    pointObj.Point = tempPoint;
                    map[i, j] = tempPoint;
                    tempPoint.gameObject = pointObj.gameObject;
                    index++;
                }
            }
        }


        public bool IsFindPath()
        {
            return AStarWrapper.Instance.FindPath(startPoint, targetPoint, map, mapWidth, mapHeight);
        }

        public List<Point> GetAStarPath(Point startPoint, Point targetPoint)
        {

            List<Point> result = new List<Point>();
            Point temp = targetPoint.Parent;
            while (true)
            {
                if (temp == startPoint)
                {
                    break;
                }
                if (temp != null)
                {
                    result.Add(temp);
                }
                temp = temp.Parent;
            }
            return result;
        }

        public List<Point> GetAStarPath()
        {
            return GetAStarPath(startPoint, targetPoint);
        }
       
        public Point GetEndPoint()
        {
            return targetPoint;
        }

        private void SetCubeColor(int x, int y, Color c)
        {
            map[x, y].gameObject.GetComponent<Renderer>().material.color = c;
        }

        void OnDrawGizmos()
        {

#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                bool isFind = AStarWrapper.Instance.FindPath(startPoint, targetPoint, map, mapWidth, mapHeight);
                if (isFind == true)
                {

                    newPath = GetAStarPath();
                }
                if (newPath == null)
                    return;

                if (newPath.Count > 0)
                {
                    int index = 1;
                    foreach (Point node in newPath)
                    {
                        if (index < newPath.Count)
                        {
                            Point nextNode = newPath[index];
                            Debug.DrawLine(node.position + new Vector3(0, 10, 0), nextNode.position + new Vector3(0, 10, 0), Color.white);
                            index++;
                        }
                    };
                }
            }
#endif
        }
    }
}