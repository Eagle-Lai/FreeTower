using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace FTProject
{
    public class BaseTower : MonoBehaviour
    {
        public SphereCollider _sphereCollider;

        protected List<GameObject> _enemyList = new List<GameObject>();

        protected Transform _bulletPoint;

        protected Transform _head;

        protected TowerPosition _towerPosition;

        protected float _rotateSpeed = 120f;
        public float RotateSpeed
        {
            get
            {
                return _rotateSpeed;
            }
            set
            {
                _rotateSpeed = value;
            }
        }

        private void Awake()
        {
            this.OnAwake();
        }
        private void Start()
        {
            OnStart();
        }
        protected void Update()
        {
            OnUpdate();
        }
        private void OnDestroy()
        {
            Clear();
        }
        private void OnTriggerEnter(Collider other)
        {
            TriggerGameObjectEnter(other);
        }
        private void OnTriggerExit(Collider other)
        {
            TriggerGameObjectExit(other);

        }



        protected virtual void OnAwake()
        {

        }

     
        protected virtual void OnStart()
        {
            _sphereCollider = transform.GetComponent<SphereCollider>();
            _sphereCollider.radius = TowerCofig.Radius;
            _head = transform.Find("Head");
            _bulletPoint = transform.Find("Cube/BulletPoint");
            _towerPosition = transform.GetComponentInChildren<TowerPosition>();
        }

      

        protected virtual void OnUpdate()
        {
            GameObject temp = GetNearsetTarget();
            if (temp != null)
            {
                Quaternion rot = GetEnemyRotate(temp);
                _head.rotation = Quaternion.Slerp(_head.rotation, rot, _rotateSpeed * Time.deltaTime);
            }
        }

        public Quaternion GetEnemyRotate(GameObject go)
        {
           return Quaternion.LookRotation(new Vector3(transform.position.x - go.transform.position.x, 0, transform.position.z - go.transform.position.z));
        }



        protected virtual void Clear()
        {

        }

        protected virtual void TriggerGameObjectEnter(Collider other)
        {
            if (_towerPosition.isBuild)
            {
                if (other.gameObject.name.Contains("Enemy"))
                {
                    _enemyList.Add(other.gameObject);
                }
            }
        }


        protected virtual void TriggerGameObjectExit(Collider other)
        {
            if (_towerPosition.isBuild)
            {
                _enemyList.Remove(other.gameObject);
            }
        }

        public GameObject GetNearsetTarget()
        {
            if (_enemyList.Count <= 0) return null;
            GameObject nearest = null;
            float distance = float.MaxValue;
            for (int i = _enemyList.Count - 1; i > 0; i--)
            {
                if(_enemyList[i] == null)
                {
                    return null;
                }
                float currentDistance = FTProjectUtils.GetPointDistance(gameObject, _enemyList[i]);
                if(currentDistance < distance)
                {
                    nearest = _enemyList[i];
                    distance = currentDistance;
                }
            }
            return nearest;
        }
    }
}