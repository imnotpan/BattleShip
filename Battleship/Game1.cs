using Battleship.src.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using System.Drawing;

namespace Battleship
{
    public class Game1 : Core
    {
        public Vector2 ScreenPixelResolution;

        public Game1() : base(1280, 720)
        {
            IsMouseVisible = true;
            DefaultSamplerState = SamplerState.PointClamp;
            
            DebugRenderEnabled = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            Texture2D pixCursor = Content.Load<Texture2D>("Sprites/HUD/Mouse");
            int cursorWidth = pixCursor.Width;
            int cursorHeight = pixCursor.Height;
            var mouse = MouseCursor.FromTexture2D(pixCursor, cursorWidth / 2, cursorHeight / 2 - 32);
            Mouse.SetCursor(mouse);
            

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