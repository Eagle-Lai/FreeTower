using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg;

namespace FTProject
{

    public class RoundCountManager : BaseManager<RoundCountManager>
    {
        public SceneInfo _currentSceneInfo;

        public int _currentIndex = 0;

        public List<RoundData> _roundData;

        public List<int> _EnemyList = new List<int>();
        public List<int> _IntervalList = new List<int>();

        public List<int> _roundIndexs;
        public List<int> _intervalList;

        public override void OnInit()
        {
            EventDispatcher.AddEventListener(EventName.UpdateEvent, MyUpdate);
            base.OnInit();
            InitRoundInfo();
        }

        private void InitRoundInfo()
        {;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            EventDispatcher.RemoveEventListener(EventName.UpdateEvent, MyUpdate);
        }

        private void MyUpdate()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GenerateEnemyByInfoItem();
            }
        }


        public void GenerateEnemyByInfoItem()
        {
            
        }

        /// <summary>
        /// 设置场景信息
        /// </summary>
        /// <param name="info"></param>
        public void SetSceneInfo(SceneInfo info)
        {
            _currentSceneInfo = info;
            UpdateRoundInfo();
        }
        /// <summary>
        /// 设置轮次信息
        /// </summary>
        public void UpdateRoundInfo()
        {
            _currentIndex++;
        }

        public void GenerateEnemyByRoundInfo()
        {
            _currentIndex++;
            _roundIndexs  = Launch.Instance.Tables.TBRoundData.Get(_currentIndex).EnemyIndexs;
            _intervalList = Launch.Instance.Tables.TBRoundData.Get(_currentIndex).Interval;
            Debug.Log(_roundIndexs.Count);
            Debug.Log(_intervalList.Count);
            int index = 0;
            for (int i = 0; i < _roundIndexs.Count; i++)
            {
                TimerManager.Instance.AddTimer(_intervalList[i] / 1000 + i * 1, 1, () => 
                {
                    EnemyManager.Instance.GenerateEnemyByList(_roundIndexs[index]);
                    index++;
                }, false);
            }
        }

        public List<int> GetEnemyList()
        {
            UpdateRoundInfo();
            if(_EnemyList != null)
            {
                return _EnemyList;
            }
            return null;
        }

        public List<int> GetIntervalList()
        {
            if(_IntervalList != null)
            {
                return _IntervalList;
            }
            return null;
        }
    }
}
