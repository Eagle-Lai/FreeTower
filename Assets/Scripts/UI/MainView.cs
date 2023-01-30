/**
Create By LaiZhangYin, Eagle
if you have any question, please call wechat:782966734
**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace FTProject
{
    public class MainView : BaseView
    {
        NormalTower tower;

        private Button normalTowerBtn;

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
            normalTowerBtn = _gameObject.transform.Find("Button").GetComponent<Button>();
            normalTowerBtn.onClick.AddListener(OnClickBtn);
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void OnClickBtn()
        {
            TowerManager.Instance.GetTower<NormalTower>(TowerType.Normal);
        }
        private void OnClickBtnUp(GameObject go, PointerEventData eventData)
        {
           
        }
    }
}