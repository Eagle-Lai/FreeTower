using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class NormalEnemy : BaseEnemy
    {
        List<Node> nodes;
        private int index = 0;
        private int _timerId;
        private float x;
        private float z;
        public override void OnInit()
        {
            base.OnInit();
            _speed = 10f;
            _timerId = TimerManager.Instance.AddTimer(Time.deltaTime, -1, OnUpdate);
        }

        public override void OnStart()
        {                           
            base.OnStart();
            
            //if(_go != null)
            //{
            //   nodes = AStarPath.Instance.GetPath();
            //}
            //_speed = 5;
            //MoveToGoal();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            TimerManager.Instance.RemoveTimerById(_timerId);
        }

        public void OnUpdate()
        {

        }

    }
}