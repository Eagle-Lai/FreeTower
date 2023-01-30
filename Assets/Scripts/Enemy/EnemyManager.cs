using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{   
    public enum EnemyType
    {
        NONE = 0,
        NormalEnemy,
    }
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

        public override void OnInit()
        {
            base.OnInit();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public void GenerateEnemy(Transform parent) 
        {
            TimerManager.Instance.AddTimer(1f, 10, () => 
            {
                GetNormalEnemy(parent);
            });
        }

        public BaseEnemy GetNormalEnemy(Transform parent)
        {
            BaseEnemy baseEnemy = null;
            foreach (var item in enemyDictionary)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    if (item.Value[i].IsFree)
                    {
                        item.Value[i].IsFree = false;
                        item.Value[i].MoveToGoal(() =>
                        {
                            item.Value[i].IsFree = true;
                            item.Value[i].gameObject.transform.position = Vector3.zero;
                        });
                        return item.Value[i];
                    }
                }
            }
            ResourcesManager.Instance.LoadAndInitGameObject("NormalEnemy", parent, (go) =>
            {
                baseEnemy = new NormalEnemy();
                baseEnemy.gameObject = go;
                baseEnemy.gameObject.transform.localPosition = new Vector3(0, 1.2f, 0);
                baseEnemy.OnStart();
            });
            baseEnemy.IsFree = false;
            if(enemyDictionary.TryGetValue(EnemyType.NormalEnemy, out List<BaseEnemy> enemyList) == false)
            {
                enemyDictionary.Add(EnemyType.NormalEnemy, new List<BaseEnemy>());
            }
            enemyDictionary[EnemyType.NormalEnemy].Add(baseEnemy);
            baseEnemy.MoveToGoal(()=> 
            {
                baseEnemy.IsFree = true;
                baseEnemy.gameObject.transform.position = Vector3.zero;
            });
            return baseEnemy;
        }
    }
}