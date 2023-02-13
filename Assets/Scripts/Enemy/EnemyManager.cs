using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{

    public class EnemyManager : BaseManager<EnemyManager>
    {
        public Dictionary<EnemyType, List<BaseEnemy>> enemyDictionary = new Dictionary<EnemyType, List<BaseEnemy>>();

        public Transform IdleEnemyParent;

        public Transform MoveEnemyParent;

        public override void OnInit()
        {
            base.OnInit();
            IdleEnemyParent = GameObject.Find("IdleEnemyParent").transform;
            MoveEnemyParent = GameObject.Find("MoveEnemyParent").transform;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public void GenerateEnemy()
        {
            TimerManager.Instance.AddTimer(1f, 10, () =>
            {
                NormalEnemy enemy = CreateEnemy<NormalEnemy>(EnemyType.NormalEnemy);
                enemy.EnemyAttack();
            });
        }

        public T CreateEnemy<T>(EnemyType type) where T : BaseEnemy
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
            ResourcesManager.Instance.LoadAndInitGameObject("NormalEnemy", IdleEnemyParent, (go) =>
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