using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.UI;

namespace Battleship.src.Scenes
{
    public class GameScene : Scene
    {

        public UICanvas Canvas;
        public Table _table;

        //Controller
        GameControllers GameControllers;

        public override void Initialize()
        {
            Color miColor = new Color(0x91, 0xCD, 0xAE, 255);
            ClearColor = miColor;

            GameControllers = new GameControllers(this);



        }
        public override void Update()
        {
            base.Update();
            GameControllers.Update();
        }
    }
}
