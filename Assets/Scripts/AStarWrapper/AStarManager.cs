using AStar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace FTProject
{
    public class AStarManager : BaseManager<AStarManager>
    {

        public LineRenderer _LineRenderer;

        private Action<string> callback;

        public int mapWidth;
        public int mapHeight;
        Point[,] map = null;

        public Point startPoint = null;
        public Point targetPoint = null;

        public List<Point> newPath = null;

        private Transform PointParent;


        string path;

        private Vector3[] vector3Path;

        private bool isInitMap = false;

        private string MapName;

        public override void OnInit()
        {
            base.OnInit();
        }

        public void OnInitMapData()
        {
            MapName = "/Map/Map" + (GameSceneManager.Instance.GetCurrentSceneInfo()._SceneInfo.Id) + ".txt";
            Debug.Log(MapName);
            path =
#if UNITY_ANDROID && !UNITY_EDITOR
        Application.streamingAssetsPath + MapName;
#elif UNITY_IPHONE && !UNITY_EDITOR
        "file://" + Application.streamingAssetsPath + MapName;
#elif UNITY_STANDLONE_WIN || UNITY_EDITOR
        "file://" + Application.streamingAssetsPath + MapName;
#else
        string.Empty;
#endif
            EventDispatcher.AddEventListener<BaseTower>(EventName.DestroyTower, UpdatePath);
            EventDispatcher.AddEventListener(EventName.BuildTowerSuccess, UpdateStarPath);
            EventDispatcher.AddEventListener(EventName.RefreshPathEvent, UpdateStarPath);
            Launcher.Instance.StartCoroutine(ReadData(path, (value) => { Debug.Log(value); InitMap(value); }));
        }

        private void UpdatePath(BaseTower baseTower)
        {
            bool isFind = AStarWrapper.Instance.FindPath(startPoint, targetPoint, map, mapWidth, mapHeight);
            if(isFind)
            {
                DrawLine();
            }
        }

        private void UpdateStarPath()
        {
            bool isFind = AStarWrapper.Instance.FindPath(startPoint, targetPoint, map, mapWidth, mapHeight);
            if (isFind)
            {
                DrawLine();
            }
        }

        IEnumerator ReadData(string path, Action<string> action)
        {
            WWW www = new WWW(path);
            yield return www;
            while (www.isDone == false)
            {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
            string data = www.text;
            action(data);
            //newPath = new List<Point>();

            //if (startPoint != null && targetPoint != null)
            //{
            //    bool isFind = AStarWrapper.Instance.FindPath(startPoint, targetPoint, map, mapWidth, mapHeight);
            //    if (isFind == true)
            //    {

            //        newPath = GetPath();
            //    }
            //}
            
            yield return new WaitForEndOfFrame();
           
            
        }

        // Start is called before the first frame update

        public override void OnDestroy()
        {
            base.OnDestroy();
            EventDispatcher.RemoveEventListener<BaseTower>(EventName.DestroyTower, UpdatePath);
            EventDispatcher.RemoveEventListener(EventName.BuildTowerSuccess, UpdateStarPath);
            EventDispatcher.RemoveEventListener(EventName.RefreshPathEvent, UpdateStarPath);
        }
        private void DrawLine()
        {
            vector3Path = GetPathPosition();
            if (vector3Path.Length > 0)
            {
                if (vector3Path.Length > 0)
                {
                    if(_LineRenderer == null)
                    {
                        GameObject go = ResourcesManager.Instance.LoadAndInitGameObject("LineRender", startPoint.transform);
                        _LineRenderer = go.GetComponent<LineRenderer>();
                        _LineRenderer.transform.localPosition = Vector3.zero;
                    }
                    _LineRenderer.positionCount = vector3Path.Length;
                    _LineRenderer.SetPositions(vector3Path);
                }
            }
        }

        public void SetPointParentPosition(Vector3 position)
        {
            PointParent.transform.position = position;
        }

        void InitMap(string valueStr)
        {
            PointParent = ResourcesManager.Instance.LoadAndInitGameObject("PointParent").transform;
            GameObject go;
            string[] infos = valueStr.Split("\r\n");
            for (int i = 0; i < infos.Length; i++)
            {
                //Debug.Log(infos[i]);
                char[] temp = infos[i].ToCharArray();
                for (int j = 0; j < temp.Length; j++)
                {
                    if (map == null)
                    {
                        mapWidth =  temp.Length;
                        mapHeight = infos.Length;
                        map = new Point[mapHeight + 1, mapWidth + 1];
                        Debug.LogError("map width : " + mapWidth);
                        Debug.LogError("map height : " + mapHeight);
                    }
                    BasePoint pointObj = null;
                    Point tempPoint = new Point(i, j);
                    tempPoint.position = new Vector3(i * 2, 0, j * 2);
                    switch (temp[j])
                    {
                        case '*':
                            go = ResourcesManager.Instance.LoadAndInitGameObject("StartPoint", PointParent.transform, null, Vector3.zero, GlobalConst.PointScale);
                            pointObj = go.AddComponent<StartPoint>();
                            pointObj._PointType = PointType.Start;
                            startPoint = tempPoint;
                            break;

                        case '&':
                            go = ResourcesManager.Instance.LoadAndInitGameObject("EndPoint", PointParent.transform, null, Vector3.zero, GlobalConst.PointScale);
                            pointObj = go.AddComponent<EndPoint>();
                            pointObj._PointType = PointType.End;
                            targetPoint = tempPoint;
                            break;

                        case '-':
                            go = ResourcesManager.Instance.LoadAndInitGameObject("NormalPoint", PointParent.transform, null, Vector3.zero, GlobalConst.PointScale);
                            pointObj = go.AddComponent<NormalPoint>();
                            pointObj._PointType = PointType.Normal;
                            break;

                        case '#':
                            go = ResourcesManager.Instance.LoadAndInitGameObject("NormalObstacle", PointParent.transform, null, Vector3.zero, GlobalConst.PointScale);
                            pointObj = go.AddComponent<NormalObstacle>();
                            pointObj._PointType = PointType.Obstacle;
                            tempPoint.IsWall = true;
                            break;
                        default:
                            go = ResourcesManager.Instance.LoadAndInitGameObject("NormalPoint", PointParent.transform, null, Vector3.zero, GlobalConst.PointScale);
                            pointObj = go.AddComponent<BasePoint>();
                            break;
                    }
                    pointObj.name += string.Format("({0},{1})", i, j);
                    pointObj.transform.position = tempPoint.position;
                    pointObj.Point = tempPoint;
                    map[i, j] = tempPoint;
                    tempPoint.gameObject = pointObj.gameObject;
                    pointObj.SetColumnAndRow(i, j);
                    pointObj.InitPoint();
                    tempPoint.name = pointObj.name;
                    tempPoint.transform = pointObj.transform;
                }
            }

            UpdateStarPath();
            isInitMap = true;
            EventDispatcher.TriggerEvent(EventName.MapInitFinish);
        }


        public bool IsFindPath()
        {
            return AStarWrapper.Instance.FindPath(startPoint, targetPoint, map, mapWidth, mapHeight);
        }

        public List<Point> GetAStarPath(Point startPoint, Point targetPoint)
        {

            List<Point> result = new List<Point>();
            if (startPoint != null && targetPoint != null)
            {
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
            return null;
        }

        public List<Point> GetPath()
        {
            bool isFind = AStarWrapper.Instance.FindPath(startPoint, targetPoint, map, mapWidth, mapHeight);
            return GetAStarPath(startPoint, targetPoint);
        }

        public List<Point> CalculateEnemyPath(Point point)
        {
            AStarWrapper.Instance.FindPath(point, targetPoint, map, mapWidth, mapHeight);
            return GetAStarPath(startPoint, targetPoint);
        }

        public Vector3[] GetPathPosition()
        {
            List<Point> points = GetPath();
            
            if (points != null && points.Count > 0)
            {
                //points.Reverse();
                //points.Add(targetPoint);
                Vector3[] result = new Vector3[points.Count];
                for (int i = 0; i < points.Count; i++)
                {
                    result[i] = points[i].position + Vector3.up;
                }
                points.Reverse();
                return result;
            }
            return null;
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

                    vector3Path = GetPathPosition();
                }
                if (vector3Path == null)
                    return;

                if (vector3Path.Length > 0)
                {
                    int index = 1;
                    //DrawLine();
                    foreach (Vector3 node in vector3Path)
                    {
                        if (index < newPath.Count)
                        {
                            Vector3 nextNode = vector3Path[index];
                            Debug.DrawLine(node + new Vector3(0, 10, 0), nextNode + new Vector3(0, 10, 0), Color.white);
                            index++;
                        }
                    };
                }

                
            }
#endif
        }
    }
}