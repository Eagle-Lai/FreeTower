using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class BaseTower : MonoBehaviour
    {

        public bool isBuild = false;

        public bool isGenerate = false;

        Vector3 cubeScreenPos;
        protected virtual void Awake()
        {            
        }

        protected virtual void Start()
        {
            EventDispatcher.AddEventListener<BaseTower>(HandlerName.OperateUp, OperateUpEvent);
            isGenerate = true;
        }

        private void OperateUpEvent(object obj)
        {
            throw new NotImplementedException();
        }

        protected virtual void Update()
        {
            if (isBuild == false)
            {
                cubeScreenPos = Camera.main.WorldToScreenPoint(transform.position);

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
                transform.position = new Vector3(curMousePos.x, 0.9f, curMousePos.z);
            }
            if (isGenerate && (Input.GetMouseButton(0) || Input.touchCount == 1) && isBuild == false)
            {
                isBuild = true;
                isGenerate = false;

            }
        }

        protected virtual void OnDestroy()
        {
            
        }


        protected virtual void OnTriggerEnter(Collider other)
        {

        }

        protected virtual void OperateUpEvent(BaseTower tower)
        {
            Debug.Log("up");
        }
    }
}