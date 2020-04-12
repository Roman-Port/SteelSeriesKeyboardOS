using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyboardOS.Framework.Views
{
    /// <summary>
    /// Represents a monochrome display to be used
    /// </summary>
    public class DisplayFrame : ViewBase
    {
        public int width;
        public int height;
        public bool[,] canvas;

        public DisplayFrame(int width, int height) : base()
        {
            this.width = width;
            this.height = height;
            this.canvas = new bool[width, height];
        }

        public override void DrawView()
        {
            //Clear
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    canvas[x, y] = false;
                }
            }

            //Redraw
            foreach (var c in children)
                c.DrawView();
        }

        public byte[] GetSerializedData()
        {
            //Export
            byte[] buffer = new byte[(width * height) / 8];
            int pos = 0;
            byte posMinor = 0;
            int canvasPos = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (canvas[x, y])
                        buffer[pos] |= (byte)(1UL << (7 - posMinor));
                    posMinor++;
                    canvasPos++;
                    if (posMinor > 7)
                    {
                        posMinor = 0;
                        pos++;
                    }
                }
            }

            //FUCK
            /*for (int i = 0; i<buffer.Length; i++)
            {
                for (byte j = 0; j < 8; j++)
                {
                    if (Program.rand.Next(0, 4) == 1)
                    {
                        buffer[i] ^= (byte)(1 << j);
                    }
                }
            }*/

            return buffer;
        }

        public Image<Rgba32> GetScreenshotData()
        {
            Image<Rgba32> img = new Image<Rgba32>(width, height);
            for(int x = 0; x<width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (canvas[x, y])
                        img[x, y] = new Rgba32(255, 255, 255);
                    else
                        img[x, y] = new Rgba32(0, 0, 0);
                }
            }
            return img;
        }

        public override void WritePixel(int x, int y, int value)
        {
            if (x >= width || x < 0)
                return;
            if (y >= height || y < 0)
                return;
            if (value == 0)
                canvas[x, y] = false;
            else
                canvas[x, y] = true;
        }
    }
}
