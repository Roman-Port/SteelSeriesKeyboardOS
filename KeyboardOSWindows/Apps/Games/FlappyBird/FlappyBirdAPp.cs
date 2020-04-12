using KeyboardOS.Framework.Views;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyboardOS.Framework.Views.BasicViews;

namespace KeyboardOS.Apps.Games.FlappyBird
{
    public class FlappyBirdApp : KeyboardApp
    {
        public FlappyBirdApp(OSInstance instance) : base(instance)
        {
        }

        public int timer = 0;
        public FlappyBirdView fp;
        public TextBox scoreView;
        public InnerView scoreboard;
        public bool intro = true;

        public const int SCOREBOARD_WIDTH = 24;

        public override Image<Rgba32> GetAppIcon()
        {
            return LoadImageFromAsset("default_image", "png");
        }

        public override string GetAppName()
        {
            return "Flappy";
        }

        public override void OnAppClosed()
        {
            
        }

        public override void OnAppLaunched()
        {
            DisableStatusBar();
            intro = true;
            fp = new FlappyBirdView(0, 0, appFrame.width - SCOREBOARD_WIDTH, appFrame.height, appFrame);

            //Create scoreboard
            scoreboard = new InnerView(width - SCOREBOARD_WIDTH, 0, SCOREBOARD_WIDTH, height, appFrame);
            scoreboard.effect_invert = true;
            scoreView = new TextBox(0, (height / 2) - 4, SCOREBOARD_WIDTH, 8, scoreboard, "0", TextBox.TextFormatting.Right);

            Reset();
        }

        public override void OnAppResumed()
        {
            context.KeyboardOSInputEvent += Context_KeyboardOSInputEvent;
        }

        private void Context_KeyboardOSInputEvent(Framework.InputKey key)
        {
            if (key == Framework.InputKey.Enter)
            {
                fp.OnJump();
                intro = false;
            }
            if (key == Framework.InputKey.Up || key == Framework.InputKey.Down)
                Reset();
            if (key == Framework.InputKey.Back)
                CloseApp();
        }

        public override void OnAppSuspended()
        {
            context.KeyboardOSInputEvent -= Context_KeyboardOSInputEvent;
        }

        public override void OnAppTicked()
        {
            //If we lost, abort
            if(fp.lost)
            {
                fp.effect_invert = true;
                scoreboard.effect_invert = false;
                return;
            }
            
            //Tick and update all
            if(!intro)
                fp.GameTick();
            scoreView.text = Math.Max(fp.GetCurrentPipeIndex(), 0).ToString();
        }

        public void Reset()
        {
            fp.lost = false;
            fp.effect_invert = false;
            scoreboard.effect_invert = true;
            fp.pipe_heights.Clear();
            fp.player_y = FlappyBirdView.INITIAL_PLAYER_POS;
            fp.level_scroll = 0;
            fp.player_velocity = 0;
        }
    }
}
