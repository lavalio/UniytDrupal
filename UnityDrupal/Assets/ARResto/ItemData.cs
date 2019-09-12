using UnityEngine;

namespace FancyScrollView.MonResto
{
    [System.Serializable]
    public class ItemData
    {
        public string Message { get; }
        public Sprite Image { get; set; }
        public int Count { get; }

        public ItemData(string message, int count)
        {
            Message = message;
            Count = count;
        }
    }
}
