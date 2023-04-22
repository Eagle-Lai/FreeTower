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
            _IntervalList = new List<List<float>>();
            _EnemyList = new List<EnemyList>();
        }

        
        /// <summary>
        /// 时间间隔列表
        /// </summary>
        public List<List<float>> _IntervalList;

        /// <summary>
        /// 敌人列表
        /// </summary>
        public List<EnemyList> _EnemyList;
    }
}
