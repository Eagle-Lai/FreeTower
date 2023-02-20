using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace FTProject
{
    public class MainView : BaseView
    {
        NormalTower tower;

        private Button normalTowerBtn;

        private Button generateEnemyBtn;

        private TextMeshProUGUI _hpTxt;

        private TextMeshProUGUI _countTxt;

        private int _hp;

        private int _count;

        private int _TotalCount;

        public override void OnInit()
        {
            base.OnInit();
            EventDispatcher.AddEventListener<int>(EventName.PlayerHpChangeEvent, PlayerHpChange);
            EventDispatcher.AddEventListener<int>(EventName.PlayerRoundCountChange, RoundCountChange);
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
           // normalTowerBtn.onClick.AddListener(OnClickBtn);
            generateEnemyBtn.onClick.AddListener(OnClickGenerateEnemyBtn);
            _hpTxt = _gameObject.transform.Find("Hp").GetComponent<TextMeshProUGUI>();
            _countTxt = _gameObject.transform.Find("Count").GetComponent<TextMeshProUGUI>();
            UIEventListener.Get(normalTowerBtn.gameObject).onPointerDown = GenerateClick;
            _hp = GlobalConst.PlayerHp;
            _count = GlobalConst.RoundCount;
            _countTxt.text = _count.ToString();
            _hpTxt.text = _hp.ToString();
            _TotalCount = GlobalConst.RoundCount;
            
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
        }
        
        private void GenerateClick(GameObject go, PointerEventData eventData)
        {
            TowerManager.Instance.GetTower<NormalTower>(TowerType.Normal);
        }

        private void OnClickBtn()
        {
            
        }

        private void OnClickGenerateEnemyBtn()
        {
            RoundCountManager.Instance.GenerateEnemyByInfoItem();
        }

        private void PlayerHpChange(int value)
        {
            _hp += value;
            _hpTxt.text = _hp.ToString();
        }

        private void RoundCountChange(int value)
        {
            _count += value;
            _countTxt.text = string.Format("{0}/{1}", _count, _TotalCount);
        }
    }
}