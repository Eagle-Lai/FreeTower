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
        /// ��ʼ���ֵ��˵�ʱ��
        /// </summary>
        public float interval;

        /// <summary>
        ///��������֮����ֵļ����ʱ���б�
        /// </summary>
        public List<float> _IntervalList;

        public EnemyList EnemyList;
    }
        
}
