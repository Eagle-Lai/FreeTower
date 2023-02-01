/**
Create By LaiZhangYin, Eagle
if you have any question, please call wechat:782966734
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FTProject
{
    public class NormalBullet : BaseBullet
    {

        protected override void OnUpdate()
        {
            base.OnUpdate();
            transform.Translate(Vector3.forward * _speed);
        }
        protected override void TriggerGameObject(Collider other)
        {
            base.TriggerGameObject(other);
            if (other.gameObject.name.Contains("Enemy"))
            {
                BaseEnemy enemy = other.GetComponent<NormalEnemy>();
                enemy.Reset();
                Reset();
            }
        }
    }
}
