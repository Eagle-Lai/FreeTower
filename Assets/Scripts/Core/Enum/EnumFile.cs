using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{

    public enum EnemyType
    {
        NONE = 0,
        NormalEnemy,
    }
    public enum NodeType
    {
        None = 0,
        Start,
        Normal, 
        End,
        Obstacle,
    }

    public enum BulletType
    {
        None = 0,
        NormalBullet,
    }
}