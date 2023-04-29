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
using UnityEngine.SceneManagement;

namespace FTProject
{
    public class Launcher : MonoBehaviour
    {

        private List<IManagerInterface> managerList = new List<IManagerInterface>()
        {
          new EnemyManager(),
          new BulletManager(),
          new RoundCountManager(),
          new GameSceneManager(),
          new JsonDataManager(),
          new AStarManager(),
        };


        public DataTables Tables;

        public static Launcher Instance;

        private void Awake()
        {
            Debug.Log("========");
            DontDestroyOnLoad(gameObject);
            Instance = this;
            InitGameInfo();
            for (int i = 0; i < managerList.Count; i++)
            {
                managerList[i].OnInit();
            }
                                                                        
            TimerManager.Instance.Update(Time.fixedDeltaTime);
            InitDataTables();
            //Tables = new Tables(Reader);
            //Debug.Log(Tables.TBEnemyData.Get(1).Name);
        }

        public void InitDataTables()
        {
            GameObject go = new GameObject("DataTables");
            Tables = go.AddComponent<DataTables>();
        }

        private void InitGameInfo()
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            
        }
        private void Start()
        {
            //UIManager.Instance.OpenView<MainView>("MainView");
            Debug.Log("========");
            //SceneManager.LoadScene("Start");
            UIManager.Instance.OpenView<StartView>("StartView");
        }

        private void Update()                                                         
        {
            TimerManager.Instance.Update(Time.deltaTime);
            EventDispatcher.TriggerEvent(EventName.UpdateEvent);
        }

        private void OnDestroy()
        {
            for (int i = 0; i < managerList.Count; i++)
            {
                managerList[i].OnDestroy();
            }
        }

    }
}