using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardOS.Framework.Views.BasicViews
{
    public class VScrollView : View
    {
        public VScrollView(int x, int y, int width, int height, ViewBase parent, bool addToParent = true) : base(x, y, width, height, parent, addToParent)
        {
        }

        public int offset;
        public int contentHeight;

        public override void WritePixel(int x, int y, int value)
        {
            //Set the content height
            contentHeight = Math.Max(contentHeight, y);
            
            //Get real Y
            int targetY = y - offset;

            //Make sure not to draw out of bounds
            if (targetY - this.y < 0)
                return;
            
            base.WritePixel(x, targetY, value);
        }

        public void ScrollBy(int offset)
        {
            ScrollTo(this.offset + offset);
        }

        public void ScrollTo(int next)
        {
            if (next < 0)
                next = 0;
            if (next > contentHeight - this.height)
                next = contentHeight - this.height;
            offset = next;
        }
    }
}
