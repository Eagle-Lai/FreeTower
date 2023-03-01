using AStar;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class BasePoint : MonoBehaviour
    {
        [SerializeField]
        public Point Point;
        public PointType _PointType;
        public bool IsHaveBuild = false;

        public bool isWall = false;

        protected MeshRenderer meshRenderer;

        public BaseTower BaseTower;

        public JsonData _jsonData;

        public int column, row;

        /// <summary>
        /// ��ײ����
        /// </summary>
        public GameObject currentTriggerObj;

        protected virtual void Awake()
        {
           

        }
        protected virtual void Start()
        {
            EventDispatcher.AddEventListener(EventName.UpdateEvent, MyUpdate);
            EventDispatcher.AddEventListener<List<BasePoint>>(EventName.BuildNormalTower, BuildFail);
            meshRenderer = transform.GetComponent<MeshRenderer>();
        }

        public virtual void OnDestroy()
        {
            EventDispatcher.RemoveEventListener(EventName.UpdateEvent, MyUpdate);
            EventDispatcher.RemoveEventListener<List<BasePoint>>(EventName.BuildNormalTower, BuildFail);
        }


        protected virtual void MyUpdate()
        {
            if (currentTriggerObj != null && meshRenderer.material.color != Color.green && _PointType == PointType.Normal)
            {
                float distance = FTProjectUtils.GetPointDistance(this.gameObject, currentTriggerObj);

                Point.IsWall = true;
                bool isFind = AStarManager.Instance.IsFindPath();
                if (!isFind)
                {
                    Point.IsWall = false;
                    ChangeColor(Color.red);
                    return;
                }
                if (distance >= 0.6f)
                {
                    ChangeColor(Color.red);
                }
                if (Mathf.Max(0.1f, distance) < 0.6f)
                {
                    ChangeColor(Color.green);
                }
            }
        }

        private void BuildFail(List<BasePoint> points)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i] == this)
                {
                    ChangeColor(Color.black);
                }
            }
        }
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name.Contains("TowerBase"))
            {
                currentTriggerObj = other.gameObject;
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            currentTriggerObj = null;
            if (_PointType == PointType.Normal && other.gameObject.name.Contains("Tower"))
            {
                Debug.Log(other.gameObject.name);
                Debug.Log(gameObject.name);
                ChangeColor(Color.black);
                Point.IsWall = false;
                isWall = false;
            }
        }
        public virtual void ChangeColor(Color color)
        {
            if (meshRenderer != null)
            {
                meshRenderer.material.color = color;
            }
        }

        public Color GetNodeColor()
        {
            return meshRenderer.material.color;
        }

        public void BuildSuccess()
        {
            Point.IsWall = true;
            IsHaveBuild = true;
            ChangeColor(Color.black);
            _PointType = PointType.Obstacle;
            isWall = true;
        }

        public void ResetPoint()
        {
            Point.IsWall = false;
            IsHaveBuild = false;
            ChangeColor(Color.black);
            _PointType = PointType.Normal;
            isWall = false;
        }

        public void SetColumnAndRow(int col, int r)
        {
            column = col;
            row = r;
        }

        public void InitPoint()
        {
            StartCoroutine(JsonDataManager.Instance.Read(column + row.ToString() + gameObject.name, (data) =>
             {
                 if (data != null)
                 {
                     _jsonData = data;
                     InitTowerInfo(data);
                 }
             }));
        }

        public void InitTowerInfo(JsonData data)
        {
            if (data != null && data.Keys.Count > 0)
            {
                //foreach (JsonData item in data)
                {
                    if (BaseTower == null)
                    {
                        bool temp = int.TryParse(data["Type"].ToString(), out int value);
                        if (temp)
                        {
                            TowerType towerType = (TowerType)value;
                            TowerJsonData towerJsonData = new TowerJsonData();
                            switch (towerType)
                            {
                                case TowerType.Normal:
                                    BaseTower = TowerManager.Instance.GetTower<NormalTower>(TowerType.Normal);
                                    BaseTower.transform.SetObjParent(transform, GlobalConst.BuildYVector3, GlobalConst.BuildScale, Quaternion.identity);
                                    break;
                                default:
                                    break;
                            }
                            BaseTower.TowerPosition.SetBuildSuccess();
                            BaseTower.TowerType = towerType;
                            string TowerName = "";
                            if (data["TowerName"]!= null)
                            {
                                TowerName = data["TowerName"].ToString();
                                BaseTower.TowerName = TowerName;
                            }
                            temp = int.TryParse(data["Level"].ToString(), out int result);
                            if (temp)
                            {
                                towerJsonData.TowerName = TowerName;
                                towerJsonData.Level = result;
                                towerJsonData.Type = (int)towerType;
                                ChangeColor(Color.black);

                                IsHaveBuild = true;
                                Point.IsWall = true;
                                isWall = true;
                                currentTriggerObj = null;
                                AStarManager.Instance.SetPointAsWall(column, row);
                                EventDispatcher.TriggerEvent(EventName.BuildTowerSuccess);
                            }
                        }
                        else
                        {
                            Debug.LogError("something errors +  " + gameObject.name);
                        }
                    }
                }
            }
        }
    }
}