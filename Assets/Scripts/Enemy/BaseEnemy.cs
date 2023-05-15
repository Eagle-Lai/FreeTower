using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using AStar;
using TMPro;
using cfg;

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

        protected List<Point> _pathPosition;

        private float _rotateSpeed = 180.0f;

        private TextMeshPro _HpTxt;

        private float _HpSpeed = 0;

        private TweenCallback _tweenCallback;

        private Rigidbody _rigidbody;

        public bool isRestart = false;

        public EnemyType _EnemyType;

        public EnemyDataItem _EnemyDataItem;

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

            EventDispatcher.AddEventListener(EventName.RefreshPathEvent, SetPath);
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
            StartMovePath();
            _EnemyType = EnemyType.NormalEnemy;
            _ArticleBlood = transform.Find("HpParent");
            _HpTransform = transform.Find("HpParent/Bg/Hp");
            _HpTxt = transform.Find("HpParent/EnemyHpTxt").GetComponent<TextMeshPro>();
            _HpTxt.gameObject.SetActive(false);
            _rigidbody = transform.GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
            _speed = _EnemyDataItem.EnemyStaticData.Speed;
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
            EventDispatcher.RemoveEventListener(EventName.RefreshPathEvent, SetPath);
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
            if (_pathPosition != null && _pathPosition.Count > 0)
            {
                if (_currentPositionIndex < _pathPosition.Count)
                {
                    Quaternion rot = FTProjectUtils.GetRotate(_pathPosition[_currentPositionIndex].position, transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rot, 120.0f * Time.deltaTime);
                    transform.Translate(Vector3.forward * _speed * Time.deltaTime);
                    float distance = FTProjectUtils.GetPointDistance(_pathPosition[_currentPositionIndex].position, transform.position);
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

        //private void FixedUpdate()
        //{
        //    _rigidbody.velocity = transform.rotation * Vector3.forward * 10;
        //}

        public void SetPath()
        {
            UpdatePath();
        }
        public void UpdatePath()
        {
            
            List<Point> path = AStarManager.Instance.UpdatePathByEnemyPoint(_pathPosition[_currentPositionIndex]);
            path.Reverse();
            path.Add(AStarManager.Instance.GetEndPoint());
            _currentPositionIndex = 0;
            _pathPosition = path;
        }
        /// <summary>
        /// 每次重置敌人后寻找的路线
        /// </summary>
        public void StartMovePath()
        {
            List<Point> path = AStarManager.Instance.GetPath();
            path.Reverse();
            path.Add(AStarManager.Instance.GetEndPoint());
            _pathPosition = path;
        }

        public virtual void EnemyStartMove()
        {
            EnemyState = EnemyState.Move;
            this.gameObject.SetActive(true);
            StartMovePath();
            transform.SetObjParent(EnemyManager.Instance.MoveEnemyParent, new Vector3(0, 1f, 0), Vector3.one * GlobalConst.EnemyScale);
            
        }

        public virtual void Reset()
        {
            EnemyManager.Instance.RecycleEnemy(_EnemyType, this);
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

        public void InitEnemyDataItem(EnemyData data)
        {
            if(_EnemyDataItem == null)
            {
                _EnemyDataItem = new EnemyDataItem(data);
                OnStart();
            }
        }

        public virtual void EnemyAwake()
        {

        }
    }
} 