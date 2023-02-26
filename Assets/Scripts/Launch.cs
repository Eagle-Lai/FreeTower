using cfg;
using cfg.item;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

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

        public Tables Tables;

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
            Tables = new Tables(Reader);
            Debug.Log(Tables.TBEnemyData.Get(1).Name);
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
            string path = Application.streamingAssetsPath + "/json/" + fileName + ".json";
            Debug.Log(path);
            return JSON.Parse(File.ReadAllText(path));
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