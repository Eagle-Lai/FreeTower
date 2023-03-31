/**
Create By LaiZhangYin, Eagle
if you have any question, please call wechat:782966734
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class Targetter : MonoBehaviour
    {

        public SphereCollider _sphereCollider;

        protected List<GameObject> _enemyList = new List<GameObject>(); 
        

        private void Awake()
        {
            EventDispatcher.AddEventListener<BaseEnemy>(EventName.EnemyResetEvent, EnemyReset);
            _sphereCollider = transform.GetComponent<SphereCollider>();
            if (_sphereCollider == null)
            {
                _sphereCollider = gameObject.AddComponent<SphereCollider>();
            }
            _sphereCollider.isTrigger = true;
            _sphereCollider.radius = TowerCofig.Radius;
        }

        private void Start()
        {
           
        }

        private void EnemyReset(BaseEnemy baseEnemy)
        {
            if(baseEnemy != null)
            {
                _enemyList.Remove(baseEnemy.gameObject);
            }
        }

        private void OnDestroy()
        {
            EventDispatcher.RemoveEventListener<BaseEnemy>(EventName.EnemyResetEvent, EnemyReset);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name.Contains("Enemy"))
            {
                _enemyList.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_enemyList.Count > 0)
            {
                _enemyList.Remove(other.gameObject);
            }
        }

        public GameObject GetNearsetTarget()
        {
            if (_enemyList.Count <= 0) return null;
            return _enemyList[0];
        }
    }
}
