using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace FTProject
{
    public class StartView : BaseView
    {
        public Button StartBtn;                              
        public override void OnInit()
        {
            base.OnInit();
        }

        public override void OnShow()
        {
            base.OnShow();
        }

        public override void OnStart()
        {
            base.OnStart();
        }

        public override void InitComponent()
        {
            base.InitComponent();
            StartBtn = _gameObject.transform.Find("BtnStart").GetComponent<Button>();
            StartBtn.onClick.AddListener(OnClickBtnStart);
        }

        public void OnClickBtnStart()
        {
            CloseSelf();
            UIManager.Instance.OpenView<SelectView>("SelectView");
        }

        public override void OnHide()
        {
            base.OnHide();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            StartBtn.onClick.RemoveAllListeners();
        }
    }
}
