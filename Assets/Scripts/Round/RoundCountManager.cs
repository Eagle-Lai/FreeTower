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

        public RoundData _roundData;

        public List<int> _EnemyList = new List<int>();

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

        private void GenerateEnemy()
        {
            EnemyManager.Instance.GenerateEnemy();
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
            _roundData = Launch.Instance.Tables.TBRoundData.Get(_currentIndex);
            _EnemyList = _roundData.EnemyIndexs;
        }

        public List<int> GetEnemyList()
        {
            if(_EnemyList != null)
            {
                return _EnemyList;
            }
            return null;
        }
    }
}
