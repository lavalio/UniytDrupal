using UnityEngine;

namespace House
{
    public class ToggleInfo : MonoBehaviour
    {
        public ActiveStateToggler at;

        void Start()
        {
            at.SetNoActivate();
        }
        void OnMouseDown()
        {
            at.ToggleActive();
            //Debug.Log("Clicked");
        }

    }
}
