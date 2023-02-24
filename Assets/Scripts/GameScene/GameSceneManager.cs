using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FTProject
{
    public class GameSceneManager : BaseManager<GameSceneManager>
    {
        public RoundCountManager _roundCountManager;

        public int CurrentIndex = 0;

        public override void OnInit()
        {
            base.OnInit();
            _roundCountManager = RoundCountManager.Instance;
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
