using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg;


namespace FTProject
{
    public class RoundInfoData
    {

        public RoundInfoData()
        {
        }
    }

    public class EnemyListData
    {
        public EnemyListData()
        {
            _IntervalList = new List<float>();
        }
        /// <summary>
        /// 开始出现敌人的时间
        /// </summary>
        public float interval;

        /// <summary>
        ///两个敌人之间出现的间隔的时间列表
        /// </summary>
        public List<float> _IntervalList;

        public EnemyList EnemyList;
    }
        
}
