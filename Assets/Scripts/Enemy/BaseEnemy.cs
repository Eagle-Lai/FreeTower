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
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        protected float _Hp;
        public float Hp
        {
            get { return _Hp; }
            set { _Hp = value; }
        }
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
        }


        protected virtual void OnStart()
        {
            UpdatePath();
        }



        protected virtual void OnUpdate()
        {
            if (EnemyState == EnemyState.Move)
            {
                MoveToGoal();
            }
        }



        protected virtual void Clear()
        {

        }

        public virtual void Hit()
        {
            Reset();
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

        public void UpdatePath()
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
            EventDispatcher.TriggerEvent<BaseEnemy>(HandlerName.EnemyResetEvent, this);
        }
    }
} 