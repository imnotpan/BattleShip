using Battleship.src.Scenes;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Battleship.src.Controllers.Ships;

namespace Battleship
{
    public class Game1 : Core
    {
        public Game1() : base(640 * 2, 480 * 2)
        {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            DebugRenderEnabled = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Scene.SetDefaultDesignResolution(640, 480, Scene.SceneResolutionPolicy.NoBorder);
            Scene = new GameScene();

        }

    }

    public static class Constants
    {
        public static int SCALE_WIDTH = 2;
        public static int PIX_SCREEN_WIDTH = Game1.GraphicsDevice.Viewport.Width/ SCALE_WIDTH;
        public static int PIX_SCREEN_HEIGHT = Game1.GraphicsDevice.Viewport.Height/ SCALE_WIDTH;

    }
}