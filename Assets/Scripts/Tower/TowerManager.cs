/**
Create By LaiZhangYin, Eagle
if you have any question, please call wechat:782966734
**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public enum TowerType 
    { 
        None = 0,
        Normal,
        Power,
        Slow,
    }

    public class TowerManager 
    {
        private Dictionary<TowerType, string> towerDictionary = new Dictionary<TowerType, string>() { { TowerType.Normal, "NormalTower" },
                        {TowerType.Power, "PowerTower" },
                        {TowerType.Slow, "SlowTower" },
                        };

        private static TowerManager _instance;
        public static TowerManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new TowerManager();
                }
                return _instance;
            }
        }

        public TowerManager()
        {
        }
        
        public T GetTower<T>(TowerType type) where T : BaseTower, new()
        {
            GameObject gameObject = ResourcesManager.Instance.LoadAndInitGameObject(towerDictionary[type]);
            T t = gameObject.AddComponent<T>();
            return t;
        }
    }
}