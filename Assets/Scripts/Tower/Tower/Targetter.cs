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
            _sphereCollider = transform.GetComponent<SphereCollider>();
            if(_sphereCollider == null)
            {
                _sphereCollider = gameObject.AddComponent<SphereCollider>();
            }
            _sphereCollider.isTrigger = true;
            _sphereCollider.radius = TowerCofig.Radius;
        }

        private void Start()
        {
            
        }

        private void OnDestroy()
        {
            
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
            _enemyList.Remove(other.gameObject);
        }

        public GameObject GetNearsetTarget()
        {
            if (_enemyList.Count <= 0) return null;
            //GameObject nearest = null;
            //float distance = float.MaxValue;
            //for (int i = _enemyList.Count - 1; i > 0; i--)
            //{
            //    if (_enemyList[i] == null)
            //    {
            //        return null;
            //    }
            //    float currentDistance = FTProjectUtils.GetPointDistance(gameObject, _enemyList[i]);
            //    if (currentDistance < distance)
            //    {
            //        nearest = _enemyList[i];
            //        distance = currentDistance;
            //    }
            //}
            //return nearest;
            return _enemyList[0];
        }
    }
}
