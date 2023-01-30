using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace FTProject {
    public class BaseEnemy : BaseDisplayObject
    {
        public BaseEnemy()
        {
            OnInit();
        }

        public BaseEnemy(GameObject go)
        {
            this._go = go;
            OnInit();
        }

        protected GameObject _go;
        public GameObject gameObject
        {
            get { return _go; }
            set { _go = value; }
        }

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
        public override void OnInit()
        {
            base.OnInit();
        }

        public override void OnStart()
        {
            base.OnStart();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
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
            _go.transform.DOLocalPath(poss, _speed).onComplete = ()=> 
            { 
                callback();
                _isMoveState = false;
            };
        }
    }
} 