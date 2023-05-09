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

        public TowerPosition TowerPosition;

        protected Targetter _targetter;

        public bool isCanBuild = false;


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

        public int Level;

        public string TowerName;

        public TowerType TowerType;

        public TowerJsonData TowerJsonData;

        public MeshRenderer[] _towerMesh;

        public Transform _Cylinder;

        private void Awake()
        {
            this.OnAwake();
        }
        private void Start()
        {
            OnStart();
        }
        protected void MyUpdate()
        {
            OnUpdate();
        }
        private void OnDestroy()
        {
            Clear();
        }

        protected virtual void OnAwake()
        {
            TowerPosition = transform.GetComponentInChildren<TowerPosition>();

            _targetter = transform.Find("Targetter").GetComponent<Targetter>();
            _towerMesh = transform.GetComponentsInChildren<MeshRenderer>();
            _Cylinder = transform.Find("Cylinder/Cylinder").transform;
        }


        protected virtual void OnStart()
        {
            EventDispatcher.AddEventListener(EventName.UpdateEvent, MyUpdate);
           
            
            this._searchRate = GlobalConst.SearchRate;
            _head = transform.Find("Head");
            _bulletPoint = transform.Find("Head/BulletPoint");
           
            if (_targetter == null)
            {
                _targetter = transform.Find("Targetter").gameObject.AddComponent<Targetter>();
            }
            _bulletPoint = transform.Find("Head/BulletPoint").transform;
        }


        protected virtual void OnUpdate()
        {
            RefreshTowerColor();
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
            bool isFire = currentTargetGameObject != null && _fireTimer <= 0 && TowerPosition.TowerBuildState == TowerBuildState.Build;
            return isFire;
        }

        protected virtual void TowerAttack()
        {
           
        }
        
        protected virtual void Clear()
        {
            EventDispatcher.RemoveEventListener(EventName.UpdateEvent, MyUpdate);
        }

        public void SetTowerJsonData(TowerJsonData JsonData)
        {
            TowerJsonData = JsonData;
            UpdateTowerByData();
        }

        public void UpdateTowerByData()
        {
            if(TowerJsonData != null)
            {
                
            }
        }

        public void SetBuildSuccessWithJson()
        {
            TowerPosition.SetBuildSuccess();
            SetTowerColor(Color.white);
            _Cylinder.gameObject.SetActive(false);
        }

        public void SetBuildSuccess()
        {
            SetTowerColor(Color.white);
            _Cylinder.gameObject.SetActive(false);
        }

        public void DestroyTower()
        {
            ResetBullet();
        }

        public void ResetBullet()
        {
            if (_bulletPoint.childCount > 0)
            {
                BaseBullet[] bullets = _bulletPoint.transform.GetComponentsInChildren<BaseBullet>();
                for (int i = 0; i < bullets.Length; i++)
                {
                    bullets[i].Reset();
                }
            }
        }

        public void ResetTowerScale(Transform parent)
        {
            transform.SetParent(null);
            transform.localScale = Vector3.one;
            
            transform.SetParent(parent);
            transform.localPosition = new Vector3(0, 6.5f, 0);
        }

        public void RefreshTowerColor()
        {
            if (!TowerPosition.isBuild)
            {
                if (isCanBuild)
                {
                    SetTowerColor(Color.green);
                }
                else
                {
                    SetTowerColor(Color.red);
                }
            }
        }

        public void SetTowerColor(Color color)
        {
            if (_towerMesh.Length > 0)
            {
                for (int i = 0; i < _towerMesh.Length; i++)
                {
                    _towerMesh[i].material.color = color;
                }
            }
        }


    }
}