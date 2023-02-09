/**
Create By LaiZhangYin, Eagle
if you have any question, please call wechat:782966734
**/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class GlobalConst
    {
        /// <summary>
        /// Ѱ�Ҽ��
        /// </summary>
        public const float  SearchRate = 0.05f; 
        /// <summary>
        /// �ӵ��ٶ�
        /// </summary>
        public const float BulletSpeed = 20f;                                                                
        /// <summary>
        /// ���������ٶ�
        /// </summary>
        public const float EnemySpeed = 10f;
        /// <summary>
        /// �������������
        /// </summary>
        public const float FireInterval = 0.2f;
        /// <summary>
        /// �ӵ����ü��
        /// </summary>
        public const float BulletResetTimeInterval = 3f;

        /// <summary>
        /// �ӵ�����
        /// </summary>
        public const float BulletScale = 0.2f;
        
        /// <summary>
        /// ��������
        /// </summary>
        public const float EnemyScale = 1.2f;

        /// <summary>
        /// �������
        /// </summary>
        public static Vector3 PointVector3 = new Vector3(0, 0.5f, 0);

        /// <summary>
        /// �������
        /// </summary>
        public static Vector3 PointScale = new Vector3(2.0f, 0.2f, 2.0f);

        /// <summary>
        /// δ�����ʱ��Y�������
        /// </summary>
        public const float UnbuildYPosition = 1.6f;

        /// <summary>
        /// �����Y�������
        /// </summary>
        public const float BuildYPosition = 1.75f;
    }
}