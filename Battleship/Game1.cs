using Battleship.src.Scenes;
using Microsoft.Xna.Framework;
using Nez;
using System;

namespace Battleship
{
    public class Game1 : Core
    {
        public Vector2 ScreenPixelResolution;

        public Game1() : base(1280, 720)
        {
            IsMouseVisible = true;

            //DebugRenderEnabled = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Scene.SetDefaultDesignResolution(720, 576, Scene.SceneResolutionPolicy.NoBorderPixelPerfect);
            Scene = new GameScene();
        }
        

    }

    public static class Constants
    {
        public static int SCALE_WIDTH = 2;
        public static int PIX_SCREEN_WIDTH = 720;
        public static int PIX_SCREEN_HEIGHT = 576;
    }
}