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

        public override void OnInit()
        {
            base.OnInit();
            //IdleEnemyParent = GameObject.Find("IdleEnemyParent").transform;
            //MoveEnemyParent = GameObject.Find("MoveEnemyParent").transform;

        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public void GenerateEnemyByList(int index)
        {
            List<int> list = Launcher.Instance.Tables.TBEnemyList.Get(index).EnemyIndexs;
            List<int> interval = Launcher.Instance.Tables.TBEnemyList.Get(index).Interval;
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
                                    normal.EnemyAttack();
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
            enemy.EnemyAttack();
            enemyDictionary[EnemyType.NormalEnemy].Add(enemy);
            return enemy;
        }

        public void RecycleEnemy(EnemyType type, BaseEnemy baseEnemy)
        {
            baseEnemy.Reset();
        }


    }
}