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
        private Touch _OldTouch1;  //�ϴδ�����1(��ָ1)  
        private Touch _OldTouch2;  //�ϴδ�����2(��ָ2)  

        // ��¼��ָ������λ��  
        Vector2 _M_Screenpos = new Vector2();

        //��ת�����ƶ�����
        bool _bMoveOrRotation;

        private Vector3 orginPosition;

        //�����ʼλ��
        Vector3 _OldPosition;

        private bool isBuildingTower = false;

        void Start()
        {
            //��¼��ʼ�������Position
            _OldPosition = Camera.main.transform.position;
            this.orginPosition = transform.position;
            EventDispatcher.AddEventListener(EventName.BuildingTower, BuildingTower);
            EventDispatcher.AddEventListener(EventName.BuildTowerSuccess, BuildTowerSuccess);
        }
        void Update()
        {

#if !UNITY_EDITOR
            //û�д���  
            if (Input.touchCount <= 0)
            {
                return;
            }
            //���㴥��   
            if (1 == Input.touchCount && !isBuildingTower)
            {
                //���Ϊ True ��ָ����Ϊ��ת  ���Ϊ Fals ��ָ����Ϊ�ƶ�
                if (_bMoveOrRotation)
                {
                    //ˮƽ������ת
                    Touch _Touch = Input.GetTouch(0);
                    Vector2 _DeltaPos = _Touch.deltaPosition;
                    transform.Rotate(Vector3.down * _DeltaPos.x, Space.World);
                    transform.Rotate(Vector3.right * _DeltaPos.y, Space.World);
                }
                else
                {
                    //�ƶ�

                    if (Input.touches[0].phase == TouchPhase.Began)
                    {
                        // ��¼��ָ������λ��  
                        _M_Screenpos = Input.touches[0].position;

                    }
                    // ��ָ�ƶ�  
                    else if (Input.touches[0].phase == TouchPhase.Moved)
                    {

                        // �ƶ������  
                        Camera.main.transform.Translate(new Vector3(-Input.touches[0].deltaPosition.x * Time.deltaTime * 0.1f, -Input.touches[0].deltaPosition.y * Time.deltaTime * 0.1f, 0));
                    }
                }
            }

            //��㴥��, �Ŵ���С  
            Touch _NewTouch1 = Input.GetTouch(0);
            Touch _NewTouch2 = Input.GetTouch(1);

            //��2��տ�ʼ�Ӵ���Ļ, ֻ��¼����������  
            if (_NewTouch2.phase == TouchPhase.Began)
            {
                _OldTouch2 = _NewTouch2;
                _OldTouch1 = _NewTouch1;
                return;
            }

            //�����ϵ����������µ��������룬���Ҫ�Ŵ�ģ�ͣ���СҪ����ģ��  
            float _OldDistance = Vector2.Distance(_OldTouch1.position, _OldTouch2.position);
            float _NewDistance = Vector2.Distance(_NewTouch1.position, _NewTouch2.position);

            //��������֮�Ϊ����ʾ�Ŵ����ƣ� Ϊ����ʾ��С����  
            float _Offset = _NewDistance - _OldDistance;

            //�Ŵ����ӣ� һ�����ذ� 0.01������(100�ɵ���)  
            float _ScaleFactor = _Offset / 100f;
            Vector3 _LocalScale = transform.localScale;
            Vector3 _Scale = new Vector3(_LocalScale.x + _ScaleFactor,
                                   _LocalScale.y + _ScaleFactor,
                                   _LocalScale.z + _ScaleFactor);

            //��С���ŵ� 0.3 ��  
            if (_Scale.x > 0.3f && _Scale.y > 0.3f && _Scale.z > 0.3f)
            {
                transform.localScale = _Scale;
            }

            //��ס���µĴ����㣬�´�ʹ��  
            _OldTouch1 = _NewTouch1;
            _OldTouch2 = _NewTouch2;
#else
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                this.gameObject.transform.Translate(new Vector3(0, 0, Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * view_value));
            }
            //�ƶ��ӽ�
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

        //ͨ����ť������ص������λ��
        public void BackPosition()
        {
            //λ�ûع�ԭ��
            Camera.main.transform.position = _OldPosition;
            //��ת����
            Camera.main.transform.eulerAngles = Vector3.zero;
        }

        //���õ�ָ������ʽ ��ת�����ƶ�
        public void RotationOrMove()
        {
            _bMoveOrRotation = !_bMoveOrRotation;
        }
    }
}
