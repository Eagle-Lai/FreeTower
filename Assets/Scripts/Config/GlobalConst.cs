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
        /// ���Ѫ��
        /// </summary>
        public const int PlayerHp = 10;
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
        public const float EnemySpeed = 15f;
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
        public const float UnbuildYPosition = 1.8f;

        /// <summary>
        /// �����Y�������
        /// </summary>
        public const float BuildYPosition = 1.75f;

        /// <summary>
        /// ������λ������
        /// </summary>
        public static Vector3 BuildYVector3 = new Vector3(0, 10.2f, 0);
        /// <summary>
        /// ������ģ������
        /// </summary>
        public static Vector3 BuildScale = new Vector3(0.6f, 6, 0.6f);

        /// <summary>
        /// ����Ѫ��
        /// </summary>
        public const int EnemyHp = 6;

        
        /// <summary>
        /// �غ���
        /// </summary>
        public const int RoundCount = 10;

        /// <summary>
        /// �������ɼ��
        /// </summary>
        public const float EnemyGenerateInterval = 1.0f;

        /// <summary>
        /// һ�����ɵĵ�������
        /// </summary>
        public const int RoundEnemyNumber = 10;

        /// <summary>
        /// ��ҵĽ��
        /// </summary>
        public const int GoldCoin = 100;
    }
}