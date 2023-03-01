using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using AStar;
using TMPro;

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

        private float _rotateSpeed = 180.0f;

        private TextMeshPro _HpTxt;

        private float _HpSpeed = 0;

        private TweenCallback _tweenCallback;

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
            EventDispatcher.AddEventListener(EventName.UpdateEvent, MyUpdate);
            _currentPositionIndex = 0;
            _speed = GlobalConst.EnemySpeed;
            EnemyState = EnemyState.Idle;

            EventDispatcher.AddEventListener(EventName.UpdateAStarPath, SetPath);
            EventDispatcher.AddEventListener(EventName.BuildTowerSuccess, SetPath);
            EventDispatcher.AddEventListener<BaseTower>(EventName.DestroyTower, DestroyTower);

            _CurrentHp = GlobalConst.EnemyHp;
            _TotalHp = GlobalConst.EnemyHp;
        }
         
        private void DestroyTower(BaseTower baseTower)
        {
            SetPath();
        }

        protected virtual void OnStart()
        {
            SetPath();
            _ArticleBlood = transform.Find("HpParent");
            _HpTransform = transform.Find("HpParent/Bg/Hp");
            _HpTxt = transform.Find("HpParent/EnemyHpTxt").GetComponent<TextMeshPro>();
            _HpTxt.gameObject.SetActive(false);
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
            //ʹ��  Vector3.ProjectOnPlane ��ͶӰ������ͶӰƽ�淨���������ڼ���ĳ��������ĳ��ƽ���ϵ�ͶӰ����  
            Vector3 lookPoint = Vector3.ProjectOnPlane(this._ArticleBlood.transform.position - Camera.main.transform.position, Camera.main.transform.forward);
            this._ArticleBlood.LookAt(Camera.main.transform.position + lookPoint);
            Quaternion rot = FTProjectUtils.GetRotate(Camera.main.transform.position, _ArticleBlood.position);
        }


        protected virtual void Clear()
        {
            EventDispatcher.RemoveEventListener(EventName.UpdateEvent, MyUpdate);
            EventDispatcher.RemoveEventListener(EventName.UpdateAStarPath, SetPath);
            EventDispatcher.RemoveEventListener(EventName.BuildTowerSuccess, SetPath);
            EventDispatcher.RemoveEventListener<BaseTower>(EventName.DestroyTower, DestroyTower);
        }

        public virtual void Hit()
        {
            if(_CurrentHp > 0)
            {
                UpdateHp();
            }
            else
            {
                Reset();
            }
        }

        public void Hurt(float hurt)
        {
            _HpTxt.gameObject.SetActive(true);
            _HpTxt.DOFade(0, 2f);
            _tweenCallback = _HpTxt.transform.DOLocalMoveY(2f, 2f).onComplete = HPTxt;
            _CurrentHp -= hurt;
            _HpTxt.text = _CurrentHp.ToString();
        }

        private void HPTxt()
        {
            _HpTxt.DOFade(1, 0f);
            ResetHpTxt();
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
            List<Point> path = AStarManager.Instance.GetPath();
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
            ResetHpTxt();
        }

        private void ResetHpTxt()
        {
            _HpTxt.transform.localPosition = new Vector3(0, 1, 0);
            _HpTxt.gameObject.SetActive(false);
        }
    }
} 