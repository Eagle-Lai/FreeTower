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
        List<BaseGameScene> _data;
        public override void OnInit()
        {
            base.OnInit();
            EventDispatcher.AddEventListener(EventName.UpdateEvent, LateUpdate);
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
            _data = GameSceneManager.Instance.AllScene;
            _LoopListView2.InitListView(_data.Count, OnGetItemByIndex);
            _BackBtn = _gameObject.transform.Find("Back").GetComponent<Button>();
            _BackBtn.onClick.AddListener(OnClickBack);
        }
        public override void OnHide()
        {
            base.OnHide();
        }

        LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
        {
            if (index < 0 || index >= _data.Count)
            {
                return null;
            }
            //get a new item. Every item can use a different prefab, the parameter of the NewListViewItem is the prefab¡¯name. 
            //And all the prefabs should be listed in ItemPrefabList in LoopListView2 Inspector Setting
            LoopListViewItem2 item = listView.NewListViewItem("SelectViewItem");
            SelectViewItem itemScript = item.GetComponent<SelectViewItem>();
            if (item.IsInitHandlerCalled == false)
            {
                item.IsInitHandlerCalled = true;
                itemScript.Init();
            }
            if(index == 0 || index == _data.Count - 1)
            {
                itemScript.gameObject.HideObject();
            }
            else
            {
                itemScript.gameObject.ShowObject();
            }
            itemScript.SetItemData(_data[index]);
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
                scale = Mathf.Clamp(scale, 0.6f, 1);
                CanvasGroup canvas = itemScript.GetComponent<CanvasGroup>();
                if(canvas == null)
                {
                    canvas = itemScript.gameObject.AddComponent<CanvasGroup>();
                }
                canvas.alpha = scale;
                itemScript.transform.localScale = new Vector3(scale, scale, 1);
            }
        }

        public override void OnDestroy()
        {
            EventDispatcher.RemoveEventListener(EventName.UpdateEvent, LateUpdate);
        }

    }
}
