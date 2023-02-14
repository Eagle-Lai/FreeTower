using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using AStar;

namespace FTProject {
    public class BaseEnemy : MonoBehaviour
    {
        protected float _speed;

        protected Transform _ArticleBlood;

        protected Transform _HpTransform;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        protected float _TotalHp;

        protected float _CurrentHp;
       
        public EnemyState EnemyState;

        protected int _currentPositionIndex;

        protected Vector3[] _pathPosition;

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
            _currentPositionIndex = 0;
            _speed = GlobalConst.EnemySpeed;
            EnemyState = EnemyState.Idle;
            EventDispatcher.AddEventListener(EventName.UpdateAStarPath, SetPath);
            _CurrentHp = GlobalConst.EnemyHp;
            _TotalHp = GlobalConst.EnemyHp;
        }
         

        protected virtual void OnStart()
        {
            SetPath();
            _ArticleBlood = transform.Find("HpParent");
            _HpTransform = transform.Find("HpParent/Bg/Hp");
        }



        protected virtual void OnUpdate()
        {
            if (EnemyState == EnemyState.Move)
            {
                MoveToGoal();
            }
            LookAtCamera();
        }

        protected void LookAtCamera()
        {
            //使用  Vector3.ProjectOnPlane （投影向量，投影平面法向量）用于计算某个向量在某个平面上的投影向量  
            Vector3 lookPoint = Vector3.ProjectOnPlane(this._ArticleBlood.transform.position - Camera.main.transform.position, Camera.main.transform.forward);
            this._ArticleBlood.LookAt(Camera.main.transform.position + lookPoint);
            Quaternion rot = FTProjectUtils.GetRotate(Camera.main.transform.position, _ArticleBlood.position);
            _ArticleBlood.rotation = Quaternion.Slerp(_ArticleBlood.rotation, rot, 120.0f * Time.deltaTime);
        }


        protected virtual void Clear()
        {
            
        }

        public virtual void Hit()
        {
            if(_CurrentHp > 0)
            {
                _CurrentHp--;
                UpdateHp();
            }
            else
            {
                Reset();
            }
        }

        protected void UpdateHp()
        {
            _HpTransform.localScale = new Vector3(_CurrentHp / _TotalHp, 1, 1);
        }
        

        public void MoveToGoal(Action callback = null)
        {
            if (_pathPosition != null && _pathPosition.Length > 0)
            {
                if (_currentPositionIndex < _pathPosition.Length)
                {
                    Quaternion rot = FTProjectUtils.GetRotate(_pathPosition[_currentPositionIndex], transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rot, 120.0f * Time.deltaTime);
                    transform.Translate(Vector3.forward * _speed * Time.deltaTime);
                    float distance = FTProjectUtils.GetPointDistance(_pathPosition[_currentPositionIndex], transform.position);
                    if (distance < 0.1f)
                    {
                        _currentPositionIndex++;
                    }
                }
                else
                {
                    Reset();
                }
            }
        }

        public void SetPath()
        {
            List<Point> path = AStarManager.Instance.GetAStarPath();
            path.Reverse();
            path.Add(AStarManager.Instance.GetEndPoint());
            _pathPosition = new Vector3[path.Count];
            for (int i = 0; i < path.Count; i++)
            {
                _pathPosition[i] = path[i].position;
            }
            
        }

        public virtual void EnemyAttack()
        {
            EnemyState = EnemyState.Move;
            this.gameObject.SetActive(true);
            transform.SetObjParent(EnemyManager.Instance.MoveEnemyParent, new Vector3(0, 1f, 0), Vector3.one * GlobalConst.EnemyScale);
        }

        public virtual void Reset()
        {
            EnemyState = EnemyState.Idle;
            _currentPositionIndex = 0;
            transform.SetObjParent(EnemyManager.Instance.IdleEnemyParent, new Vector3(0, 1f, 0), Vector3.one * GlobalConst.EnemyScale);
            this.gameObject.SetActive(false);
            EventDispatcher.TriggerEvent<BaseEnemy>(EventName.EnemyResetEvent, this);
            _CurrentHp = GlobalConst.EnemyHp;
            _ArticleBlood.rotation = Quaternion.identity;
            _HpTransform.transform.localScale = Vector3.one;
        }
    }
} 