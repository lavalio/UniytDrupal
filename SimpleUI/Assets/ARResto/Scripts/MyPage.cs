using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FancyScrollView.MonResto
{
    public class MyPage : MonoBehaviour
    {
        [SerializeField] ScrollView scrollView = default;

        public GetObj getObj;
        [SerializeField] public List<ItemData> items;
        //[SerializeField] public List<Sprite> sprites;
        bool dataSet = false;

        void Start()
        {
            items = getObj.itemDatas;
         }

        void Update()
        {
            if (items.Count == 0 | dataSet)
            {
                return;
            }
            else
            {
                setData();
            }
        }

        void setData()
        {
            /*var items = Enumerable.Range(0, sprites.Count)
                .Select(i => new ItemData((sprites.Count).ToString(), sprites[i]))
                .ToArray(); */

            Debug.Log(items.Count);

            scrollView.UpdateData(items);
            scrollView.SelectCell(0);
            dataSet = true;
        }

    }
}
