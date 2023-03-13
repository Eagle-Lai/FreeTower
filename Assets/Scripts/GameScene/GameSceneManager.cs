using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg;


namespace FTProject
{
    public class GameSceneManager : BaseManager<GameSceneManager>
    {
        public RoundCountManager _roundCountManager;

        public SceneInfo _SceneInfo;

        public int CurrentIndex = 0;

        public override void OnInit()
        {
            base.OnInit();
            //_roundCountManager = RoundCountManager.Instance;
            //_SceneInfo = Launcher.Instance.Tables.TBSceneInfo.Get(CurrentIndex);
            //_roundCountManager.SetSceneInfo(_SceneInfo);
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public void UpdateSceneInfo()
        {
            CurrentIndex++;
            _SceneInfo = Launcher.Instance.Tables.TBSceneInfo.Get(CurrentIndex);
            _roundCountManager.SetSceneInfo(_SceneInfo);
        }
    }
}
