using UnityEngine;
using UnityEngine.UI;

namespace FancyScrollView.MonResto
{
    public class Cell : FancyScrollViewCell<ItemData, Context>
    {
        [SerializeField] Animator animator = default;
        [SerializeField] Text index = default;
        [SerializeField] Text teaser = default;
        [SerializeField] Text title = default;
        [SerializeField] Image image = default;
        [SerializeField] Button button = default;

        static class AnimatorHash
        {
            public static readonly int Scroll = Animator.StringToHash("scroll");
        }

        void Start()
        {
            button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
        }

        public override void UpdateContent(ItemData itemData)
        {
            /* 2019-09-12 
            Here we control the information in cell
             */
            var count = itemData.Count;
            index.text = Index.ToString()+"/"+count;
            teaser.text = itemData.Message;
            title.text = itemData.Name;
            image.sprite = itemData.Sprite;
        }

        public override void UpdatePosition(float position)
        {
            currentPosition = position;

            if (animator.isActiveAndEnabled)
            {
                animator.Play(AnimatorHash.Scroll, -1, position);
            }

            animator.speed = 0;
        }

        // GameObject が非アクティブになると Animator がリセットされてしまうため
        // 現在位置を保持しておいて OnEnable のタイミングで現在位置を再設定します
        float currentPosition = 0;

        void OnEnable() => UpdatePosition(currentPosition);
    }
}
