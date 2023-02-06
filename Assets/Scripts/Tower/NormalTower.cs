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
            _bulletPoint = transform.Find("Head/Cube/BulletPoint").transform;
        }

        protected override void TowerAttack()
        {
            NormalBullet bullet = BulletManager.Instance.AttackEnemy<NormalBullet>(BulletType.NormalBullet);
            if (_bulletPoint == null)
            {
                _bulletPoint = transform.Find("Head/Cube/BulletPoint").transform;
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