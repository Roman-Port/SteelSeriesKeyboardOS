using KeyboardOS.Framework.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardOS.Apps.Games.FlappyBird
{
    public class FlappyBirdView : View
    {
        public FlappyBirdView(int x, int y, int width, int height, ViewBase parent, bool addToParent = true) : base(x, y, width, height, parent, addToParent)
        {
        }

        public const int INITIAL_PLAYER_POS = 15;
        public const int PLAYER_POS_X = 10;
        public const int PLAYER_SIZE = 6;
        public const int PIPE_INTERVAL = 30;
        public const int PIPE_MARGIN = 80;
        public const int PIPE_WIDTH = 5;
        public const int PIPE_OPENING_HEIGHT = 18;
        public const int PIPE_OPENING_MIN = 1;
        public const int PIPE_OPENING_MAX = 22;

        public Random rand = new Random();
        public int player_y = INITIAL_PLAYER_POS;
        public int player_velocity = 0;
        public int level_scroll;
        public List<int> pipe_heights = new List<int>();
        public bool lost = false;

        public override void DrawView()
        {
            base.DrawView();

            //Draw top and bottom boundries
            for(int x = 0; x<width; x++)
            {
                WritePixel(x, 0, 1);
                WritePixel(x, height - 1, 1);
            }

            //Draw background
            /*for (int x = 0; x < width; x++) {
                double h = (Math.Sin((level_scroll + x)/4) + 2) * 2;
                for(int y = 0; y<h; y++)
                {
                    WritePixel(x, height-y, 1);
                }
            }*/

            //Draw player
            for(int x = 0; x<PLAYER_SIZE; x++)
            {
                for (int y = 0; y < PLAYER_SIZE; y++)
                {
                    WritePixel(PLAYER_POS_X + x, y + player_y, 1);
                }
            }

            //Draw each pipe on the screen
            for(int i = 0; i<GetPipeIndexFromScreenPixel(width + PIPE_INTERVAL); i+=1)
            {
                int pos = ((i * PIPE_INTERVAL) + PIPE_MARGIN) - level_scroll;
                int height = 0;
                while (pipe_heights.Count <= i)
                    pipe_heights.Add(rand.Next(PIPE_OPENING_MIN, PIPE_OPENING_MAX));
                height = pipe_heights[i];
                for(int x = 0; x< PIPE_WIDTH; x++)
                {
                    //Draw
                    for (int y = 0; y < this.height; y += 1)
                    {
                        if(y < height || y > height + PIPE_OPENING_HEIGHT)
                            WritePixel(pos + x, y, 1);
                    }

                    //If this is on the same V-Line as the player, check to see if a collision happened
                    if(pos + x >= PLAYER_POS_X && pos + x < PLAYER_POS_X + PLAYER_SIZE   &&   ((player_y < height) || (player_y + PLAYER_SIZE) > height + PIPE_OPENING_HEIGHT))
                    {
                        lost = true;
                    }
                }
            }

            //Check if the player has fallen off the top or bottom of the screen
            if (player_y + (PLAYER_SIZE / 2) < 0)
                lost = true;
            if (player_y + (PLAYER_SIZE / 2) > height)
                lost = true;
        }

        public int GetCurrentPipeIndex()
        {
            return GetPipeIndexFromScreenPixel(PLAYER_POS_X + PLAYER_SIZE);
        }

        public int GetPipeIndexFromScreenPixel(int x)
        {
            return ((level_scroll + x) - PIPE_MARGIN) / PIPE_INTERVAL;
        }

        public void GameTick()
        {
            //Do player physics
            player_y -= player_velocity;
            player_velocity -= 1;

            //Scroll level
            level_scroll += 2;
        }

        public void OnJump()
        {
            player_velocity = 3;
        }
    }
}
