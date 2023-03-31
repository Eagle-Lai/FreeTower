using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg;

namespace FTProject
{

    public class EnemyManager : BaseManager<EnemyManager>
    {
        public Dictionary<EnemyType, List<BaseEnemy>> enemyDictionary = new Dictionary<EnemyType, List<BaseEnemy>>();

        public Transform IdleEnemyParent;

        public Transform MoveEnemyParent;

        private Dictionary<int, List<EnemyData>> _enemyListDictionary = new Dictionary<int, List<EnemyData>>();

        /// <summary>
        /// 场次的序号
        /// </summary>
        private int _roundIndex = 0;

        public int RoundIndex
        {
            get { return _roundIndex; }
        }
        /// <summary>
        /// 每一场次敌人列表的序号,每场开始都会归零
        /// </summary>
        public int _EnemyListIndex = 0;

        public override void OnInit()
        {
            base.OnInit();
            //IdleEnemyParent = GameObject.Find("IdleEnemyParent").transform;
            //MoveEnemyParent = GameObject.Find("MoveEnemyParent").transform;
            EventDispatcher.AddEventListener(EventName.GenerateEnemyEvent, GenerateEnemy);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            EventDispatcher.RemoveEventListener(EventName.GenerateEnemyEvent, GenerateEnemy);
        }

        public void GenerateEnemy()
        {
            BaseGameScene info = GameSceneManager.Instance.GetCurrentSceneInfo();
            _roundIndex++;
            if(_roundIndex >= info._SceneInfo.RoundList.Count)
            {
                _roundIndex = 0;
            }
            int index = info._SceneInfo.RoundList[_roundIndex];
            Debug.Log(string.Format("第{0}波敌人", _roundIndex));
            GenerateEnemyByRoundInfo(index);
        }

        public void GenerateEnemyByRoundInfo(int _currentIndex)
        {
            
            if (_currentIndex >= Launcher.Instance.Tables.TBRoundData.DataList.Count - 1)
            {
                _currentIndex = 1;
            }
            List<int> _roundIndexs = Launcher.Instance.Tables.TBRoundData.Get(_currentIndex).EnemyIndexs;
            int interval = Launcher.Instance.Tables.TBRoundData.Get(_currentIndex).Interval;
            FTProjectUtils.LogList(_roundIndexs, "敌人波次数据:______");
            int index = 0;
            for (int i = 0; i < _roundIndexs.Count; i++)
            {
                TimerManager.Instance.AddTimer(interval * i / 1000 + i * 1, 1, () =>
                {
                    GenerateEnemyByList(_roundIndexs[index]);
                    index++;
                }, false);
            }
        }

        public void GenerateEnemyByList(int index)
        {
            List<int> list = Launcher.Instance.Tables.TBEnemyList.Get(index).EnemyIndexs;
            int interval = Launcher.Instance.Tables.TBEnemyList.Get(index).Interval;
            FTProjectUtils.LogList(list, "敌人列表数据");
            for (int i = 0; i < list.Count; i++)
            {
                EnemyData data = Launcher.Instance.Tables.TBEnemyData.Get(list[i]);
                if (data != null)
                {                                                                           
                    switch (data.Type)
                    {
                        case 1:
                            float timer = data.Interval / 1000 + (i * 0.2f);
                            TimerManager.Instance.AddTimer(timer, 1, () =>
                                {
                                    NormalEnemy normal = CreateEnemy<NormalEnemy>(EnemyType.NormalEnemy, data.Name);
                                    normal.EnemyStartMove();
                                }, false
                             );
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public T CreateEnemy<T>(EnemyType type, string name) where T : BaseEnemy
        {
            if (enemyDictionary.TryGetValue(type, out List<BaseEnemy> list))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var item = list[i];
                    if(item.EnemyState == EnemyState.Idle)
                    {
                        return item as T;
                    }
                }
            }
            if (IdleEnemyParent == null)
            {
                IdleEnemyParent = GameObject.Find("IdleEnemyParent").transform;
                MoveEnemyParent = GameObject.Find("MoveEnemyParent").transform;
            }
            T enemy = null;
            ResourcesManager.Instance.LoadAndInitGameObject(name, IdleEnemyParent, (go) =>
            {
                enemy = go.AddComponent<T>();
                enemy.transform.localPosition = new Vector3(0, 1f, 0);
            });
            if (enemyDictionary.TryGetValue(EnemyType.NormalEnemy, out List<BaseEnemy> enemyList) == false)
            {
                enemyDictionary.Add(EnemyType.NormalEnemy, new List<BaseEnemy>());
            }
            enemy.EnemyStartMove();
            enemyDictionary[EnemyType.NormalEnemy].Add(enemy);
            return enemy;
        }

        public void RecycleEnemy(EnemyType type, BaseEnemy baseEnemy)
        {
            baseEnemy.Reset();
        }
    }
}