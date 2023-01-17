using cfg;
using cfg.item;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FTProject
{
    public class Launch : MonoBehaviour
    {

        private void Awake()
        {
            TimerManager.Instance.Update(Time.fixedDeltaTime);
            AStarPath.Instance.Start();
            //Tables t = new Tables(Reader);
            //Item item = t.TbItem.Get(100010);
            //Debug.Log(item.Desc);
            Tables table = new Tables(Reader);
            Equip equip = table.TbEquip.Get(1);
            Debug.Log(equip.Color);
        }

        private JSONNode Reader(string fileName)
        {
            return JSON.Parse(File.ReadAllText(Application.dataPath + "/../GenerateDatas/json/" + fileName + ".json"));
        }

        private void Start()
        {
            ResourcesManager.Instance.LoadAndInitGameObject("Capsule", this.transform, (go) =>
            {
                BaseEnemy baseEnemy = new NormalEnemy();
                baseEnemy.gameObject = go;
                baseEnemy.OnStart();
                Debug.Log("+==================================");
            });
            
        }

        private void Update()
        {
            TimerManager.Instance.Update(Time.fixedDeltaTime);
        }

        private void OnDrawGizmos()
        {
           
        }
    }
}