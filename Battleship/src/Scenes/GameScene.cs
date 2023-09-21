using Battleship.src.Controllers;
using Battleship.src.Controllers.Ships;
using Microsoft.Xna.Framework;
using Nez;
using System;
using System.ComponentModel;

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
            _textureLoader.loadTexture("Sprites/ship_Carrier");
            _textureLoader.loadTexture("Sprites/ship_Cruiser");
            _textureLoader.loadTexture("Sprites/ship_PatrolBoat");




            /* Game */
            _game = new GameManager(this, _textureLoader);

        }
        public override void Update()
        {
            base.Update();
            _game.Update();
        }
    }
}
