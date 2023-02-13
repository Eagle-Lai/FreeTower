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

        private Button generateEnemyBtn;

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
            generateEnemyBtn = _gameObject.transform.Find("GenerateEnemyBtn").GetComponent<Button>();
            normalTowerBtn.onClick.AddListener(OnClickBtn);
            generateEnemyBtn.onClick.AddListener(OnClickGenerateEnemyBtn);
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void OnClickBtn()
        {
            TowerManager.Instance.GetTower<NormalTower>(TowerType.Normal);
        }

        private void OnClickGenerateEnemyBtn()
        {
            EnemyManager.Instance.GenerateEnemy();
        }
        private void OnClickBtnUp(GameObject go, PointerEventData eventData)
        {
           
        }
    }
}