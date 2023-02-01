/**
Create By LaiZhangYin, Eagle
if you have any question, please call wechat:782966734
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FTProject
{
    public class BulletManager : BaseManager
    {
        private Dictionary<BulletType, List<BaseBullet>> bulletDictionary = new Dictionary<BulletType, List<BaseBullet>>();

        private Transform BulletParent;

        public override void OnInit()
        {
            base.OnInit();
            BulletParent = GameObject.Find("BulletParent").transform;
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
                    if (bullet.IsFree)
                    {
                        bullet.IsFree = false;
                        return bullet as T;
                    }
                }
            }
            GameObject go = ResourcesManager.Instance.LoadAndInitGameObject("NormalBullet");
            T t = go.AddComponent<T>();
            if(bulletDictionary.TryGetValue(type, out List<BaseBullet> blist) == false)
            {
                bulletDictionary.Add(type, new List<BaseBullet>());
            }
            bulletDictionary[type].Add(t);
            t.IsFree = false;
            return t;
        }

        public void RecycleBullet(BulletType type, BaseBullet baseBullet)
        {
            if(bulletDictionary.TryGetValue(type, out List<BaseBullet> list))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if(list[i] == baseBullet)
                    {
                        list[i].IsFree = true;
                    }
                }
            }
        }
    }
}
