using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FTProject
{
    /// <summary>
    /// Main场景的管理类
    /// </summary>
    public class GameSceneLauncher : MonoBehaviour
    {
        public BaseGameScene _currentSceneInfo;

        private GameObject IdleObject;

        public GameObject _currentMapObj;

        private void Awake()
        {
            _currentSceneInfo = GameSceneManager.Instance.GetCurrentSceneInfo();
            EventDispatcher.AddEventListener(EventName.MapInitFinish, MapInitFinish);
            _currentMapObj = ResourcesManager.Instance.LoadAndInitGameObject(_currentSceneInfo._SceneInfo.MapName);
            AStarManager.Instance.OnInitMapData();
            _currentMapObj.transform.position = _currentSceneInfo._SceneInfo.MapPosition;
            Camera.main.transform.position = _currentSceneInfo._SceneInfo.CameraPosition;
            Vector3 temp = _currentSceneInfo._SceneInfo.CameraRotration;
            Camera.main.transform.localEulerAngles = temp;
        }
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void MapInitFinish()
        {
            
            
            //AStarManager.Instance.SetPointParentPosition(_currentSceneInfo._SceneInfo.MapPosition);
            //IdleObject = new GameObject("IdleObject");
            //IdleObject.transform.position = _currentSceneInfo._SceneInfo.EenemyPosition;
        }
    }
}
