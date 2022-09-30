using System.Collections.Generic;
using UnityEngine;

namespace WFG.Utilities
{
    /// <summary>
    /// Base class to make any class a singleton.
    /// </summary>
    /// <typeparam name="T">Class to make a singleton</typeparam> 
    public abstract class Singleton<T> : MonoBehaviour
    {

        private static T _instance = default(T);

        public void Awake()
        {
            if (EqualityComparer<T>.Default.Equals(_instance, default(T)))
            {
                _instance = (T)(object)this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public static T Instance()
        {
            return _instance;
        }

    }
}
