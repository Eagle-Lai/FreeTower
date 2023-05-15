using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg;

namespace FTProject
{
    public class EnemyDataItem
    {
        public EnemyData EnemyStaticData;

        public EnemyDataItem(EnemyData enemyData)
        {
            EnemyStaticData = enemyData;
        }
    }
}