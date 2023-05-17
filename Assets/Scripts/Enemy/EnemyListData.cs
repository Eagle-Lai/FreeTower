using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg;                               

namespace FTProject
{
    public class EnemyListData
    {
        public EnemyList _EnemyList;

        public bool isStart;

        /// <summary>
        /// 这个波次每个敌人间出现的间隔
        /// </summary>
        public float interval;

        public float _ListInterval;
        public EnemyListData()
        {
            AddEventListener();                
        }

        public void SetEnemyListData(EnemyList list)
        {
            _EnemyList = list;
            this.interval = list.EnemyInterval / 1000;
            isStart = true;
            _ListInterval = list.Interval / 1000;
        }

        private void AddEventListener()
        {
            EventDispatcher.AddEventListener(EventName.UpdateEvent, Update);
        }

        public void Update()
        {
            if (isStart)
            {
                if(_EnemyList != null)
                {
                    _ListInterval -= Time.deltaTime;
                    if(_ListInterval < 0)
                    {
                        Debug.LogError("=================================================================");
                        isStart = false;
                        for (int i = 0; i < _EnemyList.EnemyIndexs.Count; i++)
                        {
                            int index = _EnemyList.EnemyIndexs[i];
                            EnemyData data = Launcher.Instance.Tables.TBEnemyData.Get(index);
                            float time = i * interval;
                            Debug.Log(time);
                            TimerManager.Instance.AddTimer(time, 1, () =>
                            {
                                EnemyManager.Instance.CreateEnemy(data.Id);
                            });
                        }
                    }
                }
            }
        }

        public void OnDestroy()
        {
            EventDispatcher.RemoveEventListener(EventName.UpdateEvent, Update);
        }
    }
}
