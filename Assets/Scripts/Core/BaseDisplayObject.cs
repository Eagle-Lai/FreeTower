/**
Create By LaiZhangYin, Eagle
if you have any question, please call wechat:782966734
**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class BaseDisplayObject
    {
        protected GameObject _gameObject;

        public object[] datas;
        public bool IsActive
        {
            get { return _gameObject.activeSelf; }
            set { _gameObject.SetActive(value); }
        }
        public GameObject GameObject
        {
            get { return _gameObject; }
        }
        /// <summary>
        /// ��ʼ������
        /// </summary>
        public virtual void OnInit()
        {

        }
        /// <summary>
        /// ��ʼ����
        /// </summary>
        public virtual void OnStart()
        {

        }
        /// <summary>
        /// ���ٷ���
        /// </summary>
        public virtual void OnDestroy()
        {

        }

        public virtual void SetGameObject(GameObject gameObject)
        {
            this._gameObject = gameObject;
        }

        public void SetData(object[] datas)
        {
            this.datas = datas;
        }

        public void SetParent(Transform parent)
        {
            _gameObject.transform.SetParent(parent);
        }
    }
}