using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperScrollView;

namespace FTProject
{
    public class SelectView : BaseView
    {
        LoopListView2 _LoopListView2;
        Button _BackBtn;
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
            _LoopListView2 = _gameObject.transform.Find("ScrollView").GetComponent<LoopListView2>();
            _LoopListView2.InitListView(100, OnItemUpdate);
            _BackBtn = _gameObject.transform.Find("Back").GetComponent<Button>();
            _BackBtn.onClick.AddListener(OnClickBack);
        }
        public override void OnHide()
        {
            base.OnHide();
        }

        private LoopListViewItem2 OnItemUpdate(LoopListView2 view, int index)
        {
            LoopListViewItem2 item = null;
            return item;
        }

        private void OnClickBack()
        {
            CloseSelf();
            UIManager.Instance.OpenView<StartView>("StartView");
        }

        public override void OnDestroy()
        {
        }

    }
}
