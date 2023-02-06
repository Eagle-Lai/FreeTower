/**
Create By LaiZhangYin, Eagle
if you have any question, please call wechat:782966734
**/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace FTProject
{
    public class BaseTower : MonoBehaviour
    {

        protected GameObject currentTargetGameObject;

        protected Transform _bulletPoint;

        protected Transform _head;

        protected TowerPosition _towerPosition;

        protected Targetter _targetter;

        protected float _searchRate;
        public float SearchRate
        {
            get { return _searchRate; }
            set { _searchRate = value; }
        }

        protected float _searchTimer;

        protected float _rotateSpeed = 150f;
        public float RotateSpeed
        {
            get { return _rotateSpeed; }
            set { _rotateSpeed = value; }
        }
        public float FireInterval
        {
            get { return GlobalConst.FireInterval; }
        }

        protected float _fireTimer;

        

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

        protected virtual void OnAwake()
        {
            this._searchRate = GlobalConst.SearchRate;
            
            _targetter = transform.Find("Targetter").GetComponent<Targetter>();
            if (_targetter == null)
            {
                _targetter = transform.Find("Targetter").gameObject.AddComponent<Targetter>();
            }
            _bulletPoint = transform.Find("Head/Cube/BulletPoint").transform;
        }


        protected virtual void OnStart()
        {

            _head = transform.Find("Head");
            _bulletPoint = transform.Find("Cube/BulletPoint");
            _towerPosition = transform.GetComponentInChildren<TowerPosition>();
        }


        protected virtual void OnUpdate()
        {
            currentTargetGameObject = _targetter.GetNearsetTarget();
            _searchTimer -= Time.deltaTime;
            if (currentTargetGameObject != null && _searchTimer <= 0)
            {
                Quaternion rot = FTProjectUtils.GetRotate(currentTargetGameObject.transform, gameObject);
                _head.rotation = Quaternion.Slerp(_head.rotation, rot, _rotateSpeed * Time.deltaTime);
                _searchTimer = _searchRate;
            }
            _fireTimer -= Time.deltaTime;
            if (IsCanFire())
            {
                _fireTimer = FireInterval;
                TowerAttack();
            }
        }

        protected virtual bool IsCanFire()
        {
            return currentTargetGameObject != null && _fireTimer <= 0 && _towerPosition.TowerBuildState == TowerBuildState.Build;
        }

        protected virtual void TowerAttack()
        {
           
        }
        
        protected virtual void Clear()
        {

        }
    }
}