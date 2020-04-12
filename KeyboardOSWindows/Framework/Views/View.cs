using System;
using System.Collections.Generic;
using System.Text;

namespace KeyboardOS.Framework.Views
{
    public abstract class View : ViewBase
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public ViewBase parent;

        public bool effect_invert;
        public bool hidden;

        public View(int x, int y, int width, int height, ViewBase parent, bool addToParent = true) : base()
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.parent = parent;
            this.children = new List<View>();
            if(addToParent)
                this.parent.children.Add(this);
        }

        public override void DrawView()
        {
            //Draw
            DrawBackground();
            
            //Draw children
            foreach (var c in children)
                c.DrawView();
        }

        public override void WritePixel(int x, int y, int value)
        {
            if (hidden)
                return;
            if (effect_invert && value == 0)
                value = 1;
            else if (effect_invert && value == 1)
                value = 0;

            parent.WritePixel(x + this.x, y + this.y, value);
        }

        public virtual void DrawBackground()
        {
            //Draw background
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    WritePixel(x, y, 0);
                }
            }
        }
    }
}
