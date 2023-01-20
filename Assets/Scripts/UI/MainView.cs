using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace FTProject
{
    public class MainView : BaseView
    {

        private Button button;                                                

        public override void OnInit()
        {
            base.OnInit();
        }

        public override void OnShow()
        {
            base.OnShow();
        }

        public override void InitComponent()
        {
            base.InitComponent();
            button = _gameObject.transform.Find("Button").GetComponent<Button>();
            UIEventListener.Get(button.gameObject).onPointerDown = OnClickBtnDown;
            UIEventListener.Get(button.gameObject).onPointerUp = OnClickBtnUp;
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void OnClickBtnDown(GameObject go, PointerEventData eventData)
        {
            Debug.Log("=================");
        }

        private void OnClickBtnUp(GameObject go, PointerEventData eventData)
        {
            Debug.Log("=====###########============");
        }
    }
}