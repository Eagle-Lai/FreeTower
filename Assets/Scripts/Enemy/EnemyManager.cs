using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{   

    public class EnemyManager : BaseManager
    {
        private static EnemyManager _instance;
        public static EnemyManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new EnemyManager();
                }
                return _instance;
            }
        }
        public Dictionary<EnemyType, List<BaseEnemy>> enemyDictionary = new Dictionary<EnemyType, List<BaseEnemy>>();

        public Transform EnemyParent;

        public override void OnInit()
        {
            base.OnInit();
            EnemyParent = GameObject.Find("EnemyParent").transform;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public void GenerateEnemy() 
        {
            TimerManager.Instance.AddTimer(1f, 10, () => 
            {
                GetNormalEnemy<NormalEnemy>(EnemyType.NormalEnemy);
            });
        }

        public T GetNormalEnemy<T>(EnemyType type) where T : BaseEnemy
        {
            //foreach (var item in enemyDictionary)
            //{
            //    for (int i = 0; i < item.Value.Count; i++)
            //    {
            //        if (item.Value[i].IsFree)
            //        {
            //            item.Value[i].IsFree = false;
            //            item.Value[i].MoveToGoal(() =>
            //            {
            //                item.Value[i].IsFree = true;
            //                item.Value[i].gameObject.transform.position = Vector3.zero;
            //            });
            //            return item.Value[i] as T;
            //        }
            //    }
            //}
            if(enemyDictionary.TryGetValue(type, out List<BaseEnemy> list))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var item = list[i];
                    if (item.IsFree)
                    {
                        item.IsFree = false;
                        item.MoveToGoal();
                        return item as T;
                    }

                }
            }
            T t = null;
            if (EnemyParent == null)
            {
                EnemyParent = GameObject.Find("EnemyParent").transform;
            }
            ResourcesManager.Instance.LoadAndInitGameObject("NormalEnemy", EnemyParent, (go) =>
            {
                t = go.AddComponent<T>();
                t.transform.localPosition = new Vector3(0, 1.2f, 0);
            });
            t.IsFree = false;
            if(enemyDictionary.TryGetValue(EnemyType.NormalEnemy, out List<BaseEnemy> enemyList) == false)
            {
                enemyDictionary.Add(EnemyType.NormalEnemy, new List<BaseEnemy>());
            }
            enemyDictionary[EnemyType.NormalEnemy].Add(t);
            t.MoveToGoal();
            return t;
        }

        public void RecycleEnemy(EnemyType type, BaseEnemy baseEnemy)
        {
            if(enemyDictionary.TryGetValue(type, out List<BaseEnemy> list))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if(list[i] == baseEnemy)
                    {
                        list[i].IsFree = true;
                        list[i].Reset();
                    }
                }
            }
        }
    }
}