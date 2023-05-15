using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class NormalEnemy : BaseEnemy
    {

        protected override void OnStart()
        {
            base.OnStart();
            if (_EnemyDataItem != null)
            {
                _speed = _EnemyDataItem.EnemyStaticData.Speed;
            }
        }
    }
}