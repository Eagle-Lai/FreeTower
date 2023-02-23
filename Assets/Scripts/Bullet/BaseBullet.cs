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

        protected bool _isReset;

        protected float _resetTime;

        protected GameObject currentTarget;

        protected BulletType _bulletType;

        public BulletState BulltState;

        protected int _bulletResetTimerId;

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
            this._resetTime = GlobalConst.BulletResetTimeInterval;
            this.BulltState = BulletState.None;
        }

        protected virtual void OnStart()
        {
            
        }

        protected void SetRecycleTimer()
        {
            _bulletResetTimerId = TimerManager.Instance.AddTimer(3, 1, () =>
            {
                Reset();
            }, false);
        }

        protected virtual void OnUpdate()
        {
            if(BulltState ==  BulletState.Fire)
            {
                transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            }
        }

        protected virtual void Clear()
        {
            if (_bulletResetTimerId > 0)
            {
                TimerManager.Instance.RemoveTimerById(_bulletResetTimerId);
            }
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
            if (other.gameObject.name.Contains("Enemy"))
            {
                BaseEnemy BaseEnemy = other.gameObject.GetComponent<BaseEnemy>();
                if(BaseEnemy != null)
                {
                    BaseEnemy.Hit();
                    BaseEnemy.Hurt(1);
                    Reset();
                }
            }
        }



        public virtual void Reset()
        {
            BulltState =  BulletState.Idle;
            this.transform.SetObjParent(BulletManager.Instance.BulletParent, Vector3.zero, Vector3.one * GlobalConst.BulletScale);
            this._bulletResetTimerId = 0;
            gameObject.HideObject();
        }

        public void SetParent(Transform transform)
        {
            this.transform.SetObjParent(BulletManager.Instance.BulletParent, Vector3.zero, Vector3.one * GlobalConst.BulletScale);
        }

        public void ResetBulletScale()
        {
            this.transform.localScale = Vector3.one * 0.1f;
        }

        public void BulletAttack()
        {
            BulltState = BulletState.Fire;
            SetRecycleTimer();
            gameObject.ShowObject();
        }
    }
}
