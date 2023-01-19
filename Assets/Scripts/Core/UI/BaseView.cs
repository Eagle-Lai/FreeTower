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
        /// ������ʾ��ʱ����õķ���(���ж�ε��õ����)
        /// </summary>
        public virtual void OnShow()
        {

        }
        /// <summary>
        /// �������ص�ʱ����õķ���(���ж�ε��õ����)
        /// </summary>
        public virtual void OnHide()
        {

        }
    }
}