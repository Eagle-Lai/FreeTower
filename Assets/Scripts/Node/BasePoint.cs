using AStar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class BasePoint : MonoBehaviour
    {
        [SerializeField]
        public Point Point;
        public PointType PointType;
        public bool IsHaveBuild = false;
       

        protected MeshRenderer meshRenderer;

        /// <summary>
        /// ��ײ����
        /// </summary>
        public GameObject currentTriggerObj;

        protected virtual void Awake()
        {
            EventDispatcher.AddEventListener(EventName.UpdateEvent, MyUpdate);
        }
        protected virtual void Start()
        {
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
            if (currentTriggerObj != null && meshRenderer.material.color != Color.green && PointType == PointType.Normal)
            {
                ////float distance = Vector3.Distance(transform.position, currentTriggerObj.transform.position);
                //float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(currentTriggerObj.transform.position.x, currentTriggerObj.transform.position.z));
                float distance = FTProjectUtils.GetPointDistance(this.gameObject, currentTriggerObj);
                //node.MarkAsObstacle();
                //List<Node> nodes = GridManager.Instance.GetPath();
                //if (nodes == null)
                //{
                //    node.MarkAsObstacle(false);
                //    ChangeColor(Color.red);
                //    return;
                //}
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
                if (Mathf.Max(0.1f, distance) < 0.6f )
                {
                    ChangeColor(Color.green);
                }
            }
        }

        private void BuildFail(List<BasePoint> points)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if(points[i] == this)
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
            if (PointType == PointType.Normal)
            {
                ChangeColor(Color.black);
                Point.IsWall = false;
            }
        }
        public virtual void ChangeColor(Color color)
        {
            if(meshRenderer != null)
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
            PointType = PointType.Obstacle;
        }

        public void DestroyTower()
        {
            Point.IsWall = false;
            IsHaveBuild = false;
            ChangeColor(Color.black);
            PointType = PointType.Normal;
        }
    }
}