/**
Create By LaiZhangYin, Eagle
if you have any question, please call wechat:782966734
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FTProject
{
    public class BaseBullet : MonoBehaviour
    {
        protected float _speed;
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        protected bool _isFree;
        public bool IsFree
        {
            get { return _isFree; }
            set { _isFree = value; }
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
            this._speed = GlobalConst.BulletSpeed;
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

        protected void OnTriggerEnter(Collider other)
        {
            TriggerGameObject(other);
        }

        protected void OnTriggerExit(Collider other)
        {
            
        }

        protected virtual void TriggerGameObject(Collider other)
        {
        }

        public virtual void Reset()
        {
            this.gameObject.transform.position = Vector3.zero;
        }
    }
}
