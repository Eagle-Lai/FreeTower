using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class BaseView : BaseDisplayObject
    {
        protected float activeTime = 15;

        protected string _layout;
        public string Layout { get; set; }

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
    }
}