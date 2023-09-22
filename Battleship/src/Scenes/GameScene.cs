using Battleship.src.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.UI;

namespace Battleship.src.Scenes
{
    internal class GameScene : Scene
    {


        /* Game Controllers */
        private GameManager _game;
        private TextureLoader _textureLoader;


        public override void Initialize()
        {
            /* Load Texture */
            _textureLoader = new TextureLoader(Content);
            _textureLoader.loadTexture("Sprites/Celda");
            _textureLoader.loadTexture("Sprites/GridEnemy");

            _textureLoader.loadTexture("Sprites/ship_BattleShip");
            _textureLoader.loadTexture("Sprites/ship_Carrier");
            _textureLoader.loadTexture("Sprites/ship_Cruiser");
            _textureLoader.loadTexture("Sprites/ship_PatrolBoat");

            /* Game */
            _game = new GameManager(this, _textureLoader);

            Color miColor = new Color(0x8F, 0xC0, 0xC0, 255); // 255 para el valor alfa (sin transparencia)

            ClearColor = miColor; // You can replace Color.Black with any other Color value you prefer

        }
        public override void Update()
        {
            base.Update();
            _game.Update();
        }
    }
}
