/**
Create By LaiZhangYin, Eagle
if you have any question, please call wechat:782966734
**/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FTProject
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class CameraController : MonoBehaviour
    {
        public float move_speed;
        public float view_value;
        private Touch _OldTouch1;  //上次触摸点1(手指1)  
        private Touch _OldTouch2;  //上次触摸点2(手指2)  

        // 记录手指触屏的位置  
        Vector2 _M_Screenpos = new Vector2();

        //旋转还是移动布尔
        bool _bMoveOrRotation;

        private Vector3 orginPosition;

        //相机初始位置
        Vector3 _OldPosition;

        private bool isBuildingTower = false;

        void Start()
        {
            //记录开始摄像机的Position
            _OldPosition = Camera.main.transform.position;
            this.orginPosition = transform.position;
            EventDispatcher.AddEventListener(EventName.BuildingTower, BuildingTower);
            EventDispatcher.AddEventListener(EventName.BuildTowerSuccess, BuildTowerSuccess);
        }
        void Update()
        {

#if !UNITY_EDITOR
            //没有触摸  
            if (Input.touchCount <= 0)
            {
                return;
            }
            //单点触摸   
            if (1 == Input.touchCount && !isBuildingTower)
            {
                //如果为 True 单指操作为旋转  如果为 Fals 单指操作为移动
                if (_bMoveOrRotation)
                {
                    //水平上下旋转
                    Touch _Touch = Input.GetTouch(0);
                    Vector2 _DeltaPos = _Touch.deltaPosition;
                    transform.Rotate(Vector3.down * _DeltaPos.x, Space.World);
                    transform.Rotate(Vector3.right * _DeltaPos.y, Space.World);
                }
                else
                {
                    //移动

                    if (Input.touches[0].phase == TouchPhase.Began)
                    {
                        // 记录手指触屏的位置  
                        _M_Screenpos = Input.touches[0].position;

                    }
                    // 手指移动  
                    else if (Input.touches[0].phase == TouchPhase.Moved)
                    {

                        // 移动摄像机  
                        Camera.main.transform.Translate(new Vector3(-Input.touches[0].deltaPosition.x * Time.deltaTime * 0.1f, -Input.touches[0].deltaPosition.y * Time.deltaTime * 0.1f, 0));
                    }
                }
            }

            //多点触摸, 放大缩小  
            Touch _NewTouch1 = Input.GetTouch(0);
            Touch _NewTouch2 = Input.GetTouch(1);

            //第2点刚开始接触屏幕, 只记录，不做处理  
            if (_NewTouch2.phase == TouchPhase.Began)
            {
                _OldTouch2 = _NewTouch2;
                _OldTouch1 = _NewTouch1;
                return;
            }

            //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型  
            float _OldDistance = Vector2.Distance(_OldTouch1.position, _OldTouch2.position);
            float _NewDistance = Vector2.Distance(_NewTouch1.position, _NewTouch2.position);

            //两个距离之差，为正表示放大手势， 为负表示缩小手势  
            float _Offset = _NewDistance - _OldDistance;

            //放大因子， 一个像素按 0.01倍来算(100可调整)  
            float _ScaleFactor = _Offset / 100f;
            Vector3 _LocalScale = transform.localScale;
            Vector3 _Scale = new Vector3(_LocalScale.x + _ScaleFactor,
                                   _LocalScale.y + _ScaleFactor,
                                   _LocalScale.z + _ScaleFactor);

            //最小缩放到 0.3 倍  
            if (_Scale.x > 0.3f && _Scale.y > 0.3f && _Scale.z > 0.3f)
            {
                transform.localScale = _Scale;
            }

            //记住最新的触摸点，下次使用  
            _OldTouch1 = _NewTouch1;
            _OldTouch2 = _NewTouch2;
#else
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                this.gameObject.transform.Translate(new Vector3(0, 0, Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * view_value));
            }
            //移动视角
            if (Input.GetMouseButton(2))
            {
                transform.Translate(Vector3.left * Input.GetAxis("Mouse X") * move_speed);
                transform.Translate(Vector3.up * Input.GetAxis("Mouse Y") * -move_speed);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                this.transform.position = orginPosition;
            }

            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool isHit = Physics.Raycast(ray, out RaycastHit hit, 100, 3, QueryTriggerInteraction.Ignore);
                if (isHit && hit.collider.gameObject.name.Contains("Tower"))
                {
                    GameObject.Destroy(hit.collider.gameObject);
                }
            }
#endif
        }

        private void BuildingTower()
        {
            isBuildingTower = true;
        }

        private void BuildTowerSuccess()
        {
            isBuildingTower = false;
        }

        //通过按钮让物体回到最初的位置
        public void BackPosition()
        {
            //位置回归原点
            Camera.main.transform.position = _OldPosition;
            //旋转归零
            Camera.main.transform.eulerAngles = Vector3.zero;
        }

        //设置单指操作方式 旋转还是移动
        public void RotationOrMove()
        {
            _bMoveOrRotation = !_bMoveOrRotation;
        }
    }
}
