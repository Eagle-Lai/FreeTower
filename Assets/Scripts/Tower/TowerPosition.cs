/**
Create By LaiZhangYin, Eagle
if you have any question, please call wechat:782966734
**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class TowerPosition : MonoBehaviour
    {

        Vector3 cubeScreenPos;


        private float yPos = 1.8f;

        Transform parent;

        public List<BaseNode> enterNodeList = new List<BaseNode>();
        public bool isBuild = false;
        private void Awake()
        {
            parent = transform.parent.GetComponent<Transform>();
        }

        private void Update()
        {
            if (isBuild == false)
            {
                cubeScreenPos = Camera.main.WorldToScreenPoint(parent.position);

                //2. 计算偏移量
                //鼠标的三维坐标
                Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cubeScreenPos.z);
                //鼠标三维坐标转为世界坐标
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                //目前的鼠标二维坐标转为三维坐标
                Vector3 curMousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cubeScreenPos.z);
                //目前的鼠标三维坐标转为世界坐标
                curMousePos = Camera.main.ScreenToWorldPoint(curMousePos);
                //物体世界位置
                parent.position = new Vector3(curMousePos.x, yPos, curMousePos.z);
            }
            if ((Input.GetMouseButtonUp(0) || Input.touchCount == 1) && isBuild == false && BuildTower())
            {
                this.transform.localPosition = new Vector3(0, -1.5f, 0);
                isBuild = true;
            }
        }

        public bool BuildTower()
        {
            if (enterNodeList != null)
            {
                for (int i = 0; i < enterNodeList.Count; i++)
                {
                    Color color = enterNodeList[i].GetNodeColor();
                    if (color == Color.green)
                    {
                        parent.transform.position = enterNodeList[i].transform.position + new Vector3(0, 1.9f, 0);
                        enterNodeList.Clear();
                        return true;
                    }
                }
            }
            return false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name.Contains("NormalNode") && isBuild == false)
            {
                BaseNode node = other.transform.GetComponent<BaseNode>();
                if (node != null && enterNodeList != null)
                {
                    enterNodeList.Add(node);
                }
            }
        }

    }
}