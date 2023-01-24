using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class NormalTower : BaseTower
    {
                
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();

            
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
        }

        protected override void OperateUpEvent(BaseTower tower)
        {
            base.OperateUpEvent(tower);
            NormalTower normalTower = tower as NormalTower;
            if(normalTower == this)
            {
                isBuild = true;                
            }
        }
    }
}