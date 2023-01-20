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

        private AssetItemData uIItemData;
        public AssetItemData UIItemData { get { return uIItemData; } }

        public override void OnInit()
        {
            base.OnInit();
            activeTime = ACTIVETIME;
        }

        /// <summary>
        /// 界面显示的时候调用的方法(会有多次调用的情况)
        /// </summary>
        public virtual void OnShow()
        {

        }
        /// <summary>
        /// 界面隐藏的时候调用的方法(会有多次调用的情况)
        /// </summary>
        public virtual void OnHide()
        {

        }

        public override void SetGameObject(GameObject gameObject)
        {
            base.SetGameObject(gameObject);
            InitComponent();
        }

        public virtual void InitComponent()
        {

        }

        public void SetUIItem(AssetItemData data)
        {
            this.uIItemData = data;
        }

        public void ResetActiveTime()
        {
            activeTime = ACTIVETIME;
        }
    }
}