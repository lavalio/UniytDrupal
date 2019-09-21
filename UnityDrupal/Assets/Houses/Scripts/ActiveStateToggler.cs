using UnityEngine;
using System.Collections;

namespace House
{
    public class ActiveStateToggler : MonoBehaviour
    {
        /*
        private static ActiveStateToggler instance = null;
        public static ActiveStateToggler Instance
        {
            get
            {
                if (instance == null)
                    instance = GameObject.FindObjectOfType(typeof(ActiveStateToggler)) as ActiveStateToggler;
                return instance;
            }
        }
        */
        public void SetNoActivate()
        {
            gameObject.SetActive(false);
        }

        public void ToggleActive()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
