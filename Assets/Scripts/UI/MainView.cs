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

        private TextMeshProUGUI _GoldCoinTxt;

        private int _hp;

        private int _count;

        private int _TotalCount;

        private int _GoldCoin;

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
            //generateEnemyBtn.onClick.RemoveAllListeners
            _hpTxt = _gameObject.transform.Find("Hp").GetComponent<TextMeshProUGUI>();
            _countTxt = _gameObject.transform.Find("RoundCount").GetComponent<TextMeshProUGUI>();
            _GoldCoinTxt = _gameObject.transform.Find("GoldCoin").GetComponent<TextMeshProUGUI>();

            UIEventListener.Get(normalTowerBtn.gameObject).onPointerDown = DownEvent;
            UIEventListener.Get(normalTowerBtn.gameObject).onPointerUp = UpEvent;

            _hp = GlobalConst.PlayerHp;
            _count = GlobalConst.RoundCount;
            _GoldCoin = GlobalConst.GoldCoin;

            _countTxt.text = _count.ToString();
            _hpTxt.text = _hp.ToString();
            _GoldCoinTxt.text = _GoldCoin.ToString();

            _TotalCount = GlobalConst.RoundCount;
            
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            EventDispatcher.RemoveEventListener<int>(EventName.PlayerRoundCountChange, RoundCountChange);
            EventDispatcher.RemoveEventListener<int>(EventName.PlayerHpChangeEvent, PlayerHpChange);
        }
        
        private void DownEvent(GameObject go, PointerEventData eventData)
        {
            EventDispatcher.TriggerEvent(EventName.OperateDown);
            TowerManager.Instance.GetTower<NormalTower>(TowerType.Normal);
        }

        private void UpEvent(GameObject go, PointerEventData eventData)
        {
            EventDispatcher.TriggerEvent(EventName.OperateUp);
        }

        private void OnClickBtn()
        {
            
        }

        private void OnClickGenerateEnemyBtn()
        {
            //RoundCountManager.Instance.GenerateEnemyByInfoItem();
            //RoundCountManager.Instance.GenerateEnemyByRoundInfo();
            EnemyManager.Instance.GenerateEnemy();
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