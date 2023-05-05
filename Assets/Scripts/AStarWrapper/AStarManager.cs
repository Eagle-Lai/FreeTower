using AStar;
using System;
using System.Collections;
using System.Collections.Generic;
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

        BaseGameScene _BaseGameScene;

        public float xscale = 1f;//缩放比例
        public float yscale = 1f;


        private List<GameObject> mapArrowPathList = new List<GameObject>();

        public override void OnInit()
        {
            base.OnInit();
        }

        public void OnInitMapData()
        {
            _BaseGameScene = GameSceneManager.Instance.GetCurrentSceneInfo();
            MapName = "/Map/Map" + _BaseGameScene._SceneInfo.Id + ".txt";
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
            ResetMapArrowPath();

            if (vector3Path.Length > 0)
            {
                if (mapArrowPathList.Count < vector3Path.Length)
                {
                    FillPathList();
                }
                for (int i = 0; i < vector3Path.Length; i++)
                {
                    mapArrowPathList[i].transform.position = vector3Path[i];
                    mapArrowPathList[i].ShowObject();
                    mapArrowPathList[i].transform.SetParent(startPoint.transform);
                    //mapArrowPathList[i].transform.localScale = Vector3.one;
                    if (i < vector3Path.Length - 1)
                    {
                        //Quaternion rot = FTProjectUtils.GetRotate(vector3Path[i + 1], vector3Path[i]);
                        //Debug.Log(rot.eulerAngles);
                        //Vector3 pos = vector3Path[i] - vector3Path[i + 1];

                        ////mapArrowPathList[i].transform.rotation.SetLookRotation(rot.eulerAngles);
                        //Quaternion rotation = Quaternion.LookRotation(pos, Vector3.back);
                        //mapArrowPathList[i].transform.rotation = rotation;
                        mapArrowPathList[i].transform.LookAt(vector3Path[i + 1]);
                        mapArrowPathList[i].transform.Rotate(90, 0, -90);
                    }
                }
                //if (_LineRenderer == null)
                //{
                //    GameObject go = ResourcesManager.Instance.LoadAndInitGameObject("LineRender", startPoint.transform);
                //    _LineRenderer = go.GetComponent<LineRenderer>();
                //    _LineRenderer.transform.localPosition = Vector3.zero;
                //}
                //_LineRenderer.positionCount = vector3Path.Length;
                //_LineRenderer.SetPositions(vector3Path);
            }
        }
        //private void DrawLine(Vector3 start, Vector3 end, int index)
        //{
        //    MeshRenderer mr;
        //    if (index >= lines.Count)
        //    {
        //        mr = Instantiate(meshRenderer);
        //        lines.Add(mr);
        //    }
        //    else
        //    {
        //        mr = lines[index];
        //    }

        //    var tran = mr.transform;
        //    var length = Vector3.Distance(start, end);
        //    tran.localScale = new Vector3(xscale, length, 1);
        //    tran.position = (start + end) / 2;
        //    //指向
        //    tran.LookAt(start);
        //    //旋转偏移
        //    tran.Rotate(90, 0, 0);
        //    mr.material.mainTextureScale = new Vector2(1, length * yscale);
        //    mr.gameObject.SetActive(true);
        //}
        private void ResetMapArrowPath()
        {
            if(mapArrowPathList.Count > 0)
            {
                for (int i = 0; i < mapArrowPathList.Count; i++)
                {
                    mapArrowPathList[i].HideObject();
                    mapArrowPathList[i].transform.SetParent(null);
                    mapArrowPathList[i].transform.localScale = Vector3.one;
                }
            }
        }

        private void FillPathList()
        {
            int index = mapArrowPathList.Count;
            for (int i = index; i < vector3Path.Length; i++)
            {
                GameObject go = ResourcesManager.Instance.LoadAndInitGameObject("MapArrow");
                go.HideObject();
                go.transform.localEulerAngles = new Vector3(90, 0, 0);
                mapArrowPathList.Add(go);
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
            int pointScale = _BaseGameScene._SceneInfo.PointScale;
            string[] infos = valueStr.Split("\n");
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
                    tempPoint.position = new Vector3(i * pointScale, 0, j * pointScale);
                    
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
                        case '@':
                            go = ResourcesManager.Instance.LoadAndInitGameObject("EmptyPoint", PointParent.transform, null, Vector3.zero, GlobalConst.PointScale);
                            pointObj = go.AddComponent<EmptyPoint>();
                            pointObj._PointType = PointType.Empty;
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
                    pointObj.SetColumnAndRow(i, j);
                    pointObj.InitPoint();
                    pointObj.SetPointScale(pointScale);


                    tempPoint.gameObject = pointObj.gameObject;
                    tempPoint.name = pointObj.name;
                    tempPoint.transform = pointObj.transform;

                    map[i, j] = tempPoint;
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

        public List<Point> UpdatePathByEnemyPoint(Point point)
        {
            AStarWrapper.Instance.FindPath(point, targetPoint, map, mapWidth, mapHeight);
            return GetAStarPath(point, targetPoint);
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

        private void SetCubeColor(int x, int y, UnityEngine.Color c)
        {
            map[x, y].gameObject.GetComponent<Renderer>().material.color = c;
        }

      

//#if UNITY_EDITOR
//        void OnDrawGizmos()
//        {
//            if (Application.isPlaying && startPoint != null && targetPoint != null && map != null)
//            {
//                bool isFind = AStarWrapper.Instance.FindPath(startPoint, targetPoint, map, mapWidth, mapHeight);
//                if (isFind == true)
//                {

//                    vector3Path = GetPathPosition();
//                }
//                if (vector3Path == null)
//                    return;

//                if (vector3Path.Length > 0)
//                {
//                    int index = 1;
//                    //DrawLine();
//                    foreach (Vector3 node in vector3Path)
//                    {
//                        if (index < newPath.Count)
//                        {
//                            Vector3 nextNode = vector3Path[index];
//                            Debug.DrawLine(node + new Vector3(0, 10, 0), nextNode + new Vector3(0, 10, 0), UnityEngine.Color.white);
//                            index++;
//                        }
//                    };
//                }

                
//            }

//        }
//#endif
    }
}