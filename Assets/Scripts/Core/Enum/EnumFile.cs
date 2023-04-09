using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{

    public enum EnemyType
    {
        NONE = 0,
        /// <summary>
        /// 普通敌人
        /// </summary>
        NormalEnemy,
    }
    public enum PointType
    {
        None = 0,
        /// <summary>
        /// 开始点
        /// </summary>
        Start,
        /// <summary>
        /// 普通地图点
        /// </summary>
        Normal, 
        /// <summary>
        /// 结束点
        /// </summary>
        End,
        /// <summary>
        /// 障碍物
        /// </summary>
        Obstacle,
        /// <summary>
        /// 空节点
        /// </summary>
        Empty,
    }

    public enum BulletType
    {
        None = 0,
        /// <summary>
        /// 普通子弹
        /// </summary>
        NormalBullet,
    }

    /// <summary>
    /// 子弹状态
    /// </summary>
    public enum BulletState
    {
        None = 0,
        /// <summary>
        /// 攻击状态
        /// </summary>
        Fire,
        /// <summary>
        /// 空闲状态
        /// </summary>
        Idle,
    }

    public enum EnemyState
    {
        None = 0,
        /// <summary>
        /// 默认状态
        /// </summary>
        Idle,
        /// <summary>
        /// 行走状态
        /// </summary>
        Move,
    }

    public enum TowerBuildState
    {
        None = 0,
        /// <summary>
        /// 已建造
        /// </summary>
        Build,
        /// <summary>
        /// 未建造
        /// </summary>
        Unbuild,
    }
}