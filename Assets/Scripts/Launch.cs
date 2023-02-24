using cfg;
using cfg.item;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace FTProject
{
    public class Launch : MonoBehaviour
    {

        private List<IManagerInterface> managerList = new List<IManagerInterface>()
        {
          new EnemyManager(),
          new BulletManager(),
          new RoundCountManager(),
          new GameSceneManager(),
        };

        public static Launch Instance;
        public BaseEnemy baseEnemy;
        private void Awake()
        {
            Instance = this;
            InitGameInfo();
            for (int i = 0; i < managerList.Count; i++)
            {
                managerList[i].OnInit();
            }
            DontDestroyOnLoad(gameObject);
            TimerManager.Instance.Update(Time.fixedDeltaTime);
            //AStarPath.Instance.Start();
            //Tables t = new Tables(Reader);
            //Item item = t.TbItem.Get(100010);
            //Debug.Log(item.Desc);
            //Tables table = new Tables(Reader);
            //Equip equip = table.TbEquip.Get(1);
            //Debug.Log(equip.Color);
            //FTProjectUtils.ReadData("tbenemydata", ())
        }



        private void InitGameInfo()
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
        }

        private JSONNode Reader(string fileName)
        {
            return JSON.Parse(File.ReadAllText(Application.dataPath + "/../GenerateDatas/json/" + fileName + ".json"));
        }

        private void Start()
        {
            UIManager.Instance.OpenView<MainView>("MainView");
        }



        private void OnDrawGizmos()
        {

        }
        private void Update()
        {
            TimerManager.Instance.Update(Time.deltaTime);
            EventDispatcher.TriggerEvent(EventName.UpdateEvent);
        }

        private void FixedUpdate()
        {

        }

    }
}