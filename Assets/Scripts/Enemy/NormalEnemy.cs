/**
Create By LaiZhangYin, Eagle
if you have any question, please call wechat:782966734
**/
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class NormalEnemy : BaseEnemy
    {
        

        public static NormalEnemy Instacne
        {
            get { return new NormalEnemy(); }
        }

        List<Node> nodes;
        private int index = 0;
        private float x;
        private float z;
        public override void OnInit()
        {
            base.OnInit();
            _speed = 20f;
        }

        public override void OnStart()
        {                           
            base.OnStart();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public void OnUpdate()
        {

        }

    }
}