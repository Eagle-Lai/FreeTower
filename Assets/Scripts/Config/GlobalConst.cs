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
        /// 玩家血量
        /// </summary>
        public const int PlayerHp = 10;
        /// <summary>
        /// 寻找间隔
        /// </summary>
        public const float  SearchRate = 0.05f; 
        /// <summary>
        /// 子弹速度
        /// </summary>
        public const float BulletSpeed = 25f;                                                                
        /// <summary>
        /// 敌人行走速度
        /// </summary>
        public const float EnemySpeed = 12f;
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
        public const float BulletScale = 1f;
        
        /// <summary>
        /// 敌人缩放
        /// </summary>
        public const float EnemyScale = 1.2f;

        /// <summary>
        /// 点的坐标
        /// </summary>
        public static Vector3 PointVector3 = new Vector3(0, 0f, 0);

        /// <summary>
        /// 点的缩放
        /// </summary>
        public static Vector3 PointScale = new Vector3(1, 1, 1);

        /// <summary>
        /// 未建造的时候Y点的坐标
        /// </summary>
        public const float UnbuildYPosition = 1.8f;

        /// <summary>
        /// 建造后的位置坐标
        /// </summary>
        public static Vector3 BuildYVector3 = new Vector3(0, 1.95f, 0);
        /// <summary>
        /// 建造后的模型缩放
        /// </summary>
        public static Vector3 BuildScale = new Vector3(1, 1, 1);

        /// <summary>
        /// 敌人血量
        /// </summary>
        public const int EnemyHp = 6;

        
        /// <summary>
        /// 回合数
        /// </summary>
        public const int RoundCount = 10;

        /// <summary>
        /// 敌人生成间隔
        /// </summary>
        public const float EnemyGenerateInterval = 1.0f;

        /// <summary>
        /// 一次生成的敌人数量
        /// </summary>
        public const int RoundEnemyNumber = 10;

        /// <summary>
        /// 玩家的金币
        /// </summary>
        public const int GoldCoin = 100;

        /// <summary>
        /// 建造防御塔的时候的间隔
        /// </summary>
        public const float BuildDistance = 1.0f;
    }
}