using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class EventName
    {
        /// <summary>
        /// 创建普通防御塔
        /// </summary>
        public const string BuildNormalTower = "BuildNormalTower";
        /// <summary>
        /// 抬起事件
        /// </summary>
        public const string OperateUp = "OperateUp";
        /// <summary>
        /// 敌人重置事件
        /// </summary>
        public const string EnemyResetEvent = "EnemyResetEvent";
        /// <summary>
        /// 更新寻路路径事件
        /// </summary>
        public const string UpdateAStarPath = "UpdateAStarPath";

        /// <summary>
        /// 玩家HP改变事件
        /// </summary>
        public const string PlayerHpChangeEvent = "PlayerHpChangeEvent";

        /// <summary>
        /// 回合改变事件
        /// </summary>
        public const string PlayerRoundCountChange = "PlayerRoundCountChange";

        /// <summary>
        /// 创建防御塔失败事件
        /// </summary>
        public const string BuildTowerFail = "BuildTowerFail";

        /// <summary>
        /// 创建防御塔成功事件
        /// </summary>
        public const string BuildTowerSuccess = "BuildTowerSuccess";

        /// <summary>
        /// 正在创建防御塔开始事件
        /// </summary>
        public const string BuildingTower = "BuildingTower";
    }
}