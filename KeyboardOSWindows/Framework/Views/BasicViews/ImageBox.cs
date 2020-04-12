using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyboardOS.Framework.Views.BasicViews
{
    public class ImageBox : View
    {
        public ImageBox(int x, int y, int width, int height, ViewBase parent) : base(x, y, width, height, parent)
        {
            buffer = new byte[width, height];
        }

        public byte[,] buffer;

        public void SetImage(Image<Rgba32> img)
        {
            for(int x = 0; x<img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    if (img[x, y].R > (byte.MaxValue / 2))
                        buffer[x, y] = 1;
                    else
                        buffer[x, y] = 0;
                }
            }
        }

        public override void DrawView()
        {
            base.DrawView();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    WritePixel(x, y, buffer[x, y]);
                }
            }
        }
    }
}
