using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace FTProject
{
    public class TowerInfoView : BaseView
    {
        private Button _UpgradBtn;
        private Button _SellBtn;
        private Button _BgBtn;
        public BaseTower _BaseTower;

        public override void OnInit()
        {
            base.OnInit();
        }

        public override void InitComponent()
        {
            base.InitComponent();
            _UpgradBtn = _transform.Find("UpgradBtn").GetComponent<Button>();
            _SellBtn = _transform.Find("SellBtn").GetComponent<Button>();
            _BgBtn = _transform.Find("BgButton").GetComponent<Button>();

            _UpgradBtn.onClick.AddListener(OnClickUpgradBtn);
            _SellBtn.onClick.AddListener(OnClickSellBtn);
            _BgBtn.onClick.AddListener(OnClickBgBtn);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            _UpgradBtn.onClick.RemoveAllListeners();
            _SellBtn.onClick.RemoveAllListeners();
            _BgBtn.onClick.RemoveAllListeners();
        }

        public override void OnShow()
        {
            base.OnShow();

        }

        public override void OnStart()
        {
            base.OnStart();
            ShowTowerInfo();
        }

        private void OnClickUpgradBtn()
        {

        }

        private void OnClickSellBtn()
        {
            ShellTower();
        }

        private void ShellTower()
        {
            if (_BaseTower != null)
            {
                _BaseTower.DestroyTower();
                GameObject.Destroy(_BaseTower.gameObject);
                EventDispatcher.TriggerEvent(EventName.DestroyTower, _BaseTower);
                EventDispatcher.TriggerEvent(EventName.RefreshPathEvent);
            }
            CloseSelf();
        }

        private void OnClickBgBtn()
        {
            CloseSelf();
        }

        public void SetTowerInfo(BaseTower tower)
        {
            _BaseTower = tower;
        }
        private void ShowTowerInfo()
        {
            Debug.Log("show tower info view");
        }
    }
}
