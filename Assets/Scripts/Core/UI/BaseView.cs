using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class BaseView : BaseDisplayObject
    {
        public const float ACTIVETIME = 15;
        public float activeTime;

        public bool isShow = false;

        protected string _layout;
        public string Layout { get; set; }

        private AssetData.AssetItemData uIItemData;
        public AssetData.AssetItemData UIItemData { get { return uIItemData; } }

        public override void OnInit()
        {
            base.OnInit();
            activeTime = ACTIVETIME;
        }

        /// <summary>
        /// ������ʾ��ʱ����õķ���(���ж�ε��õ����)
        /// </summary>
        public virtual void OnShow()
        {
            _gameObject.ShowObject();
        }
        /// <summary>
        /// �������ص�ʱ����õķ���(���ж�ε��õ����)
        /// </summary>
        public virtual void OnHide()
        {

        }

        public void SetUI(Transform parent)
        {

            RectTransform transform = _gameObject.GetComponent<RectTransform>();
            transform.SetParent(parent);
            transform.anchorMin = Vector2.zero;
            transform.anchorMax = Vector2.one;
            transform.anchoredPosition = Vector2.zero;
            transform.sizeDelta = Vector2.zero;

        }
        public override void SetGameObject(GameObject gameObject)
        {
            base.SetGameObject(gameObject);
            InitComponent();
        }

        public virtual void InitComponent()
        {

        }

        public void SetUIItem(AssetData.AssetItemData data)
        {
            this.uIItemData = data;
        }

        public void ResetActiveTime()
        {
            activeTime = ACTIVETIME;
        }

        public void CloseSelf()
        {
            OnHide();
            _gameObject.HideObject();
        }
    }
}