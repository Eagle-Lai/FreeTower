using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public static class EventName
    {
        /// <summary>
        /// 创建普通防御塔
        /// </summary>
        public static string BuildNormalTower = "BuildNormalTower";
        /// <summary>
        /// 抬起事件
        /// </summary>
        public static string OperateUp = "OperateUp";
        /// <summary>
        /// 敌人重置事件
        /// </summary>
        public static string EnemyResetEvent = "EnemyResetEvent";
        /// <summary>
        /// 更新寻路路径事件
        /// </summary>
        public static string UpdateAStarPath = "UpdateAStarPath";
    }
}