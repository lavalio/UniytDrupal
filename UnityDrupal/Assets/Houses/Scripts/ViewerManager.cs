
using System.Collections.Generic;
using UnityEngine;

namespace House
{
    public class ViewerManager : MonoBehaviour
    {
        private static ViewerManager instance = null;
        public static ViewerManager Instance
        {
            get
            {
                if (instance == null)
                    instance = GameObject.FindObjectOfType(typeof(ViewerManager)) as ViewerManager;
                return instance;
            }
        }

        //public GameObject view;
        public PoinsManager pm;

        private void Start()
        {
            ResetView();
        }

        void ResetView()
        {
            //pm.quad.SetActive(false);
            Debug.Log("Reset");
        }

        public void LoadView()
        {
            //pm.quad.SetActive(true);
            Debug.Log("Load");
        }
    }
}