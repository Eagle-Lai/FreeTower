using AStar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace FTProject
{
    public class AStarManager : MonoBehaviour
    {

        private Action<string> callback; 

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
        string path;
        public static AStarManager Instance;
        void Awake()
        {
            Instance = this;
          path =
#if UNITY_ANDROID && !UNITY_EDITOR
        Application.streamingAssetsPath + "/Map.txt";
#elif UNITY_IPHONE && !UNITY_EDITOR
        "file://" + Application.streamingAssetsPath + "/Map.txt";
#elif UNITY_STANDLONE_WIN || UNITY_EDITOR
        "file://" + Application.streamingAssetsPath + "/Map.txt";
#else
        string.Empty;
#endif
           
        }

        IEnumerator ReadData(string path, Action<string> action)
        {
            WWW www = new WWW(path);
            yield return www;
            while (www.isDone == false)
            {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(0.5f);
            string data = www.text;
            action(data);
            newPath = new List<Point>();

            if (startPoint != null && targetPoint != null)
            {
                bool isFind = AStarWrapper.Instance.FindPath(startPoint, targetPoint, map, mapWidth, mapHeight);
                if (isFind == true)
                {

                    newPath = GetAStarPath();
                }
            }
            yield return new WaitForEndOfFrame();
        }

        // Start is called before the first frame update
        void Start()
        {
            Init();
            StartCoroutine(ReadData(path, (value) => { Debug.Log(value); InitMap(value);  }));
        }

        void Init()
        {
           
          
        }

        void InitMap(string valueStr)
        {
            Transform parent = ResourcesManager.Instance.LoadAndInitGameObject("PointParent").transform;
            GameObject go;
            string[] infos = valueStr.Split("\r\n");
            Debug.Log(infos.Length);
            for (int i = 0; i < infos.Length; i++)
            {
                Debug.Log(infos[i]);
                char[] temp = infos[i].ToCharArray();
                for (int j = 0; j < temp.Length; j++)
                {
                    if (map == null)
                    {
                        mapWidth =  temp.Length;
                        mapHeight = infos.Length;
                        map = new Point[mapHeight + 1, mapWidth + 1];
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
                            go = ResourcesManager.Instance.LoadAndInitGameObject("NormalPoint", parent.transform, null, Vector3.zero, GlobalConst.PointScale);
                            pointObj = go.AddComponent<BasePoint>();
                            break;
                    }
                    pointObj.name += string.Format("({0},{1})", i, j);
                    pointObj.transform.position = tempPoint.position;
                    pointObj.Point = tempPoint;
                    map[i, j] = tempPoint;
                    tempPoint.gameObject = pointObj.gameObject;
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
            if (Application.isPlaying && startPoint != null && targetPoint != null && map != null)
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