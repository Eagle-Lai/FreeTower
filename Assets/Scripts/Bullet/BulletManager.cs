/**
Create By LaiZhangYin, Eagle
if you have any question, please call wechat:782966734
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FTProject
{
    public class BulletManager : BaseManager<BulletManager>
    {
        private Dictionary<BulletType, List<BaseBullet>> bulletDictionary = new Dictionary<BulletType, List<BaseBullet>>();

        private Transform _BulletParent;

        public Transform BulletParent
        {
            get { return _BulletParent; }
        }

        

        public override void OnInit()
        {
            base.OnInit();
            //_BulletParent = GameObject.Find("BulletParent").transform;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public T GetBullet<T>(BulletType type) where T : BaseBullet
        {
            if(bulletDictionary.TryGetValue(type, out List<BaseBullet> list))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    BaseBullet bullet = list[i];
                    if (bullet.BulltState ==  BulletState.Idle)
                    {
                        bullet.BulltState =  BulletState.Fire;
                       // Debug.Log("333 +");
                        return bullet as T;
                    }
                }
            }
            if(_BulletParent == null)
            {
                _BulletParent = GameObject.Find("BulletParent").transform;
            }
            GameObject go = ResourcesManager.Instance.LoadAndInitGameObject("NormalBullet", _BulletParent, null, Vector3.zero, Vector3.one * GlobalConst.BulletScale);
            T t = go.AddComponent<T>();
            if(bulletDictionary.TryGetValue(type, out List<BaseBullet> blist) == false)
            {
                bulletDictionary.Add(type, new List<BaseBullet>());
            }
            bulletDictionary[type].Add(t);
            t.BulltState =  BulletState.Fire;
           // Debug.Log("111 +");
            return t;
        }

        public T AttackEnemy<T>(BulletType type) where T : BaseBullet
        {
            return GetBullet<T>(type);
        }

        public void RecycleBullet(BulletType type, BaseBullet baseBullet)
        {
            //if(bulletDictionary.TryGetValue(type, out List<BaseBullet> list))
            //{
            //    switch (type)
            //    {
            //        case BulletType.None:
            //            break;
            //        case BulletType.NormalBullet:
            //            baseBullet = baseBullet as NormalBullet;
            //            break;
            //        default:
            //            break;
            //    }
            ////}
            //baseBullet.transform.SetObjParent(BulletParent, Vector3.zero, Vector3.one * GlobalConst.BulletScale);
            //baseBullet.BulltState =  BulletState.Idle;
            baseBullet.Reset();
        }
    }
}
