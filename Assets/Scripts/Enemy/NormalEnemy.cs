using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class NormalEnemy : BaseEnemy
    {

        protected override void OnAwake()
        {
            base.OnAwake();
            _speed = GlobalConst.EnemySpeed;
        }
    }
}