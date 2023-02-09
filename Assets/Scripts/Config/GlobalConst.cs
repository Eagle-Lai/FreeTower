/**
Create By LaiZhangYin, Eagle
if you have any question, please call wechat:782966734
**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class GlobalConst
    {
        /// <summary>
        /// 寻找间隔
        /// </summary>
        public const float  SearchRate = 0.05f; 
        /// <summary>
        /// 子弹速度
        /// </summary>
        public const float BulletSpeed = 20f;                                                                
        /// <summary>
        /// 敌人行走速度
        /// </summary>
        public const float EnemySpeed = 10f;
        /// <summary>
        /// 防御塔攻击间隔
        /// </summary>
        public const float FireInterval = 0.2f;
        /// <summary>
        /// 子弹重置间隔
        /// </summary>
        public const float BulletResetTimeInterval = 3f;

        /// <summary>
        /// 子弹缩放
        /// </summary>
        public const float BulletScale = 0.2f;
        
        /// <summary>
        /// 敌人缩放
        /// </summary>
        public const float EnemyScale = 1.2f;

        /// <summary>
        /// 点的坐标
        /// </summary>
        public static Vector3 PointVector3 = new Vector3(0, 0.5f, 0);

        /// <summary>
        /// 点的缩放
        /// </summary>
        public static Vector3 PointScale = new Vector3(2.0f, 0.2f, 2.0f);

        /// <summary>
        /// 未建造的时候Y点的坐标
        /// </summary>
        public const float UnbuildYPosition = 1.6f;

        /// <summary>
        /// 建造后Y点的坐标
        /// </summary>
        public const float BuildYPosition = 1.75f;
    }
}