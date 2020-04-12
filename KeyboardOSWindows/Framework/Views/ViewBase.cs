using System;
using System.Collections.Generic;
using System.Text;

namespace KeyboardOS.Framework.Views
{
    public abstract class ViewBase
    {
        public List<View> children;

        public ViewBase()
        {
            this.children = new List<View>();
        }

        public abstract void DrawView();
        public abstract void WritePixel(int x, int y, int value);

        public void RemoveAllChildren()
        {
            children.Clear();
        }
    }
}
