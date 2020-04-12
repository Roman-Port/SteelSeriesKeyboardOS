using KeyboardOS.Framework.Views.BasicViews;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyboardOS.Framework.Views.SystemViews
{
    public class StatusBar : View
    {
        public StatusBar(int x, int y, int width, int height, ViewBase parent) : base(x, y, width, height, parent)
        {
            titleBox = new TextBox(0, 0, width, height - 2, this, title, TextBox.TextFormatting.Left);
            timeBox = new TextBox(0, 0, width, height - 2, this, title, TextBox.TextFormatting.Right);
        }

        public string title;

        private TextBox titleBox;
        private TextBox timeBox;
        private int tickCounter;

        public override void DrawView()
        {
            //Update fields
            titleBox.text = title;
            timeBox.text = DateTime.Now.ToShortTimeString().Replace(" PM", "").Replace(" AM", "");
            tickCounter++;
            if (tickCounter >= 10)
                timeBox.text = timeBox.text.Replace(':', ' ');
            if (tickCounter >= 20)
                tickCounter = 0;

            base.DrawView();

            //Draw line on the bottom
            for (int i = 0; i < width; i++)
                WritePixel(i, height - 1, 1);
        }
    }
}
