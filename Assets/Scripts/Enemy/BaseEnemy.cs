using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject {
    public class BaseEnemy
    {
        public BaseEnemy()
        {
            Init();
        }

        public BaseEnemy(GameObject go)
        {
            this._go = go;
            Init();
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

        
        public virtual void Init()
        {
           
        }

        public virtual void OnStart()
        {

        }

        public virtual void OnDestroy()
        {
        }

        public virtual void OnUpdate()
        {
            
        }
    }
} 