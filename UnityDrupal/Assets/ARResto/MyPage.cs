using System.Linq;
using UnityEngine;

namespace FancyScrollView.MonResto
{
    public class MyPage : MonoBehaviour
    {
        [SerializeField] ScrollView scrollView = default;

        void Start()
        {
            var items = Enumerable.Range(0, 20)
                .Select(i => new ItemData($"Cell {i}"))
                .ToArray();

            scrollView.UpdateData(items);
            scrollView.SelectCell(0);
        }
    }
}
