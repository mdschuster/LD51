using UnityEngine;
using System.Collections.Generic;

namespace WFG.Utilities
{
    /// <summary>
    /// Basic object pooling class.
    /// Only pools one object and sets its parent to this object.
    /// </summary>
    public class ObjectPooler : MonoBehaviour
    {
        public GameObject objectToPool;
        public int amountToPool;
        public bool canExpand;

        private List<GameObject> _pool;

        private void Start()
        {
            _pool = new List<GameObject>();
            for(int i = 0; i < amountToPool; i++)
            {
                GameObject go=Instantiate(objectToPool,this.transform);
                go.SetActive(false);
                _pool.Add(go);
            }
        }

        public GameObject GetPooledObject()
        {
            //just an available pooled object
            foreach(GameObject go in _pool)
            {
                if (go.activeInHierarchy == false)
                {
                    return go;
                }
            }

            //no pooled objects, should we create more?
            if (canExpand)
            {
                GameObject go = Instantiate(objectToPool,this.transform);
                go.SetActive(false);
                _pool.Add(go);
                return go;
            }


            return null;
        }
    }
}