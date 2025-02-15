using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class NormalTower : BaseTower
    {
        protected override void OnAwake()
        {
            base.OnAwake();
            
        }

        protected override void OnStart()
        {
            base.OnStart();
            _bulletPoint = transform.Find("Head/BulletPoint").transform;
        }

        protected override void TowerAttack()
        {
            NormalBullet bullet = BulletManager.Instance.AttackEnemy<NormalBullet>(BulletType.NormalBullet);
            if (_bulletPoint == null)
            {
                _bulletPoint = transform.Find("Head/BulletPoint").transform;
            }
            bullet.BulletAttack();
            bullet.transform.SetObjParent(_bulletPoint.transform, Vector3.zero, Vector3.one * GlobalConst.BulletScale);
        }
        protected override void OnUpdate()
        {
            base.OnUpdate();
        }
    }
}