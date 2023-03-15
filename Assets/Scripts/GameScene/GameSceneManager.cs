using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cfg;


namespace FTProject
{
    public class GameSceneManager : BaseManager<GameSceneManager>
    {
        public RoundCountManager _roundCountManager;

        private List<BaseGameScene> _AllGameScene;
            
        public List<BaseGameScene> AllScene
        {
            get
            {
                if(_AllGameScene == null)
                {
                    _AllGameScene = new List<BaseGameScene>();
                }
                _AllGameScene.Clear();
                UpdateAllSceneInfo();
                return _AllGameScene;
            }
        }

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

        public void UpdateAllSceneInfo()
        {
            List<SceneInfo> list = Launcher.Instance.Tables.TBSceneInfo.DataList;
            for (int i = 0; i < list.Count; i++)
            {
                SceneInfo info = list[i];
                BaseGameScene scene = new BaseGameScene(info);
                _AllGameScene.Add(scene);
            }
        }
    }
}
