/**
Create By LaiZhangYin, Eagle
if you have any question, please call wechat:782966734
**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

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

        protected bool _isFree;
        public bool IsFree
        {
            get { return _isFree; }
            set { _isFree = value; }
        }

        protected bool _isMoveState;
        public bool IsMoveState
        {
            get
            {
                return _isMoveState;
            }
            set
            {
                _isMoveState = value;
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
        protected virtual void OnAwake()
        {

        }


        protected virtual void OnStart()
        {

        }



        protected virtual void OnUpdate()
        {

        }



        protected virtual void Clear()
        {

        }

        public void MoveToGoal(Action callback = null)
        {
            List<Node> nodes = GridManager.Instance.GetPath();
            Vector3[] poss = new Vector3[nodes.Count];
            for (int i = 0; i < nodes.Count; i++)
            {
                poss[i] = nodes[i].position;
            }
            if (_isMoveState) return;
            _isMoveState = true;
            transform.DOLocalPath(poss, _speed).onComplete = ()=> 
            { 
                callback();
                Reset();
            };
        }

        public virtual void Reset()
        {
            this.gameObject.transform.position = Vector3.zero;
            IsFree = true;
            _isMoveState = false;
        }
    }
} 