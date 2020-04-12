using System;
using System.Collections.Generic;
using System.Text;

namespace KeyboardOS.Framework.Views.BasicViews
{
    public class OutlinedImageBox : ImageBox
    {
        public OutlinedImageBox(int x, int y, int width, int height, ViewBase parent) : base(x, y, width, height, parent)
        {
        }

        public override void DrawView()
        {
            base.DrawView();

            for(int i = 0; i<width; i+=1)
            {
                WritePixel(i, 0, 1);
                WritePixel(i, height - 1, 1);
            }
            for (int i = 0; i < height; i += 1)
            {
                WritePixel(0, i, 1);
                WritePixel(width - 1, i, 1);
            }
        }
    }
}
