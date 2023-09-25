using Battleship.src.Controllers;
using Battleship.src.Controllers.UI.GameHud;
using Battleship.src.Controllers.UI.MainMenu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.UI;
using System;
using System.Runtime.CompilerServices;

namespace Battleship.src.Scenes
{
    internal class GameScene : Scene
    {


        /* Game Controllers */
        private GameManager GameManager;
        private TextureLoader TextureLoader;
        public MainMenu MainMenu;
        public GameHud GameHud;

        public UICanvas Canvas;
        public Table _table;

        Batcher spriteBatch;

        public override void Initialize()
        {
            Color miColor = new Color(0x91, 0xCD, 0xAE, 255);
            ClearColor = miColor;
            
            /* Load Texture */
            TextureLoader = new TextureLoader(Content);
            TextureLoader.loadTexture("Sprites/Celda");
            TextureLoader.loadTexture("Sprites/GridEnemy");

            TextureLoader.loadTexture("Sprites/ship_BattleShip");
            TextureLoader.loadTexture("Sprites/ship_Carrier");
            TextureLoader.loadTexture("Sprites/ship_Cruiser");
            TextureLoader.loadTexture("Sprites/ship_PatrolBoat");

            TextureLoader.loadTexture("Sprites/Flag");


            Canvas = CreateEntity("ui").AddComponent(new UICanvas());
            Canvas.IsFullScreen = true;
            Canvas.RenderLayer = -1;

            GameManager = new GameManager(this, TextureLoader);
            MainMenu = new MainMenu(this, GameManager);
            GameHud = new GameHud(this, GameManager);



            SpriteFont font;
            
            font = Content.Load<SpriteFont>("Fonts/rockinRecordFont");
            var NezSprite = new NezSpriteFont(font);


            var textEntity = new Entity();
            var textComponent = new TextComponent(NezSprite, "TEXTO DE EJEMPLO", new Vector2(Constants.PIX_SCREEN_WIDTH/2, Constants.PIX_SCREEN_HEIGHT/2), Color.White);
            textComponent.RenderLayer = -100;
            textEntity.Scale = new Vector2(0.25f, 0.25f);
            textEntity.AddComponent(textComponent);
            AddEntity(textEntity);

        }
        public override void Update()
        {
            base.Update();
            GameManager.Update();
            MainMenu.Update();
            GameHud.Update();

        }
    }
}
