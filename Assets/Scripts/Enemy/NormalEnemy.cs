using System.Collections;
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
        public override void Init()
        {
            base.Init();
            _speed = 10f;
            _timerId = TimerManager.Instance.AddTimer(Time.deltaTime, -1, OnUpdate);
        }

        public override void OnStart()
        {                           
            base.OnStart();
            
            if(_go != null)
            {
               nodes = AStarPath.Instance.GetPath();
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            TimerManager.Instance.RemoveTimerById(_timerId);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Vector3 pos = nodes[index].position;
            x += Time.deltaTime;
            z += Time.deltaTime;
            if(x < pos.x && z < pos.z)
            {
                _go.transform.localPosition = new Vector3(x, 0, z);
            }
            else
            {
                index++;
            }
        }
    }
}