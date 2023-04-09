using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{

    public enum EnemyType
    {
        NONE = 0,
        /// <summary>
        /// ��ͨ����
        /// </summary>
        NormalEnemy,
    }
    public enum PointType
    {
        None = 0,
        /// <summary>
        /// ��ʼ��
        /// </summary>
        Start,
        /// <summary>
        /// ��ͨ��ͼ��
        /// </summary>
        Normal, 
        /// <summary>
        /// ������
        /// </summary>
        End,
        /// <summary>
        /// �ϰ���
        /// </summary>
        Obstacle,
        /// <summary>
        /// �սڵ�
        /// </summary>
        Empty,
    }

    public enum BulletType
    {
        None = 0,
        /// <summary>
        /// ��ͨ�ӵ�
        /// </summary>
        NormalBullet,
    }

    /// <summary>
    /// �ӵ�״̬
    /// </summary>
    public enum BulletState
    {
        None = 0,
        /// <summary>
        /// ����״̬
        /// </summary>
        Fire,
        /// <summary>
        /// ����״̬
        /// </summary>
        Idle,
    }

    public enum EnemyState
    {
        None = 0,
        /// <summary>
        /// Ĭ��״̬
        /// </summary>
        Idle,
        /// <summary>
        /// ����״̬
        /// </summary>
        Move,
    }

    public enum TowerBuildState
    {
        None = 0,
        /// <summary>
        /// �ѽ���
        /// </summary>
        Build,
        /// <summary>
        /// δ����
        /// </summary>
        Unbuild,
    }
}