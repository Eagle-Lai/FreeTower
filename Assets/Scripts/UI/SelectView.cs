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
            _LoopListView2.InitListView(100, OnGetItemByIndex);
            _BackBtn = _gameObject.transform.Find("Back").GetComponent<Button>();
            _BackBtn.onClick.AddListener(OnClickBack);
        }
        public override void OnHide()
        {
            base.OnHide();
        }

        LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
        {
            //if (index < 0 || index >= DataSourceMgr.Get.TotalItemCount)
            //{
            //    return null;
            //}

            //ItemData itemData = DataSourceMgr.Get.GetItemDataByIndex(index);
            //if (itemData == null)
            //{
            //    return null;
            //}
            //get a new item. Every item can use a different prefab, the parameter of the NewListViewItem is the prefab¡¯name. 
            //And all the prefabs should be listed in ItemPrefabList in LoopListView2 Inspector Setting
            LoopListViewItem2 item = listView.NewListViewItem("SelectViewItem");
            SelectViewItem itemScript = item.GetComponent<SelectViewItem>();
            if (item.IsInitHandlerCalled == false)
            {
                item.IsInitHandlerCalled = true;
                itemScript.Init();
            }
            itemScript.SetItemData();
            return item;
        }

        private void OnClickBack()
        {
            CloseSelf();
            UIManager.Instance.OpenView<StartView>("StartView");
        }
        void LateUpdate()
        {
            _LoopListView2.UpdateAllShownItemSnapData();
            int count = _LoopListView2.ShownItemCount;
            for (int i = 0; i < count; ++i)
            {
                LoopListViewItem2 item = _LoopListView2.GetShownItemByIndex(i);
                SelectViewItem itemScript = item.GetComponent<SelectViewItem>();
                float scale = 1 - Mathf.Abs(item.DistanceWithViewPortSnapCenter) / 700f;
                scale = Mathf.Clamp(scale, 0.4f, 1);
                //itemScript.mContentRootObj.GetComponent<CanvasGroup>().alpha = scale;
                //itemScript.mContentRootObj.transform.localScale = new Vector3(scale, scale, 1);
            }
        }

        public override void OnDestroy()
        {
        }

    }
}
