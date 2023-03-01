using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class TowerPosition : MonoBehaviour
    {

        Vector3 cubeScreenPos;


        private float yPos = 1.6f;

        public Transform parent;

        public BaseTower BaseTower;

        public List<BasePoint> enterNodeList = new List<BasePoint>();
        public bool isBuild = false;
        public TowerBuildState TowerBuildState;

        public BasePoint _BasePoint;

        private void Awake()
        {
            TowerBuildState = TowerBuildState.None;
            BaseTower = transform.parent.GetComponent<BaseTower>();
            parent = transform.parent.GetComponent<Transform>();
            EventDispatcher.AddEventListener(EventName.UpdateEvent, MyUpdate);
            EventDispatcher.AddEventListener<BaseTower>(EventName.DestroyTower, DestroyTower);
        }

        private void DestroyTower(BaseTower baseTower)
        {
            if(baseTower == parent.GetComponent<BaseTower>())
            {
                if (_BasePoint != null)
                {
                    _BasePoint.ResetPoint();
                    _BasePoint = null;
                }
            }
        }

        private void Start()
        {
            EventDispatcher.TriggerEvent(EventName.BuildingTower);
        }

        public void OnDestroy()
        {
            EventDispatcher.RemoveEventListener(EventName.UpdateEvent, MyUpdate);
        }

        private void MyUpdate()
        {
            if (isBuild == false)
            {
                cubeScreenPos = Camera.main.WorldToScreenPoint(parent.transform.position);

                //2. ����ƫ����
                //������ά����
                Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cubeScreenPos.z);
                //�����ά����תΪ��������
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                //Ŀǰ������ά����תΪ��ά����
                Vector3 curMousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cubeScreenPos.z);
                //Ŀǰ�������ά����תΪ��������
                curMousePos = Camera.main.ScreenToWorldPoint(curMousePos);
                //��������λ��
                parent.transform.position = new Vector3(curMousePos.x, GlobalConst.UnbuildYPosition, curMousePos.z);
            }
            if ((Input.GetMouseButtonUp(0)) && isBuild == false && BuildTower())
            {
                SetBuildSuccess();
            }
        }

        public void SetBuildSuccess()
        {
            isBuild = true;
            this.transform.localPosition = new Vector3(0, -1.5f, 0);
            TowerBuildState = TowerBuildState.Build;
            if (_BasePoint != null)
            {
                parent.transform.SetObjParent(_BasePoint.transform, GlobalConst.BuildYVector3, GlobalConst.BuildScale, Quaternion.identity);
                SetTowerJsonData(_BasePoint);
            }

            EventDispatcher.TriggerEvent(EventName.UpdateAStarPath);
            EventDispatcher.TriggerEvent(EventName.BuildTowerSuccess);
        }

        public bool BuildTower()
        {
            if (enterNodeList != null)
            {
                for (int i = 0; i < enterNodeList.Count; i++)
                {
                    Color color = enterNodeList[i].GetNodeColor();
                    if (color == Color.green && !enterNodeList[i].IsHaveBuild)
                    {
                        BasePoint point = enterNodeList[i];
                        //node.node.MarkAsObstacle(false);
                        //List<Node> nodes = GridManager.Instance.GetPath();
                        //if (nodes == null)
                        //{
                        //    node.node.MarkAsObstacle();
                        //    return false;
                        //}
                        point.Point.IsWall = true;
                        bool isFind = AStarManager.Instance.IsFindPath();
                        if (!isFind)
                        {
                            point.Point.IsWall = false;
                            BuildFail();
                            return false;
                        }
                        //parent.transform.position = enterNodeList[i].transform.position + new Vector3(0, GlobalConst.BuildYPosition, 0);
                        enterNodeList.Clear();
                        BasePoint node = point.transform.GetComponent<BasePoint>();
                        node.BuildSuccess();
                        _BasePoint = node;
                        node.BaseTower = parent.GetComponent<BaseTower>();
                        return true;
                    }
                }
            }
            BuildFail();
            return false;
        }

        public void SetTowerJsonData(BasePoint basePoint)
        {
            TowerJsonData towerJsonData = new TowerJsonData();
            towerJsonData.Level = 1;
            towerJsonData.Type = (int)TowerType.Normal;
            towerJsonData.TowerName = parent.gameObject.name;
            JsonDataManager.Instance.Write(basePoint.column + basePoint.row.ToString() + basePoint.gameObject.name, towerJsonData);
        }

        private void BuildFail()
        {
            EventDispatcher.TriggerEvent(EventName.BuildNormalTower, enterNodeList);
            Destroy(parent.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name.Contains("NormalPoint") && isBuild == false)
            {
                BasePoint node = other.transform.GetComponent<BasePoint>();
                if (node != null && enterNodeList != null)
                {
                    enterNodeList.Add(node);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            BasePoint node = other.transform.GetComponent<BasePoint>();
            if (node != null)
            {
                enterNodeList.Remove(node);
            }
        }

    }
}