using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class Launch : MonoBehaviour
    {

        private void Awake()
        {
            TimerManager.Instance.Update(Time.fixedDeltaTime);
            AStarPath.Instance.Start();
        }

        private void Start()
        {

            ResourcesManager.Instance.LoadAndInitGameObject("Capsule", this.transform, (go) =>
            {
                BaseEnemy baseEnemy = new NormalEnemy();
                baseEnemy.gameObject = go;
                baseEnemy.OnStart();
            });
            
        }

        private void Update()
        {
            TimerManager.Instance.Update(Time.fixedDeltaTime);
        }
    }
}