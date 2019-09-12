using System;

namespace FancyScrollView.MonResto
{
    public class Context
    {
        public int SelectedIndex = -1;
        public Action<int> OnCellClicked;
    }
}
