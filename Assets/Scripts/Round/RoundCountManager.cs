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
        RoundInfoItem _RoundInfoItem;

        public override void OnInit()
        {
            base.OnInit();
            TimerManager.Instance.AddTimer(0.02f, -1, Update);
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
        }

        private void Update()
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
            TimerManager.Instance.AddTimer(_RoundInfoItem.Interval, _RoundInfoItem.Number, GenerateEnemy);
        }
    }
}