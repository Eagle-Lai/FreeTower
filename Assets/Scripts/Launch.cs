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

        public static Launch Instance;
        public BaseEnemy baseEnemy;
        private void Awake()
        {
            Instance = this;
            TimerManager.Instance.Update(Time.fixedDeltaTime);
            //AStarPath.Instance.Start();
            //Tables t = new Tables(Reader);
            //Item item = t.TbItem.Get(100010);
            //Debug.Log(item.Desc);
            //Tables table = new Tables(Reader);
            //Equip equip = table.TbEquip.Get(1);
            //Debug.Log(equip.Color);
            //UIManager.Instance.OpenView<MainView>("MainView");
        }

        private JSONNode Reader(string fileName)
        {
            return JSON.Parse(File.ReadAllText(Application.dataPath + "/../GenerateDatas/json/" + fileName + ".json"));
        }

        private void Start()
        {
            ResourcesManager.Instance.LoadAndInitGameObject("NormalEnemy", this.transform, (go) =>
            {
                baseEnemy = new NormalEnemy();
                baseEnemy.gameObject = go;
                baseEnemy.OnStart();
            });
            
        }

        

        private void OnDrawGizmos()
        {
           
        }
        private void Update()
        {
            TimerManager.Instance.Update(Time.fixedDeltaTime);
        }

    }
}