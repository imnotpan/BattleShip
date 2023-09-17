using Battleship.src.Controllers;
using Battleship.src.Controllers.Ships;
using Microsoft.Xna.Framework;
using Nez;
using System;

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
            _textureLoader.loadTexture("Sprites/ship_BattleShip");


            /* Game */
            _game = new GameManager(this, _textureLoader);

        }
        public override void Update()
        {
            base.Update();
        }
    }
}
