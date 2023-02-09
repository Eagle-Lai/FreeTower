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
        protected override void OnStart()
        {
            base.OnStart();
            _bulletType = BulletType.NormalBullet;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
        }
        protected override void TriggerGameObject(Collider other)
        {
            base.TriggerGameObject(other);
        }
    }
}
