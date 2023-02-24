using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FTProject
{
    public struct RoundInfoItem
    {
        public string Name;
        public string Description;
        public int Level;
        public int TotalCount;
        public int Number;
        public float Interval;
    }

    public class RoundCountManager : BaseManager<RoundCountManager>
    {
        public RoundInfoItem _RoundInfoItem;

        public override void OnInit()
        {
            EventDispatcher.AddEventListener(EventName.UpdateEvent, MyUpdate);
            base.OnInit();
            InitRoundInfo();
        }

        private void InitRoundInfo()
        {
            _RoundInfoItem = new RoundInfoItem();
            _RoundInfoItem.Number = GlobalConst.RoundEnemyNumber;
            _RoundInfoItem.Interval = GlobalConst.EnemyGenerateInterval;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            EventDispatcher.RemoveEventListener(EventName.UpdateEvent, MyUpdate);
        }

        private void MyUpdate()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GenerateEnemyByInfoItem();
            }
        }

        private void GenerateEnemy()
        {
            EnemyManager.Instance.GenerateEnemy();
        }

        public void GenerateEnemyByInfoItem()
        {
            _RoundInfoItem.Number = GlobalConst.RoundEnemyNumber;
            _RoundInfoItem.Interval = GlobalConst.EnemyGenerateInterval;
            TimerManager.Instance.AddTimer(_RoundInfoItem.Interval, _RoundInfoItem.Number, GenerateEnemy);
        }

    }
}
