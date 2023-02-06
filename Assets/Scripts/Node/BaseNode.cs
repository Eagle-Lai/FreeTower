using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FTProject
{
    public class BaseNode : MonoBehaviour
    {
        [SerializeField]
        //public Node node;
        public NodeType NodeType;

       

        protected MeshRenderer meshRenderer;

        /// <summary>
        /// Åö×²ÎïÌå
        /// </summary>
        public GameObject currentTriggerObj;

        protected virtual void Awake()
        {
            
        }
        protected virtual void Start()
        {
            meshRenderer = transform.GetComponent<MeshRenderer>();
        }

        protected virtual void Update()
        {
            if (currentTriggerObj != null && meshRenderer.material.color != Color.green && NodeType == NodeType.Normal)
            {
                ////float distance = Vector3.Distance(transform.position, currentTriggerObj.transform.position);
                //float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(currentTriggerObj.transform.position.x, currentTriggerObj.transform.position.z));
                float distance = FTProjectUtils.GetPointDistance(this.gameObject, currentTriggerObj);
                //node.MarkAsObstacle();
                //List<Node> nodes = GridManager.Instance.GetPath();
                //if (nodes == null)
                //{
                //    node.MarkAsObstacle(false);
                //    ChangeColor(Color.red);
                //    return;
                //}
                if (distance >= 0.6f)
                {
                    ChangeColor(Color.red);
                }
                if (Mathf.Max(0.1f, distance) < 0.6f )
                {
                    ChangeColor(Color.green);
                }
            }
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name.Contains("TowerBase"))
            {
                currentTriggerObj = other.gameObject;
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            currentTriggerObj = null;
            if (NodeType == NodeType.Normal)
            {
                ChangeColor(Color.black);
            }
        }
        public virtual void ChangeColor(Color color)
        {
            if(meshRenderer != null)
            {
                meshRenderer.material.color = color;
            }
        }

        public Color GetNodeColor()
        {
            return meshRenderer.material.color;
        }
    }
}